using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using dts.server.Commons;

namespace dts.server.BlockProcessing
{
    public interface IBlockProcessorFactory
    {
        IBlockProcessor Create(BlockingCollection<Block> outputQueue, CountdownEvent completionSignal);
    }

    [Export(typeof(IBlockProcessorFactory))]
    public class BlockProcessorFactory : IBlockProcessorFactory
    {
        public IBlockProcessor Create(BlockingCollection<Block> outputQueue, CountdownEvent completionSignal)
        {
            return new BlockProcessor(outputQueue, completionSignal);
        }
    }
}
