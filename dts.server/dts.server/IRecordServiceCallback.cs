using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace dts.server
{
    public interface IRecordServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void RecordAdded(string recordId, IEnumerable<string> record);
    }
}
