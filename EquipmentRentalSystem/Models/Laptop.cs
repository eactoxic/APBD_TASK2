namespace EquipmentRentalSystem.Models;

public class Laptop : Equipment
{
    public int RamGb { get; set; }
    public string Processor { get; set; } = null!;

    public Laptop(string name, string description, int ramGb, string processor)
        : base(name, description)
    {
        RamGb = ramGb;
        Processor = processor;
    }

    public override string ToString()
    {
        return base.ToString() + $", RAM: {RamGb}GB, Processor: {Processor}";
    }
}
