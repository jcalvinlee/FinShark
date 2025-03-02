using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be more than 10 characters.")]
        public string Symbol { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Company name cannot be more than 20 characters.")]
        public string CompanyName { get; set; }

        [Required]
        [Range(1, 1000000000000000)]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0,100)]
        public decimal LastDiv { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Industry cannot be more than 20 characters.")]
        public string Industry { get; set; }

        [Required]
        [Range(1, 1000000000000000)]
        public long MarketCap { get; set; }
    }
}
