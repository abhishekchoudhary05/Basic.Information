using System;
using System.ComponentModel.DataAnnotations;

namespace First.Models
{
    public class Information
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public Decimal Salary { get; set; }
        public DateTime JoinedOn { get; set; }
    }
}
