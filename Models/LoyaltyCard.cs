namespace PiecesArt.Models
{
    public class LoyaltyCard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Users> Users { get; set; }
    }
}
