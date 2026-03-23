namespace EquipmentRentalSystem.Models;

using EquipmentRentalSystem.Enum;

public class User
{
    private static int _nextId = 1;

    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public UserType UserType { get; set; }
    public string? StudentNumber { get; set; }
    public string? Department { get; set; }

    public User(string firstName, string lastName, UserType userType)
    {
        Id = _nextId++;
        FirstName = firstName;
        LastName = lastName;
        UserType = userType;
    }

    public string GetFullName()
    {
        return FirstName + " " + LastName;
    }

    public int GetMaxRentals()
    {
        if (UserType == UserType.Student)
            return 2;
        else
            return 5;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {GetFullName()}, Type: {UserType}";
    }
}
