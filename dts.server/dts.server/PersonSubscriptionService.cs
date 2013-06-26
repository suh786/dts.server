using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using dts.server.Commons;
using dts.server.PersonModel;

namespace dts.server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class PersonSubscriptionService : IPersonSubscriptionService
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly Dictionary<string, DtsTaskRunner> _taskRunners;

        public PersonSubscriptionService()
        {
            _taskRunners = new Dictionary<string, DtsTaskRunner>();
        }

        #region IRegistrationService Members

        public bool Subscribe()
        {
            var callback = OperationContext.Current.GetCallbackChannel<IPersonServiceCallback>();
            var runner = new DtsTaskRunner(new DtsTask(SourceType.File, "Persons.txt", TargetType.Callback, new PersonCallbackTarget(callback)));
            var subscriberId = Guid.NewGuid().ToString();
            Debug.WriteLine("Subscriber registerd: " + subscriberId);

            _taskRunners.Add(subscriberId, runner);
            Task.Factory.StartNew(runner.Start);
            return true;
        }
        
        public bool Unsubscribe()
        {
            /*if (_runner != null)
            {
                _runner.Stop();
            }*/

            return true;
        }

        #endregion
    }
}
