namespace api.Models
{
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

        public int? StockId { get; set; }

        // This is a navigation property
        public Stock? Stock { get; set; }
    }
}
