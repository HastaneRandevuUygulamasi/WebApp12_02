using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HastaneRandevuUygulaması.Models
{
    public class Doktor
    {
        [Key]
        public int doktorId { get; set; }

        public string doktorName { get; set; }

        [ForeignKey("poliklinik")]
        public int poliklinikId { get; set; }
        public Poliklinik poliklinik { get; set; }



    }
}
