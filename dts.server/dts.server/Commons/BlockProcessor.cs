using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dts.server.Commons
{
    internal class BlockProcessor
    {
        public BlockProcessor(IEnumerable<string> _records, ConcurrentQueue<Person> _outputQueue)
        {
            Records = _records;
            OutputQueue = _outputQueue;
        }

        public IEnumerable<string> Records { get; private set; }
        public ConcurrentQueue<Person> OutputQueue { get; private set; }

        public void Process()
        {
            Task.Factory.StartNew(ParseRecords);
        }

        private void ParseRecords()
        {
            foreach (var record in Records)
            {
                var attributes = record.Split(new[] {','});

                var person = new Person
                                        {
                                            Id = attributes[0],
                                            FirstName = attributes[1],
                                            MiddleName = attributes[2],
                                            LastName = attributes[3],
                                            Address = attributes[4],
                                            Age = Convert.ToInt32(attributes[5]),
                                            FatherName = attributes[6],
                                            MotherName = attributes[7],
                                            OfficeName = attributes[8],
                                            OfficeAddress = attributes[9]
                                        };

                var personModified = ApplyTransformations(person);

                OutputQueue.Enqueue(personModified);
            }
        }

        private Person ApplyTransformations(Person person)
        {
            var modifiedPersons = new Person
                                      {
                                          Id = person.Id,
                                          FirstName = person.FirstName + "_Modified",
                                          MiddleName = person.MiddleName + "_Modified",
                                          LastName = person.LastName + "_Modified",
                                          Address = person.Address + "_Modified",
                                          Age = person.Age + 1,
                                          FatherName = person.FatherName + "_Modified",
                                          MotherName = person.MotherName + "_Modified",
                                          OfficeName = person.OfficeName + "_Modified",
                                          OfficeAddress = person.OfficeAddress + "_Modified"
                                      };

            return modifiedPersons;
        }
    }
}
