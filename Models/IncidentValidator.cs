
namespace WebApplication2.Models;

public static class IncidentValidator
{
    public static bool IsValidSeverity(string? severity)
    {
        return severity is "Low" or "Medium" or "High";
    }
}
