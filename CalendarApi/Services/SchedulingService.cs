namespace CalendarApi.Services;

// This defines the simple TimeSlot object we will work with.
public record TimeSlot(DateTime StartTime, DateTime EndTime);

public class SchedulingService{
    public List<TimeSlot> FindAvailableSlots(
        List<List<TimeSlot>> allParticipantSchedules,
        TimeSpan meetingDuration,
        DateTime searchRangeStart,
        DateTime searchRangeEnd)
    {
        // === STEP 1: Flatten all busy slots into one list and sort them. ===
        var allBusySlots = allParticipantSchedules
            .SelectMany(schedule => schedule) // Combines the list of lists into a single list
            .OrderBy(slot => slot.StartTime)    // Sorts all busy slots by when they start
            .ToList();

        // === STEP 2: Merge any overlapping or adjacent busy slots. ===
        var mergedBusySlots = new List<TimeSlot>();
        if (allBusySlots.Any())
        {
            var currentSlot = allBusySlots.First();

            for (int i = 1; i < allBusySlots.Count; i++)
            {
                var nextSlot = allBusySlots[i];
                // If the next busy slot starts before or exactly when the current one ends, they overlap.
                if (nextSlot.StartTime <= currentSlot.EndTime){
                    // Merge them by creating a new slot that spans from the earliest start to the latest end time.
                    var newEndTime = nextSlot.EndTime > currentSlot.EndTime ? nextSlot.EndTime : currentSlot.EndTime;
                    currentSlot = new TimeSlot(currentSlot.StartTime, newEndTime);
                }
                else{
                    // No overlap, so the current merged slot is complete. Add it to the list.
                    mergedBusySlots.Add(currentSlot);
                    // The next slot becomes the new starting point for our merge.
                    currentSlot=nextSlot;
                }
            }
            // Add the very last slot to the list.
            mergedBusySlots.Add(currentSlot);
        }
        // === STEP 3: Find the free gaps between the merged busy slots. ===
        var availableSlots = new List<TimeSlot>();
        // The first potential free slot starts at the beginning of our search range.
        var lastBusySlotEndTime = searchRangeStart;
        foreach (var busySlot in mergedBusySlots){
            // The gap is the time between the end of the last busy period and the start of the next one.
            var freeSlot = new TimeSlot(lastBusySlotEndTime, busySlot.StartTime);
            // === STEP 4: Check if the gap is long enough for the meeting. ===
            if (freeSlot.EndTime - freeSlot.StartTime >= meetingDuration){availableSlots.Add(freeSlot);}
            // Move our pointer to the end of the current busy period.
            lastBusySlotEndTime = busySlot.EndTime;
        }
        // Check for a final free slot between the end of the last busy period and the end of our search range.
        var finalSlot = new TimeSlot(lastBusySlotEndTime, searchRangeEnd);
        if (finalSlot.EndTime - finalSlot.StartTime >= meetingDuration){availableSlots.Add(finalSlot);}
        return availableSlots;
    }
}