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
        Task Process(Block block);
    }

    public class BlockProcessor : DisposebleObject, IBlockProcessor
    {
        private readonly BlockingCollection<Block> _outputQueue;

        public BlockProcessor(BlockingCollection<Block> outputQueue)
        {
            _outputQueue = outputQueue;
        }

        public Task Process(Block block)
        {
            var blockToProcess = block;
            return Task.Factory.StartNew(() => ProcessBlock(blockToProcess));
        }

        private void ProcessBlock(Block block)
        {
            foreach (var inputRecord in block.InputRecords)
            {
                //apply transformations here

                block.OutputRecords.Add(inputRecord);
            }

            _outputQueue.Add(block);
        }
    }
}
