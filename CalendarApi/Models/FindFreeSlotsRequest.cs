namespace CalendarApi.Models;

public class FindFreeSlotsRequest{
    public List<int> ParticipantIds {get; set; }=new();
    public int MeetingDurationMinutes {get; set;}
    public DateTime SearchRangeStart {get; set;}
    public DateTime SearchRangeEnd {get; set;}
}
