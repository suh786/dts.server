using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dts.server.Commons
{
    public interface ITarget
    {
        void AddRecords(IEnumerable<IRowRecord> records);
        void UpdateRecords(IEnumerable<IRowRecord> records);
        void DeleteRecords(IEnumerable<string> ids);
    }
}
