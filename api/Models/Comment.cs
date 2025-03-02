using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Comments")]
    public class Comment
    {
        public Comment()
        {
            CreatedOn = DateTime.Now;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        // Foreign key property
        public int? StockId { get; set; }

        // Navigation property
        public Stock? Stock { get; set; }

        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }
    }
}
