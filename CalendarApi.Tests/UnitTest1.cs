using CalendarApi.Services;

namespace CalendarApi.Tests;

public class SchedulingServiceTests
{
    [Fact] // This marks the method as a test
    public void FindAvailableSlots_WhenOneParticipantIsBusy_ReturnsCorrectSlots(){
        // Arrange (Set up the test)
        var service= new SchedulingService();
        var meetingDuration= TimeSpan.FromMinutes(30);
        var searchStart= new DateTime(2025, 6, 20, 9, 0, 0); // 9:00 AM
        var searchEnd= new DateTime(2025, 6, 20, 10, 30, 0); // 10:30 AM

        var participant1Schedule = new List<TimeSlot>{
            new(new DateTime(2025, 6, 20, 9, 30, 0), new DateTime(2025, 6, 20, 10, 0, 0)) // Busy 9:30-10:00
        };
        var allSchedules = new List<List<TimeSlot>> { participant1Schedule };
        // Act (Run the method you are testing)
        var result = service.FindAvailableSlots(allSchedules, meetingDuration, searchStart, searchEnd);
        // Assert (Check if the result is correct)
        Assert.NotNull(result);
        Assert.Equal(2, result.Count); // Should find two slots: 9:00-9:30 and 10:00-10:30
        Assert.Contains(new TimeSlot(searchStart, searchStart.Add(meetingDuration)), result);
        Assert.Contains(new TimeSlot(new DateTime(2025, 6, 20, 10, 0, 0), new DateTime(2025, 6, 20, 10, 30, 0)), result);
    }
}