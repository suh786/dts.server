using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dts.server.BlockProcessing;
using dts.server.Commons;

namespace dts.server.OutputWriter
{
    public class CallbackWriter : DisposebleObject, IOutputWriter
    {
        private readonly BlockingCollection<Block> _outputQueue;
        private readonly ITarget _callback;

        public CallbackWriter(BlockingCollection<Block> outputQueue, ITarget callback)
        {
            _outputQueue = outputQueue;
            _callback = callback;
        }

        public void Write()
        {
            Console.WriteLine(DateTime.Now.ToLongTimeString() + "Callback writer started.");

            foreach (var block in _outputQueue.GetConsumingEnumerable())
            {
                SendRecords(block.OutputRecords);
            }

            Console.WriteLine(DateTime.Now.ToLongTimeString() + "Callback writer finished writing.");
        }

        private void SendRecords(IEnumerable<IRowRecord> outputRecords)
        {
            foreach (var outputRecord in outputRecords)
            {
                _callback.AddRecord(outputRecord);
            }
        }

        protected override void DisposeInternal()
        {
            _outputQueue.CompleteAdding();

            base.DisposeInternal();
        }
    }
}
