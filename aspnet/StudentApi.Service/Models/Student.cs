using System.Collections.Generic;
using StudentApi.Abstracts;

namespace StudentApi.Models
{
    public class Student : APerson
    {
        public short Age { get; set; }

        public Student(){}
    }
}
