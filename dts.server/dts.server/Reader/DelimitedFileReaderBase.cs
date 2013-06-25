using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using dts.server.BlockProcessing;
using dts.server.Commons;

namespace dts.server.Reader
{
    public abstract class DelimitedFileReaderBase : DisposebleObject, IReader
    {
        private const int BLOCK_SIZE = 50;
        private readonly string _filePath;
        private readonly BlockingCollection<Block> _outputQueue;
        private IBlockProcessorFactory _blockProcessorFactory;

        protected DelimitedFileReaderBase(string filePath, BlockingCollection<Block> outputQueue)
        {
            _filePath = filePath;
            _outputQueue = outputQueue;
        }

        public void Read()
        {
            try
            {
                var rowRecords = new List<IRowRecord>();
                var blockNumber = 0;
                using (var fileStream = new StreamReader(new FileStream(_filePath, FileMode.Open)))
                {
                    while (!fileStream.EndOfStream)
                    {
                        var line = fileStream.ReadLine();
                        var attributes = line.Split(new[] {','});
                        rowRecords.Add(CreateRowRecord(attributes));

                        if(rowRecords.Count % BLOCK_SIZE == 0)
                        {
                            CreateAndStartBlockProcessor(rowRecords, blockNumber++);
                            rowRecords.Clear();
                        }
                    }

                    if (rowRecords.Count > 0)
                    {
                        CreateAndStartBlockProcessor(rowRecords, blockNumber);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void CreateAndStartBlockProcessor(List<IRowRecord> rowRecords, int blockNumber)
        {
            var blockProcessor = _blockProcessorFactory.Create(_outputQueue);
            var block = new Block(blockNumber, rowRecords.ToArray());
            blockProcessor.Process(block);
        }

        protected abstract IRowRecord CreateRowRecord(string[] attributes);
    }
}
