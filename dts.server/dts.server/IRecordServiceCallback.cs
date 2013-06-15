using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using dts.server.Commons;

namespace dts.server
{
    public interface IRecordServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void RecordAdded(Person record);
    }
}
