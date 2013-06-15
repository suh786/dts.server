using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace dts.server.Commons
{
    [DataContract]
    public class Person
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string OfficeName { get; set; }
        public string OfficeAddress { get; set; }

        public override string ToString()
        {

            return string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}",
                                 ",",
                                 Id,
                                 FirstName,
                                 MiddleName,
                                 LastName,
                                 Address,
                                 Age,
                                 FatherName,
                                 MotherName,
                                 OfficeName,
                                 OfficeAddress);
        }
    }
}
