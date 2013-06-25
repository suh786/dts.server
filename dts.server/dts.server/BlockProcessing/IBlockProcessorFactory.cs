using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dts.server.Commons;

namespace dts.server.BlockProcessing
{
    public interface IBlockProcessorFactory
    {
        IBlockProcessor Create(BlockingCollection<Block> outputQueue);
    }
}
