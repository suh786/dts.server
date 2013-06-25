using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dts.server.Commons
{
    public interface ITarget
    {
        void AddRecord(IRowRecord record);
        void UpdateRecord(IRowRecord record);
        void DeleteRecord(string id);
    }
}
