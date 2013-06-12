using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using dts.server.Commons;

namespace dts.server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class RegistrationService : IRegistrationService
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly Dictionary<string, IRecordServiceCallback> _recordServiceCallbacks;
        private volatile bool send;

        public RegistrationService()
        {
            
        }

        public RegistrationService(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        #region IRegistrationService Members

        public bool Subscribe(string username)
        {
            //if(_recordServiceCallbacks.ContainsKey(username)) return false;
            send = true;
            var callback = OperationContext.Current.GetCallbackChannel<IRecordServiceCallback>();
            
            if(_timer == null)
                _timer = new Timer((x) => SendRecords(callback), null, TimeSpan.Zero, TimeSpan.FromSeconds(3));

            return true;
        }
        int i = 1;
        private Timer _timer;

        private void SendRecords(IRecordServiceCallback callback)
        {
            if(send) 
                callback.RecordAdded(i.ToString(), new string[0]);
            i++;
            
        }

        public bool Unsubscribe(string username)
        {
            Task.Factory.StartNew(() => send = false);
            return false;
        }

        #endregion
    }
}
