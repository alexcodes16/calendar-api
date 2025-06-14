using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CalendarApi.Data;
using CalendarApi.Models;
using CalendarApi.Services;

namespace CalendarApi.Controllers;

[ApiController]
[Route("api/v1/schedule")]
public class SchedulingController : ControllerBase{
    private readonly SchedulingService _schedulingService;
    private readonly CalendarDbContext _context;
    public SchedulingController(SchedulingService schedulingService, CalendarDbContext context){
        _schedulingService=schedulingService;
        _context=context;
    }
    [HttpPost("find-free-slots")]
    public async Task<ActionResult<List<TimeSlot>>> FindFreeSlots([FromBody] FindFreeSlotsRequest request){
        // Step 1: Get all events for the requested participants within the time range
        var relevantEvents = await _context.Events
            .Where(e => e.Participants.Any(p => request.ParticipantIds.Contains(p.Id)))
            .Where(e => e.StartTime < request.SearchRangeEnd && e.EndTime > request.SearchRangeStart)
            .ToListAsync();

        // Step 2: Create the list of schedules for the algorithm
        var allSchedules = new List<List<TimeSlot>>();
        foreach (var userId in request.ParticipantIds){
            var userSchedule = relevantEvents
                .Where(e => e.Participants.Any(p => p.Id == userId))
                .Select(e => new TimeSlot(e.StartTime, e.EndTime))
                .ToList();
            allSchedules.Add(userSchedule);
        }
        var meetingDuration = TimeSpan.FromMinutes(request.MeetingDurationMinutes);
        // Step 3: Call your algorithm!
        var availableSlots = _schedulingService.FindAvailableSlots(
            allSchedules,
            meetingDuration,
            request.SearchRangeStart,
            request.SearchRangeEnd);

        return Ok(availableSlots);
    }
}