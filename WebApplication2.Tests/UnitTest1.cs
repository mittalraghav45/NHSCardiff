
using Xunit;
using WebApplication2.Models;

namespace WebApplication2.Tests;

public class IncidentTests
{
    [Fact]
    public void NewIncident_ShouldStoreCorrectTitle()
    {
        // Arrange
        var incident = new Incident
        {
            Title = "Food Poisoning Investigation",
            Location = "Cardiff",
            Severity = "High"
        };

        // Act
        string actualTitle = incident.Title;

        // Assert
        Assert.Equal("Food Poisoning Investigation", actualTitle);
    }


[Theory]
[InlineData("Low")]
[InlineData("Medium")]
[InlineData("High")]
public void Incident_ShouldStoreSelectedSeverity(string severity)
{
    // Arrange
    var incident = new Incident
    {
        Title = "Health Investigation",
        Location = "Cardiff",
        Severity = severity
    };

    // Act
    string actualSeverity = incident.Severity;

    // Assert
    Assert.Equal(severity, actualSeverity);
}



[Theory]
[InlineData("Low", true)]
[InlineData("Medium", true)]
[InlineData("High", true)]
[InlineData("Critical", false)]
[InlineData("", false)]
[InlineData(null, false)]
public void SeverityValidation_ShouldReturnExpectedResult(
    string? severity,
    bool expected)
{
    // Act
    bool actual = IncidentValidator.IsValidSeverity(severity);

    // Assert
    Assert.Equal(expected, actual);
}

}
