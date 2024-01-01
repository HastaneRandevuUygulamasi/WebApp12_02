using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HastaneRandevuUygulaması.Models
{
    public class ilce
    {
        [Key]
        public int ilceID { get; set; }
        public string ilceName { get; set; }

        [ForeignKey("il")]
        public int ilID { get; set; }
        public il il { get; set; }
        public List<Hastane>? Hastaneler { get; set; }
    }
}
