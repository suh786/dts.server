using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dts.server.Commons
{
    public interface IPubSubRecord : IRowRecord
    {
    }

    public interface IIdentifiable
    {
        string Id { get; }
    }
}
