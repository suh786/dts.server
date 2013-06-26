using System.Collections.Generic;
using dts.server.Commons;

namespace dts.server.BlockProcessing
{
    public class Block
    {
        public Block(int blockNumber, IEnumerable<IRowRecord> inputRecords, bool isFirstBlock = false, bool isLastBlock = false)
        {
            BlockNumber = blockNumber;
            InputRecords = inputRecords;
            IsFirstBlock = isFirstBlock;
            IsLastBlock = isLastBlock;
            OutputRecords = new List<IRowRecord>();
        }

        public int BlockNumber { get; private set; }
        public IEnumerable<IRowRecord> InputRecords { get; private set; }

        public IList<IRowRecord> OutputRecords { get; private set; }

        public bool IsFirstBlock { get; private set; }
        public bool IsLastBlock { get; private set; }
    }
}