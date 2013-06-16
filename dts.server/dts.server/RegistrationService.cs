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
        private readonly Dictionary<string, TaskRunner> _recordServiceCallbacks;

        public RegistrationService()
        {
            _recordServiceCallbacks = new Dictionary<string, TaskRunner>();
        }

        public RegistrationService(IServiceLocator serviceLocator) : this()
        {
            _serviceLocator = serviceLocator;
        }

        #region IRegistrationService Members

        public bool Subscribe(string username)
        {
            if(_recordServiceCallbacks.ContainsKey(username)) return false;
            
            var callback = OperationContext.Current.GetCallbackChannel<IRecordServiceCallback>();
            _recordServiceCallbacks[username] = new TaskRunner(callback);
            _recordServiceCallbacks[username].Start();
            
            return true;
        }
        
        public bool Unsubscribe(string username)
        {
            if(!_recordServiceCallbacks.ContainsKey(username)) return false;

            _recordServiceCallbacks.Remove(username);

            return true;
        }

        #endregion
    }
}
