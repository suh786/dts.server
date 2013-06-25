using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using dts.server.Commons;

namespace dts.server.BlockProcessing
{
    public interface IBlockProcessor
    {
        /// <summary>
        /// Process a block per processor in a seperate thread.
        /// </summary>
        /// <param name="block"></param>
        void Process(Block block);
    }

    internal class BlockProcessor
    {
        public void Process(Block block)
        {
        }
    }
}
