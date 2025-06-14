using CalendarApi.Services;

namespace CalendarApi.Tests;

public class SchedulingServiceTests{
    // ===  FIRST TEST ===
    [Fact]
    public void FindAvailableSlots_WhenOneParticipantIsBusy_ReturnsCorrectSlots(){
        // Arrange
        var service = new SchedulingService();
        var meetingDuration = TimeSpan.FromMinutes(30);
        var searchStart = new DateTime(2025, 6, 20, 9, 0, 0);
        var searchEnd = new DateTime(2025, 6, 20, 10, 30, 0);

        var participant1Schedule = new List<TimeSlot>
        {
            new(new DateTime(2025, 6, 20, 9, 30, 0), new DateTime(2025, 6, 20, 10, 0, 0))
        };
        var allSchedules = new List<List<TimeSlot>> { participant1Schedule };

        // Act
        var result = service.FindAvailableSlots(allSchedules, meetingDuration, searchStart, searchEnd);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(new TimeSlot(searchStart, searchStart.Add(meetingDuration)), result);
        Assert.Contains(new TimeSlot(new DateTime(2025, 6, 20, 10, 0, 0), new DateTime(2025, 6, 20, 10, 30, 0)), result);
    }

    // === YOUR NEW TEST ===
    [Fact]
    public void FindAvailableSlots_WithTwoParticipants_ReturnsCorrectSlots()
    {
        // Arrange
        var service = new SchedulingService();
        var meetingDuration = TimeSpan.FromMinutes(60); // 1 hour meeting
        var searchStart = new DateTime(2025, 6, 21, 9, 0, 0);  // 9:00 AM
        var searchEnd = new DateTime(2025, 6, 21, 12, 0, 0); // 12:00 PM

        // Alice is busy 9:00 - 10:00
        var aliceSchedule = new List<TimeSlot>
        {
            new(new DateTime(2025, 6, 21, 9, 0, 0), new DateTime(2025, 6, 21, 10, 0, 0))
        };

        // Bob is busy 10:30 - 11:00
        var bobSchedule = new List<TimeSlot>
        {
            new(new DateTime(2025, 6, 21, 10, 30, 0), new DateTime(2025, 6, 21, 11, 0, 0))
        };
    
        var allSchedules = new List<List<TimeSlot>> { aliceSchedule, bobSchedule };

        // Act
        var result = service.FindAvailableSlots(allSchedules, meetingDuration, searchStart, searchEnd);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result); // Should only find one 1-hour slot: 11:00-12:00
        Assert.Contains(new TimeSlot(new DateTime(2025, 6, 21, 11, 0, 0), new DateTime(2025, 6, 21, 12, 0, 0)), result);
    }
}