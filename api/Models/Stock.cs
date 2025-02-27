using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Stock
    {
        private readonly List<Comment> _comments = new List<Comment>();

        public int Id { get; set; }

        public string Symbol { get; set; }

        public string CompanyName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Purchase { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDiv { get; set; }

        public string Industry { get; set; }

        public long MarketCap { get; set; }

        public List<Comment> Comments
        {
            get
            {
                return _comments;
            }
        }
    }
}