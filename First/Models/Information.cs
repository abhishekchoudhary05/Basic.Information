using System.ComponentModel.DataAnnotations;

namespace First.Models
{
    public class Information
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Appointment { get; set; }
        public string Technology { get; set; }
        public string Task { get; set; }
    }
}
