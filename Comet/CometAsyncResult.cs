using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Comet
{
    public class CometAsyncResult :IAsyncResult
    {
        private HttpContext context;
        private object extraData;

          
        public CometAsyncResult( HttpContext context, AsyncCallback cb, object extraData )
        {
            this.context=context;
            this.cb=cb;
            this.extraData = extraData;
        }

        public object ext { get; set; }

        public HttpContext Context
        {
            get { return this.context; }
        }


        public object AsyncState
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public WaitHandle AsyncWaitHandle
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool CompletedSynchronously
        {
            get
            {
                return false;
            }
        }

        private bool isCompleted = false;
        private AsyncCallback cb;

        public bool IsCompleted
        {
            get
            {
                return this.isCompleted;
            }
        }

        public void SetCompleted()
        {
            this.isCompleted = true;

            if( cb != null )
            {
                cb(this);
            }
        }
    }
}
