﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class CourseModel
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

       
    }
}