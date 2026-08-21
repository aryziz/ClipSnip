using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ClipSnip.Models
{
    public class Appointment
    {
        [Key]
        public int UniqueId { get; set; }
        public DateTime TimeOfAppointment { get; set; }
        public int DurationInMinutes {  get; set; }
        public string UserId { get; set; }
    }
}
