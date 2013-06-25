using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dts.server.Commons
{
    public class DtsTask
    {
        public DtsTask(SourceType sourceType, string sourceName, TargetType targetType, ITarget target)
        {
            SourceType = sourceType;
            SourceName = sourceName;
            TargetType = targetType;
            Target = target;
        }

        public SourceType SourceType { get; private set; }
        public string SourceName { get; private set; }

        public TargetType TargetType { get; private set; }
        public ITarget Target { get; private set; }
    }
}
