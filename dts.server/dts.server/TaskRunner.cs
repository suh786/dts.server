using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dts.server
{
    internal class TaskRunner
    {
        private IRecordServiceCallback _recordServiceCallback;

        public TaskRunner(IRecordServiceCallback recordServiceCallback)
        {
            _recordServiceCallback = recordServiceCallback;
        }

        public void Execute()
        {
        }
    }
}
