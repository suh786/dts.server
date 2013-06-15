using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dts.server.Commons
{
    internal class TaskRunner
    {
        private const string filePath = "Persons.txt";
        private ConcurrentQueue<Person> _outputQueue;
        private volatile bool _isFinishedReading;
        public TaskRunner(IRecordServiceCallback callback)
        {
            RecordServiceCallback = callback;
            _outputQueue = new ConcurrentQueue<Person>();
        }

        public IRecordServiceCallback RecordServiceCallback { get; private set; }

        public void Start()
        {
            var taskRead = Task.Factory.StartNew(() => ReadAndStartBlockPrcoessor());
            var taskPublish = Task.Factory.StartNew(() => PublishPersons());
        }

        private void PublishPersons()
        {
            while(!_isFinishedReading && !_outputQueue.IsEmpty)
            {
                Person person;
                _outputQueue.TryDequeue(out person);
                RecordServiceCallback.RecordAdded(person);
            }
        }

        private void ReadAndStartBlockPrcoessor()
        {
            using(var file = new StreamReader(filePath))
            {
                var lines = new List<string>();
                
                while(!file.EndOfStream)
                {
                    if(lines.Count == 100)
                    {
                        StartBlockProcessor(lines);
                        lines.Clear();
                    }
                    
                    lines.Add(file.ReadLine());
                }

                if(lines.Count > 0)
                {
                    StartBlockProcessor(lines);
                    lines.Clear();
                }
            }

            _isFinishedReading = true;
        }

        private void StartBlockProcessor(List<string> lines)
        {
            var blockProcessor = new BlockProcessor(lines.ToArray(), _outputQueue);
            blockProcessor.Process();
        }

        public void Stop()
        {
            
        }
    }
}
