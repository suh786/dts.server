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
    public abstract class DelimitedFileReaderBase : ReaderBase
    {
        private readonly string _filePath;

        protected DelimitedFileReaderBase(string filePath, BlockingCollection<Block> outputQueue, string name = null) : base(outputQueue, name)
        {
            _filePath = filePath;
        }

        protected sealed override void OnRead()
        {
            try
            {
                var rowRecords = new List<IRowRecord>();
                using (var fileStream = new StreamReader(new FileStream(_filePath, FileMode.Open)))
                {
                    while (!fileStream.EndOfStream)
                    {
                        var line = fileStream.ReadLine();
                        var attributes = line.Split(new[] {','});
                        rowRecords.Add(CreateRowRecord(attributes));

                        if(rowRecords.Count % BLOCK_SIZE == 0 || fileStream.EndOfStream)
                        {
                            var block = CreateBlock(rowRecords.ToArray(), fileStream.EndOfStream);
                            SubmitToProcess(block);
                            rowRecords.Clear();
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected abstract IRowRecord CreateRowRecord(string[] attributes);
    }
}
