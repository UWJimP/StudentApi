using StudentApi.Abstracts;

namespace StudentApi.Models
{
    public class ClassReport : AEntity
    {
        public Classroom Classroom { get; set; }

        public double Grade { get; set; }
    }
}
