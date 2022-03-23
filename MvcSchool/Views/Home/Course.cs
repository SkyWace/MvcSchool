using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcSchool.Views.Home
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
