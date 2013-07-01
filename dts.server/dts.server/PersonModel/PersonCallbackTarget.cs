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

        public void AddRecords(IEnumerable<IRowRecord> records)
        {
            _callback.RecordsAdded(records.Cast<Person>().ToArray());
        }

        public void UpdateRecords(IEnumerable<IRowRecord> records)
        {
            throw new NotImplementedException();
        }

        public void DeleteRecords(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }
    }
}
