using Microsoft.AspNetCore.Mvc;
using CalendarApi.Data;
using CalendarApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CalendarApi.Controllers;

[ApiController]
[Route("api/v1/events")]
public class EventsController : ControllerBase
{
    private readonly CalendarDbContext _context;
    private readonly ILogger<EventsController> _logger; 

    // Modify the constructor to accept a logger
    public EventsController(CalendarDbContext context, ILogger<EventsController> logger){
        _context = context;
        _logger = logger;
    }
    // GET: api/v1/events
    [HttpGet]
    public async Task<ActionResult<object>> GetEvents(){
        var events = await _context.Events.Include(e => e.Participants).ToListAsync();
        // DEBUG MESSAGE
        _logger.LogInformation($"DATABASE CHECK: Found {events.Count} events to return.");

        return new { Events = events };
    }

    // POST /api/v1/events
    [HttpPost]
    public async Task<IActionResult> AddEvent([FromBody] Event newEvent){
        if (newEvent==null){return BadRequest();}
        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetEventById), new { id = newEvent.Id }, newEvent);
    }

    // GET /api/v1/events/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(int id){
         var anEvent = await _context.Events.Include(e => e.Participants).FirstOrDefaultAsync(e => e.Id == id);
         if (anEvent==null){return NotFound();}
         return Ok(anEvent);
    }

    // PUT: api/v1/events/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(int id, Event updatedEvent){
        if (id != updatedEvent.Id){return BadRequest("ID in URL does not match ID in body.");}
        var existingEvent = await _context.Events.FindAsync(id);
        if (existingEvent==null){return NotFound();}
        existingEvent.Title = updatedEvent.Title;
        existingEvent.Description = updatedEvent.Description;
        existingEvent.StartTime = updatedEvent.StartTime;
        existingEvent.EndTime = updatedEvent.EndTime;
        try{
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException){
            if (!_context.Events.Any(e => e.Id == id)){
                return NotFound();}
            else{throw;}
        }
        return NoContent();
    }

    // DELETE: api/v1/events/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id){
        var anEvent = await _context.Events.FindAsync(id);
        if (anEvent == null){return NotFound();}
        _context.Events.Remove(anEvent);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    // POST /api/v1/events/{eventId}/participants
    [HttpPost("{eventId}/participants")]
    public async Task<IActionResult> AddParticipant(int eventId, [FromBody] int userId){
        var anEvent = await _context.Events
            .Include(e => e.Participants)
            .FirstOrDefaultAsync(e => e.Id == eventId);
        if (anEvent==null){return NotFound("Event not found.");}
        var user = await _context.Users.FindAsync(userId);
        if (user==null){return NotFound("User not found.");}
        anEvent.Participants.Add(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}