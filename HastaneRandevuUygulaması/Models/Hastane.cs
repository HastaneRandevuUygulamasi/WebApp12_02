using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HastaneRandevuUygulaması.Models
{
    public class Hastane
    {
        [Key]
        public int hastaneId { get; set; }

        public string hastaneAdi { get; set; }

        [ForeignKey("ilce")]
        public int ilceId { get; set; }

        public ilce ilce { get; set; }
        public List<Poliklinik>? Poliklinikler { get; set; }
    }
}
