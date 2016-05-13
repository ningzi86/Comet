using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Comet
{
    public class CometStateManager
    {

        public event EventHandler<EventArgs> ClientInitialized;

        public event EventHandler<EventArgs> IdleClientKilled;

        public event EventHandler<EventArgs> ClientSubscribed;

        /// <summary>
        /// 工作线程
        /// </summary>
        public CometWaitThread[] workerThreads;

        /// <summary>
        /// 总线程
        /// </summary>
        private int totalThread = 5;

        /// <summary>
        /// 当前线程
        /// </summary>
        private int currentThread = 0;

        /// <summary>
        /// 锁
        /// </summary>
        private object state = new object();


        public CometStateManager()
        {
            workerThreads = new CometWaitThread[totalThread];

            for( int i = 0; i< totalThread; i++ )
            {
                workerThreads[i] = new CometWaitThread();
            }
        }


        public IAsyncResult BeginSubscribe( HttpContext context, AsyncCallback callback, object extraData )
        {

            lock ( state )
            {

                CometWaitRequest request = new CometWaitRequest(context, callback, extraData);

                workerThreads[currentThread].QueueCometWaitRequest(request);
                currentThread++;

                if( currentThread >= totalThread )
                {
                    currentThread = 0;
                }

                return request.Result;
            }


        }

        public void EndSubscribe( IAsyncResult result )
        {

            CometAsyncResult cometAsyncResult = result as CometAsyncResult;

            if( cometAsyncResult  != null )
            {
                cometAsyncResult.Context.Response.Write(cometAsyncResult.ext);
                cometAsyncResult.Context.Response.End();
            }

        }
    }
}
