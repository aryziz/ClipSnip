using System.ComponentModel.DataAnnotations;

namespace ClipSnip.Models;

public class Appointment
{
    [Key]
    public int UniqueId { get; set; }

    public DateTime TimeOfAppointment { get; set; }

    public int DurationInMinutes { get; set; }

    [MaxLength(450)]
    public string UserId { get; set; } = null!;

    public string Hairstyle { get; set; }

    public ApplicationUser ApplicationUser { get; set; } = null!;
}
