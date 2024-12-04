using System.ComponentModel.DataAnnotations;

namespace WareHause.Models
{
    public class MIlkProduct
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public DateTime Endtime { get; set; }

    }
}
