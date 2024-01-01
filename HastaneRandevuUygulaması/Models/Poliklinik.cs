using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HastaneRandevuUygulaması.Models
{
    public class Poliklinik
    {
        [Key]
        public int poliklinikId { get; set; }

        public string poliklinikName { get; set; }

        [ForeignKey("hastane")]
        public int hastaneId { get; set; }
        public Hastane hastane { get; set; }

        public List<Doktor>? Doktorlar { get; set; }

    }
}
