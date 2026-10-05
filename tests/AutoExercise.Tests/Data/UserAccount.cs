namespace AutoExercise.Tests.Data;

// Everything needed to register a user through UI signup
public record UserAccount(
    string Title,
    string Name,
    string Email,
    string Password,
    int BirthDay,
    int BirthMonth,
    int BirthYear,
    string FirstName,
    string LastName,
    string Company,
    string Address1,
    string Address2,
    string Country,
    string State,
    string City,
    string Zipcode,
    string MobileNumber);