namespace EquipmentRentalSystem.Interfaces;

using EquipmentRentalSystem.Models;

public interface IRentalService
{
    void AddEquipment(Equipment equipment);
    List<Equipment> GetAllEquipments();
    List<Equipment> GetAvailableEquipments();
    void MarkAsUnavailable(Equipment equipment);

    void AddUser(User user);
    List<User> GetAllUsers();

    Rental RentEquipment(User user, Equipment equipment, int days);
    Rental ReturnEquipment(Equipment equipment);
    List<Rental> GetActiveRentalsForUser(User user);
    List<Rental> GetOverdueRentals();

    void PrintReport();
}