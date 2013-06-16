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
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string MiddleName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public string FatherName { get; set; }
        [DataMember]
        public string MotherName { get; set; }
        [DataMember]
        public string OfficeName { get; set; }
        [DataMember]
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
