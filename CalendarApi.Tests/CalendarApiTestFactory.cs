using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using CalendarApi.Data;

namespace CalendarApi.Tests;

public class CalendarApiTestFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Find and remove the application's original DbContext registration.
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<CalendarDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add a new DbContext registration using an in-memory database for testing.
            services.AddDbContext<CalendarDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryTestDb");
            });

            // Build a service provider to create the database schema.
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<CalendarDbContext>();
                db.Database.EnsureCreated(); // Create the schema for the in-memory database.
            }
        });
    }
}