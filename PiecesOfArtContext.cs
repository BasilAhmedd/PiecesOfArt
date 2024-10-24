using Microsoft.EntityFrameworkCore;
using PiecesArt.Models;

namespace PiecesArt
{
    public class PiecesOfArtContext : DbContext
    {
        public PiecesOfArtContext(DbContextOptions<PiecesOfArtContext> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<PieceOfArt> PiecesOfArt { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<LoyaltyCard> LoyaltyCards { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Defining the relationships
            modelBuilder.Entity<Users>()
                .HasOne(u => u.LoyaltyCard)
                .WithMany(lc => lc.Users)
                .HasForeignKey(u => u.LoyaltyCardId);

            modelBuilder.Entity<PieceOfArt>()
                .HasOne(p => p.Category)
                .WithMany(c => c.PiecesOfArt)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Users>()
       .HasMany(u => u.PiecesOfArt)
       .WithOne()
       .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>().HasData(
                 new Category { Id = 1, Name = "Impressionism", Description = "A 19th-century art movement..." },
                 new Category { Id = 2, Name = "Renaissance", Description = "A period in European history..." },
                 new Category { Id = 3, Name = "Abstract", Description = "Art that uses shapes, colors..." },
                 new Category { Id = 4, Name = "Modern", Description = "Artistic work from late 19th to mid-20th century..." },
                 new Category { Id = 5, Name = "Ancient", Description = "Art from ancient civilizations..." }
             );

            modelBuilder.Entity<LoyaltyCard>().HasData(
                new LoyaltyCard { Id = 1, Name = "Bronze", Description = "10% Off" },
                new LoyaltyCard { Id = 2, Name = "Silver", Description = "20% Off" },
                new LoyaltyCard { Id = 3, Name = "Gold", Description = "30% Off" },
                new LoyaltyCard { Id = 4, Name = "Platinum", Description = "40% Off" },
                new LoyaltyCard { Id = 5, Name = "Crystal", Description = "50% Off" }
            );
        }
    }
}
