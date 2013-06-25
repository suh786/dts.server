using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dts.server.Commons;

namespace dts.server.PersonModel
{
    public class PersonCallbackTarget : ITarget
    {
        private readonly IPersonServiceCallback _callback;

        public PersonCallbackTarget(IPersonServiceCallback callback)
        {
            _callback = callback;
        }

        public void AddRecord(IRowRecord record)
        {
            _callback.RecordAdded((Person)record);
        }

        public void UpdateRecord(IRowRecord record)
        {
            throw new NotImplementedException();
        }

        public void DeleteRecord(string id)
        {
            throw new NotImplementedException();
        }
    }
}
