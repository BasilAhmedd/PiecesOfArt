namespace PiecesArt.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }

        public int LoyaltyCardId { get; set; }
        public LoyaltyCard LoyaltyCard { get; set; }

        public List<PieceOfArt> PiecesOfArt { get; set; }
    }
}
