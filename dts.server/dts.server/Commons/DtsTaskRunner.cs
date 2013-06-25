﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using dts.server.BlockProcessing;
using dts.server.OutputWriter;
using dts.server.Reader;

namespace dts.server.Commons
{
    internal class DtsTaskRunner
    {
        private readonly DtsTask _task;
        private IOutputWriterFactory _outputWriterFactory;
        private IReaderFactory _readerFactory; 
        private BlockingCollection<Block> _outputQueue;

        public DtsTaskRunner(DtsTask task)
        {
            _task = task;
        }

        public void Start()
        {
            PrepareAndStart();
        }

        private void PrepareAndStart()
        {
            var outputWriter = _outputWriterFactory.Create(_task.TargetType, _outputQueue, _task.Target);
            var reader = _readerFactory.Create(_task.SourceType, _task.SourceName, _outputQueue);

            //Start writer and reader, and send blocks to block processor.
            var executor = new TaskExecutor(reader, outputWriter);
            executor.Execute();
        }

        public void Stop()
        {
            
        }
    }
}
