namespace EquipmentRentalSystem.Models;

using EquipmentRentalSystem.Enum;
public abstract class Equipment
{
    private static int _nextId = 1;

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public EquipmentStatus Status { get; set; } = EquipmentStatus.Available;
    public string Description { get; set; } = null!;
    public DateTime AddedDate { get; set; }

    public Equipment(string name, string description = "")
    {
        Id = _nextId++;
        Name = name;
        Description = description;
        AddedDate = DateTime.Now;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Status: {Status}, Description: {Description}, Added Date: {AddedDate}";
    }
}
 