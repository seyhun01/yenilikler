using System.ComponentModel.DataAnnotations;

namespace WareHause.Models
{
    public class Chocolate
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public DateTime Endtime { get; set; }

        internal async Task AddAsync(Chocolate chocolate)
        {
            throw new NotImplementedException();
        }

        internal async Task<bool> AnyAsync(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
