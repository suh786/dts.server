using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using dts.server.BlockProcessing;
using dts.server.Commons;

namespace dts.server.Reader
{
    public abstract class ReaderBase : DisposebleObject, IReader
    {
        protected const int BLOCK_SIZE = 50;
        private readonly BlockingCollection<Block> _outputQueue;
        private readonly string _name;
        private int _blockNumber;

        [Import]
        private IBlockProcessorFactory _blockProcessorFactory;

        private readonly List<Task> _blockProcessorTasks;

        protected ReaderBase(BlockingCollection<Block> outputQueue, string name = null)
        {
            _outputQueue = outputQueue;
            _name = name ?? GetType().Name;
            _blockProcessorTasks = new List<Task>();
            _blockNumber = 0;

            SystemManager.InjectServices(this);
        }

        public void Read()
        {
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss:ffff - ") + _name + " reader started.");

            OnRead();

            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss:ffff - ") + _name + " reader finished.");

            Task.WaitAll(_blockProcessorTasks.ToArray());

            _outputQueue.CompleteAdding();
        }

        protected abstract void OnRead();

        protected Block CreateBlock(IEnumerable<IRowRecord> inputRecords, bool isLastBlock = false)
        {
            var block = new Block(_blockNumber, inputRecords, _blockNumber == 0, isLastBlock);
            _blockNumber++;
            return block;
        }

        protected void SubmitToProcess(Block block)
        {
            var blockProcessor = _blockProcessorFactory.Create(_outputQueue);

            _blockProcessorTasks.Add(blockProcessor.Process(block));
        }

        protected override void DisposeInternal()
        {
            _outputQueue.CompleteAdding();
            _blockProcessorTasks.Clear();

            base.DisposeInternal();
        }

    }
}
