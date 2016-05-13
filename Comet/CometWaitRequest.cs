using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Comet
{
    public class CometWaitRequest
    {
        private CometAsyncResult result;

        private AsyncCallback callback;
        private HttpContext context;
        private object extraData;

        public CometWaitRequest( HttpContext context, AsyncCallback callback, object extraData )
        {
            this.context   = context;
            this.callback  = callback;
            this.extraData = extraData;

            this.result = new CometAsyncResult(context, callback, extraData); 
        }

        public CometAsyncResult Result { get { return this.result; } }
    }
}
