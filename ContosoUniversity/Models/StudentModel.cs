using System;
using System.Collections.Generic;

namespace ContosoUniversity.Models
{
    public class StudentModel
    {

        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public ICollection<EnrollmentModel> Enrollments { get; set; }
    }
}