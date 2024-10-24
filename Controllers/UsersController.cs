using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PiecesArt.Models;

namespace PiecesArt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly PiecesOfArtContext _context;

        public UsersController(PiecesOfArtContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.Include(u => u.LoyaltyCard)
                                       .Include(u => u.PiecesOfArt)
                                       .ThenInclude(p => p.Category)
            .ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, Users updatedUser)
        {
            if (id != updatedUser.Id) return BadRequest();

            _context.Entry(updatedUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("updateSpecificUser")]
        public async Task<IActionResult> UpdateSpecificUser()
        {
            var user = await _context.Users.Include(u => u.PiecesOfArt)
                                           .FirstOrDefaultAsync(u => u.Id == 4);
            if (user == null) return NotFound();

            user.Name = "SEWEDY";
            user.Age = 100;

            var smallPyramid = await _context.PiecesOfArt.FirstOrDefaultAsync(p => p.Title == "Small Pyramid");
            if (smallPyramid != null)
            {
                user.PiecesOfArt.Add(smallPyramid);
            }

            var crystalCard = await _context.LoyaltyCards.FirstOrDefaultAsync(lc => lc.Name == "Crystal");
            user.LoyaltyCardId = crystalCard.Id;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("deleteSecondUser")]
        public async Task<IActionResult> DeleteSecondUser()
        {
            var secondUser = await _context.Users.OrderBy(u => u.Id).Skip(1).FirstOrDefaultAsync();
            if (secondUser == null) return NotFound();

            _context.Users.Remove(secondUser);

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
