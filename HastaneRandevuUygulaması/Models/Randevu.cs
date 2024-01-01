using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HastaneRandevuUygulaması.Models
{
    public class Randevu
    {
        [Key]
        public int randevuId { get; set; }
        public DateTime randevuTarihi { get; set; }

        [ForeignKey("doktor")]
        public int doktorId { get; set; }
        public Doktor doktor { get; set; }


        [ForeignKey("hasta")]
        public int hastaId { get; set; }
        public Hasta hasta { get; set; }



    }
}
