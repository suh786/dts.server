using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dts.server.Commons
{
    internal class TaskRunner
    {
        private const string filePath = "Persons.txt";
        private ConcurrentQueue<Person> _outputQueue;
        private volatile bool _isFinishedReading;
        private CancellationTokenSource _taskCancellationTokenSource;

        public TaskRunner(IPersonServiceCallback callback)
        {
            PersonServiceCallback = callback;
            _outputQueue = new ConcurrentQueue<Person>();
        }

        public IPersonServiceCallback PersonServiceCallback { get; private set; }

        public void Start()
        {
            _taskCancellationTokenSource = new CancellationTokenSource();
            //Task.Factory.StartNew(() => ReadAndStartBlockPrcoessor());
            Task.Factory.StartNew(() => PublishPersons());
            ReadAndStartBlockPrcoessor();
            
        }

        private void PublishPersons()
        {
            while(!_isFinishedReading || !_outputQueue.IsEmpty)
            {
                Person person;
                if (_outputQueue.TryDequeue(out person))
                {
                    Debug.WriteLine("Sending person: " + person.ToString());
                    PersonServiceCallback.RecordAdded(person);
                }
            }
        }

        private void ReadAndStartBlockPrcoessor()
        {
            using(var file = new StreamReader(filePath))
            {
                Debug.WriteLine(file.ToString());
                var lines = new List<string>();
                
                while(!file.EndOfStream)
                {
                    if(lines.Count == 100)
                    {
                        Debug.WriteLine("Starting block processor");
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
