using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CalendarApi.Data;
using CalendarApi.Models;

namespace CalendarApi.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UsersController : ControllerBase{
    private readonly CalendarDbContext _context;
    public UsersController(CalendarDbContext context){
        _context = context;}
    // GET: /api/v1/users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers(){
        // We use .Include to also show the events each user is a part of.
        return await _context.Users.Include(u => u.Events).ToListAsync();
    }
    // POST: /api/v1/users
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user){
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        // This is simplified for now. We can add a GetUserById later.
        return CreatedAtAction(nameof(GetUsers), new { id=user.Id }, user);
    }
}