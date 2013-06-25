using System.Collections.Generic;
using dts.server.Commons;

namespace dts.server.BlockProcessing
{
    public class Block
    {
        public Block(int blockNumber, IRowRecord[] inputRecords)
        {
            BlockNumber = blockNumber;
        }

        public int BlockNumber { get; private set; }

        public IEnumerable<IRowRecord> OutputRecords { get; private set; }
    }
}