using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using dts.server.Commons;

namespace dts.server.BlockProcessing
{
    public interface IBlockProcessor : IDisposable
    {
        /// <summary>
        /// Process a block per processor in a seperate thread.
        /// </summary>
        /// <param name="block"></param>
        void Process(Block block);
    }

    public class BlockProcessor : DisposebleObject, IBlockProcessor
    {
        private readonly BlockingCollection<Block> _outputQueue;
        private readonly CountdownEvent _completionSignal;

        public BlockProcessor(BlockingCollection<Block> outputQueue, CountdownEvent completionSignal)
        {
            _outputQueue = outputQueue;
            _completionSignal = completionSignal;
        }

        public void Process(Block block)
        {
            var blockToProcess = block;
            Task.Factory.StartNew(() => ProcessBlock(blockToProcess));
        }

        private void ProcessBlock(Block block)
        {
            try
            {
                foreach (var inputRecord in block.InputRecords)
                {
                    //apply transformations here

                    block.OutputRecords.Add(inputRecord);
                }

                _outputQueue.Add(block);
            }
            finally
            {
                _completionSignal.Signal();
            }
        }
    }
}
