using System.Collections.Generic;
using StudentApi.Abstracts;

namespace StudentApi.Models
{
    public class Classroom : AEntity
    {
        public string ClassCode { get; set; }

        public string Description { get; set; }

        public List<Student> Students { get; set; }

        public Classroom()
        {
            Students = new List<Student>();
        }
    }
}
