using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuUygulaması.Models
{
    public class il
    {
        [Key]
        public int ilId { get; set; }
        public string ilAdi { get; set; }

        public List<ilce>? ilceler { get; set; }

    }

}
