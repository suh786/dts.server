using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using dts.server.BlockProcessing;
using dts.server.Commons;

namespace dts.server.Reader
{
    public abstract class DelimitedFileReaderBase : DisposebleObject, IReader
    {
        private const int BLOCK_SIZE = 50;
        private readonly string _filePath;
        private readonly BlockingCollection<Block> _outputQueue;
        [Import]
        private IBlockProcessorFactory _blockProcessorFactory;

        private CountdownEvent _blockProcessorsCompletionSignal;

        protected DelimitedFileReaderBase(string filePath, BlockingCollection<Block> outputQueue)
        {
            _filePath = filePath;
            _outputQueue = outputQueue;
            SystemManager.InjectServices(this);
        }

        public void Read()
        {
            try
            {
                Console.WriteLine(DateTime.Now.ToLongTimeString() + "Reader started.");
                var rowRecords = new List<IRowRecord>();
                var blockNumber = 0;
                using (var fileStream = new StreamReader(new FileStream(_filePath, FileMode.Open)))
                {
                    while (!fileStream.EndOfStream)
                    {
                        var line = fileStream.ReadLine();
                        var attributes = line.Split(new[] {','});
                        rowRecords.Add(CreateRowRecord(attributes));

                        if(rowRecords.Count % BLOCK_SIZE == 0 || fileStream.EndOfStream)
                        {
                            AddBlockProcessorsSignalCount();
                            CreateAndStartBlockProcessor(rowRecords, blockNumber, _blockProcessorsCompletionSignal, blockNumber == 0, fileStream.EndOfStream);
                            blockNumber++;
                            rowRecords.Clear();
                        }
                    }
                }

                _blockProcessorsCompletionSignal.Wait();

                _outputQueue.CompleteAdding();

                Console.WriteLine(DateTime.Now.ToLongTimeString() + "Reader finished.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddBlockProcessorsSignalCount()
        {
            if(_blockProcessorsCompletionSignal == null)
                _blockProcessorsCompletionSignal = new CountdownEvent(1);
            else
                _blockProcessorsCompletionSignal.AddCount();
        }

        private void CreateAndStartBlockProcessor(List<IRowRecord> rowRecords, int blockNumber, CountdownEvent completionSignal, bool isFirstBlock = false, bool isLastBlock = false)
        {
            var blockProcessor = _blockProcessorFactory.Create(_outputQueue, completionSignal);
            var block = new Block(blockNumber, rowRecords.ToArray(), isFirstBlock, isLastBlock);
            blockProcessor.Process(block);
        }

        protected abstract IRowRecord CreateRowRecord(string[] attributes);

        protected override void DisposeInternal()
        {


            base.DisposeInternal();
        }
    }
}
