namespace EquipmentRentalSystem.Services;

using EquipmentRentalSystem.Database;
using EquipmentRentalSystem.Enum;
using EquipmentRentalSystem.Interfaces;
using EquipmentRentalSystem.Models;

public class RentalService : IRentalService
{
    private Singleton _db = Singleton.GetInstance();

    public void AddEquipment(Equipment equipment)
    {
        _db.Equipments.Add(equipment);
    }

    public List<Equipment> GetAllEquipments()
    {
        return _db.Equipments;
    }

    public List<Equipment> GetAvailableEquipments()
    {
        List<Equipment> available = new List<Equipment>();
        foreach (var e in _db.Equipments)
        {
            if (e.Status == EquipmentStatus.Available)
                available.Add(e);
        }
        return available;
    }

    public void MarkAsUnavailable(Equipment equipment)
    {
        equipment.Status = EquipmentStatus.Unavailable;
    }

    public void AddUser(User user)
    {
        _db.Users.Add(user);
    }

    public List<User> GetAllUsers()
    {
        return _db.Users;
    }

    public Rental RentEquipment(User user, Equipment equipment, int days)
    {
        if (equipment.Status != EquipmentStatus.Available)
        {
            throw new Exception($"{equipment.Name} is not available.");
        }

        int activeCount = 0;
        foreach (var r in _db.Rentals)
        {
            if (r.User.Id == user.Id && r.IsActive)
                activeCount++;
        }

        if (activeCount >= user.GetMaxRentals())
        {
            throw new Exception($"{user.GetFullName()} has reached the rental limit of {user.GetMaxRentals()}.");
        }

        Rental rental = new Rental(user, equipment, days);
        equipment.Status = EquipmentStatus.Rented;
        _db.Rentals.Add(rental);
        return rental;
    }

    public Rental ReturnEquipment(Equipment equipment)
    {
        Rental? rental = null;
        foreach (var r in _db.Rentals)
        {
            if (r.Equipment.Id == equipment.Id && r.IsActive)
            {
                rental = r;
                break;
            }
        }

        if (rental == null)
        {
            throw new Exception($"No active rental found for {equipment.Name}.");
        }

        rental.Return(DateTime.Now);
        return rental;
    }

    public Rental ReturnEquipmentWithDate(Equipment equipment, DateTime returnDate)
    {
        Rental? rental = null;
        foreach (var r in _db.Rentals)
        {
            if (r.Equipment.Id == equipment.Id && r.IsActive)
            {
                rental = r;
                break;
            }
        }

        if (rental == null)
        {
            throw new Exception($"No active rental found for {equipment.Name}.");
        }

        rental.Return(returnDate);
        return rental;
    }

    public List<Rental> GetActiveRentalsForUser(User user)
    {
        List<Rental> result = new List<Rental>();
        foreach (var r in _db.Rentals)
        {
            if (r.User.Id == user.Id && r.IsActive)
                result.Add(r);
        }
        return result;
    }

    public List<Rental> GetOverdueRentals()
    {
        List<Rental> result = new List<Rental>();
        foreach (var r in _db.Rentals)
        {
            if (r.IsActive && DateTime.Now > r.DueDate)
                result.Add(r);
        }
        return result;
    }

    public void PrintReport()
    {
        int available = 0;
        int rented = 0;
        int unavailable = 0;
        int activeRentals = 0;
        int overdueRentals = 0;
        decimal totalPenalty = 0;

        foreach (var e in _db.Equipments)
        {
            if (e.Status == EquipmentStatus.Available) available++;
            else if (e.Status == EquipmentStatus.Rented) rented++;
            else unavailable++;
        }

        foreach (var r in _db.Rentals)
        {
            if (r.IsActive) activeRentals++;
            if (r.IsActive && DateTime.Now > r.DueDate) overdueRentals++;
            totalPenalty += r.Penalty;
        }

        Console.WriteLine("=== REPORT ===");
        Console.WriteLine($"Total equipment: {_db.Equipments.Count}");
        Console.WriteLine($"Available: {available}");
        Console.WriteLine($"Rented: {rented}");
        Console.WriteLine($"Unavailable: {unavailable}");
        Console.WriteLine($"Total users: {_db.Users.Count}");
        Console.WriteLine($"Active rentals: {activeRentals}");
        Console.WriteLine($"Overdue rentals: {overdueRentals}");
        Console.WriteLine($"Total penalties: {totalPenalty}");
        Console.WriteLine("==============");
    }
}
