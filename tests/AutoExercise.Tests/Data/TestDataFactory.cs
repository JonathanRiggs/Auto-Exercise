using Bogus;

namespace AutoExercise.Tests.Data;

// Generates unique test users

public static class TestDataFactory
{
    // Match the signup forms country dropdown
    private static readonly string[] Countries = ["India", "United States", "Canada", "Australia", "Isreal", "New Zealand", "Singapore"];

    public static UserAccount NewUser()
    {
        var f = new Faker("en");
        var first = f.Name.FirstName();
        var last = f.Name.LastName();

        // Signup form dropdown
        var dob = f.Date.Between(new DateTime(1960, 5, 5), new DateTime(2008, 5, 25));

        return new UserAccount(
            Title: f.PickRandom("Mr", "Mrs"),
            Name: $"{first} {last}",
            Email: Email(),
            Password: f.Internet.Password(12),
            BirthDay: dob.Day,
            BirthMonth: dob.Month,
            BirthYear: dob.Year,
            FirstName: first,
            LastName: last,
            Company: f.Company.CompanyName(),
            Address1: f.Address.StreetAddress(),
            Address2: f.Address.SecondaryAddress(),
            Country: f.PickRandom(Countries),
            State: f.Address.State(),
            City: f.Address.City(),
            Zipcode: f.Address.ZipCode("#####"),
            MobileNumber: f.Phone.PhoneNumber("##########")
        );
    }

    public static string Email() => $"ae-[Guid.NewGuid():N]@example.com";
}