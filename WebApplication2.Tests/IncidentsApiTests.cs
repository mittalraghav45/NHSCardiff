using WebApplication2.Models;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebApplication2.Data;
using Xunit;

namespace WebApplication2.Tests;

public class IncidentsApiTests
{
    // Create a test server with an isolated in-memory database.
  
private static WebApplicationFactory<Program> CreateTestFactory()
{
    // Generate one unique database name per test factory.
    string databaseName = Guid.NewGuid().ToString();

    return new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Remove production SQL Server configuration.
                services.RemoveAll<ApplicationDbContext>();

                services.RemoveAll<
                    DbContextOptions<ApplicationDbContext>>();

                services.RemoveAll<
                    IDbContextOptionsConfiguration<ApplicationDbContext>>();

                // Reuse the same database name for this test.
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase(databaseName));
            });
        });
}

    [Fact]
    public async Task CreateIncident_WithInvalidSeverity_ReturnsBadRequest()
    {
        // Arrange
        using var factory = CreateTestFactory();
        using var client = factory.CreateClient();

        var invalidIncident = new
        {
            Title = "Integration Test",
            Location = "Cardiff",
            Severity = "Critical"
        };

        // Act
        var response = await client.PostAsJsonAsync(
            "/api/incidents",
            invalidIncident);

        // Assert
        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);
    }

    [Fact]
    public async Task CreateIncident_WithValidSeverity_ReturnsCreated()
    {
        // Arrange
        using var factory = CreateTestFactory();
        using var client = factory.CreateClient();

        var validIncident = new
        {
            Title = "Food Safety Investigation",
            Location = "Cardiff",
            Severity = "High"
        };

        // Act
        var response = await client.PostAsJsonAsync(
            "/api/incidents",
            validIncident);

        var responseBody =
            await response.Content.ReadAsStringAsync();

        // Assert
        Assert.True(
            response.StatusCode == HttpStatusCode.Created,
            $"Expected HTTP 201, received {(int)response.StatusCode}. " +
            $"Response: {responseBody}");
    }


[Fact]
public async Task CreateIncident_ThenGetIncidents_ReturnsSavedIncident()
{
    // Arrange
    using var factory = CreateTestFactory();
    using var client = factory.CreateClient();

    var newIncident = new
    {
        Title = "Water Quality Investigation",
        Location = "Swansea",
        Severity = "Medium"
    };

    // Act 1: Create the incident
    var postResponse = await client.PostAsJsonAsync(
        "/api/incidents",
        newIncident);

    Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

    // Act 2: Retrieve incidents using GET
    var incidents = await client.GetFromJsonAsync<List<Incident>>(
        "/api/incidents");

    // Assert
    Assert.NotNull(incidents);

    var savedIncident = Assert.Single(incidents);

    Assert.Equal("Water Quality Investigation", savedIncident.Title);
    Assert.Equal("Swansea", savedIncident.Location);
    Assert.Equal("Medium", savedIncident.Severity);
    Assert.True(savedIncident.Id > 0);
}

}
