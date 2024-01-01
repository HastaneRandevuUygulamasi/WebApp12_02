using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuUygulaması.Models
{
    public class Hasta
    {
        [Key]
        public int hastaId { get; set; }
        [Required]
        [Display(Name = "Ad")]
        public string? hastaName { get; set; }
        [Required]
        [Display(Name = "Soyad")]
        public string? hastaLastName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        public string? tc { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        public DateTime? dogumTarihi { get; set; }

        public List<Randevu>? randevular { get; set; }



    }
}
