using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Comet
{
    public class DefaultChannelHandler :IHttpAsyncHandler
    {

        private static CometStateManager stateManager;

        static DefaultChannelHandler()
        {
            stateManager = new CometStateManager();

            stateManager.IdleClientKilled += StateManager_IdleClientKilled;
            stateManager.ClientInitialized+= StateManager_ClientInitialized;
            stateManager.ClientSubscribed += StateManager_ClientSubscribed;

        }

        private static void StateManager_ClientSubscribed( object sender, EventArgs e )
        {
            throw new NotImplementedException();
        }

        private static void StateManager_ClientInitialized( object sender, EventArgs e )
        {
            throw new NotImplementedException();
        }

        private static void StateManager_IdleClientKilled( object sender, EventArgs e )
        {
            throw new NotImplementedException();
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public IAsyncResult BeginProcessRequest( HttpContext context, AsyncCallback cb, object extraData )
        {
            return stateManager.BeginSubscribe(context, cb, extraData);
        }

        public void EndProcessRequest( IAsyncResult result )
        {
            stateManager.EndSubscribe(result);
        }

        public void ProcessRequest( HttpContext context )
        {
            throw new NotImplementedException();
        }
    }
}
