namespace PiecesArt.Models
{
    public class PieceOfArt
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime PublicationDate { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
