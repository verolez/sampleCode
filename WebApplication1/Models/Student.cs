namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        public Student()
        {
            isDeleted = false;
        }
        public int ID { get; set; }

        [StringLength(150)]
        public string StudentName { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        public string Address { get; set; }

        public bool? isDeleted { get; set; }
    }
}
