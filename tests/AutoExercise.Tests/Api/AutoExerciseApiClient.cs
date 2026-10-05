using System.Text.Json;
using AutoExercise.Tests.Api.Models;
using AutoExercise.Tests.Data;
using Microsoft.Playwright;

namespace AutoExercise.Tests.Api;

// Wrapper around account endpoints used for setup and cleanup

public class AutomationExerciseApiClient(IAPIRequestContext request)
{
    public Task<ApiResponse> CreateAccountAsync(UserAccount user) => 
        SendFormAsync("POST", "/api/createAccount", new()
        {
           ["name"] = user.Name,
           ["email"] = user.Email,
           ["password"] = user.Password,
           ["title"] = user.Title,
           ["birth_date"] = user.BirthDay.ToString(),
           ["birth_month"] = user.BirthMonth.ToString(),
           ["birth_year"] = user.BirthYear.ToString(),
           ["firstname"] = user.FirstName,
           ["lastname"] = user.LastName,
           ["company"] = user.Company,
           ["address1"] = user.Address1,
           ["address2"] = user.Address2,
           ["country"] = user.Country,
           ["zipcode"] = user.Zipcode,
           ["state"] = user.State,
           ["city"] = user.City,
           ["mobile_number"] = user.MobileNumber,
        });

    public Task<ApiResponse> DeleteAccountAsync(string email, string password) =>
        SendFormAsync("DELETE", "/api/deleteAccount", new()
        {
           ["email"] = email,
           ["password"] = password, 
        });

    public Task<ApiResponse> VerifyLoginAsync(string email, string password) => 
        SendFormAsync("POST", "/api/verifyLogin", new()
        {
            ["email"] = email,
            ["password"] = password,
        });

    private async Task<ApiResponse> SendFormAsync(string method, string path, Dictionary<string, string> fields)
    {
        var body = string.Join("&", fields.Select(kv => 
            $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value)}"
        ));

        var response = await request.FetchAsync(path, new()
        {
            Method = method,
            Headers = new Dictionary<string, string>
            {
                ["Content-Type"] = "application/x-www-form-urlencoded"
            },
            DataString = body,
        });

        var text = await response.TextAsync();
        return JsonSerializer.Deserialize<ApiResponse>(text) ?? throw new InvalidOperationException($"Unparseable response from {method} {path}: {text}");
    }
}
