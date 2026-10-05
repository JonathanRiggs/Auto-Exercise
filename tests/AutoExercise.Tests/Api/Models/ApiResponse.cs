using System.Text.Json.Serialization;

namespace AutoExercise.Tests.Api.Models;

// Common response shape
public record ApiResponse(
    [property: JsonPropertyName("responseCode")] int ResponseCode,
    [property: JsonPropertyName("message")] string? Message);