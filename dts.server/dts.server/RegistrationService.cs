using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using dts.server.Commons;

namespace dts.server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class RegistrationService : IRegistrationService
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly Dictionary<string, IRecordServiceCallback> _recordServiceCallbacks;

        public RegistrationService(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        #region IRegistrationService Members

        public bool Subscribe(string username)
        {
            if(_recordServiceCallbacks.ContainsKey(username)) return false;

            _recordServiceCallbacks[username] = OperationContext.Current.GetCallbackChannel<IRecordServiceCallback>();

            return true;
        }

        public bool Unsubscribe(string username)
        {
            return false;
        }

        #endregion
    }
}
