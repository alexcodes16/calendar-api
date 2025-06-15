/*using System.Net;
using System.Net.Http.Json;
using CalendarApi.Models;

namespace CalendarApi.Tests;

// We now use our custom factory here instead of the default one.
public class EventsApiTests : IClassFixture<CalendarApiTestFactory>
{
    private readonly HttpClient _client;

    public EventsApiTests(CalendarApiTestFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostEvent_ThenGetEvent_ReturnsCorrectEvent()
    {
        // Arrange
        var newEvent = new Event
        {
            Title = "Integration Test Event",
            Description = "This will finally work!",
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddHours(1)
        };

        // Act
        var postResponse = await _client.PostAsJsonAsync("/api/v1/events", newEvent);

        // Assert
        postResponse.EnsureSuccessStatusCode();
        var createdEvent = await postResponse.Content.ReadFromJsonAsync<Event>();
        Assert.NotNull(createdEvent);

        var getResponse = await _client.GetAsync($"/api/v1/events/{createdEvent.Id}");
        getResponse.EnsureSuccessStatusCode();
        var fetchedEvent = await getResponse.Content.ReadFromJsonAsync<Event>();
        Assert.NotNull(fetchedEvent);
        Assert.Equal("Integration Test Event", fetchedEvent.Title);
    }
}*/ //
