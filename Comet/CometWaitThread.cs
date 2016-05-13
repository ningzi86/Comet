using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Comet
{
    public class CometWaitThread
    {

        /// <summary>
        /// 构造方法
        /// </summary>
        public CometWaitThread()
        {
            Task.Run(() =>
            {
                QueueCometWaitRequest_WaitCallback();
            });
        }

        /// <summary>
        /// 待处理队列
        /// </summary>
        private List<CometWaitRequest> waitRequests = new List<CometWaitRequest>();

        /// <summary>
        /// 锁
        /// </summary>
        private object state = new object();

        /// <summary>
        /// 获取一个待处理请求
        /// </summary>
        private CometWaitRequest CometWaitRequest
        {
            get
            {
                lock ( state )
                {
                    if( waitRequests.Any() )
                    {
                        CometWaitRequest waitRequest = waitRequests[0];
                        waitRequests.RemoveAt(0);

                        return waitRequest;
                    }

                    return null;
                }
            }
        }

        /// <summary>
        /// 添加一个待处理请求
        /// </summary>
        public void QueueCometWaitRequest( CometWaitRequest request )
        {
            lock ( this.state )
            {
                waitRequests.Add(request);
            }
        }

        /// <summary>
        /// 处理业务逻辑
        /// </summary>
        private void QueueCometWaitRequest_WaitCallback()
        {
            while( true )
            {
                var waitRequest = this.CometWaitRequest;
                if( waitRequest == null )
                {
                    Thread.Sleep(100);
                }
                else
                {
                    //业务逻辑
                    waitRequest.Result.ext = new Random().Next(1000, 9999).ToString();
                    waitRequest.Result.SetCompleted();
                }
            }
        }
    }
}
