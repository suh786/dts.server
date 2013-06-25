using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dts.server.BlockProcessing;
using dts.server.Commons;
using dts.server.Reader;

namespace dts.server.PersonModel
{
    public class PersonReader : DelimitedFileReaderBase
    {
        public PersonReader(string filePath, BlockingCollection<Block> outputQueue) : base(filePath, outputQueue)
        {
        }

        protected override IRowRecord CreateRowRecord(string[] attributes)
        {
            return new Person
                       {
                           Id = attributes[(int)PersonFields.Id],
                           FirstName = attributes[(int)PersonFields.FirstName],
                           MiddleName = attributes[(int)PersonFields.MiddleName],
                           LastName = attributes[(int)PersonFields.LastName],
                           Address = attributes[(int)PersonFields.Address],
                           Age = Convert.ToInt32(attributes[(int)PersonFields.Age]),
                           FatherName = attributes[(int)PersonFields.FatherName],
                           MotherName = attributes[(int)PersonFields.MotherName],
                           OfficeName = attributes[(int)PersonFields.OfficeName],
                           OfficeAddress = attributes[(int)PersonFields.OfficeAddress],
                       };
        }
    }
}
