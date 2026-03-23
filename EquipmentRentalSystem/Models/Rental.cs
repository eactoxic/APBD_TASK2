namespace EquipmentRentalSystem.Models;

using EquipmentRentalSystem.Enum;

public class Rental
{
    private static int _nextId = 1;

    public int Id { get; set; }
    public User User { get; set; } = null!;
    public Equipment Equipment { get; set; } = null!;
    public DateTime RentalDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal Penalty { get; set; }

    public bool IsActive { get; set; }

    public Rental(User user, Equipment equipment, int rentalDays)
    {
        Id = _nextId++;
        User = user;
        Equipment = equipment;
        RentalDate = DateTime.Now;
        DueDate = RentalDate.AddDays(rentalDays);
        Penalty = 0;
        IsActive = true;
    }

    public void Return(DateTime returnDate)
    {
        ReturnDate = returnDate;
        IsActive = false;
        Equipment.Status = EquipmentStatus.Available;

        if (returnDate > DueDate)
        {
            int daysLate = (int)Math.Ceiling((returnDate - DueDate).TotalDays);
            Penalty = daysLate * 5.00m;
        }
    }

    public override string ToString()
    {
        return $"Id: {Id}, User: {User.GetFullName()}, Equipment: {Equipment.Name}, Due: {DueDate:dd.MM.yyyy}, Active: {IsActive}, Penalty: {Penalty}";
    }
}