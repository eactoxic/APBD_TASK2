namespace EquipmentRentalSystem.Models;

public class Projector : Equipment
{
    public int LumensOutput { get; set; }
    public bool HasHdmi { get; set; }

    public Projector(string name, string description, int lumensOutput, bool hasHdmi)
        : base(name, description)
    {
        LumensOutput = lumensOutput;
        HasHdmi = hasHdmi;
    }

    public override string ToString()
    {
        return base.ToString() + $", Lumens: {LumensOutput}, HDMI: {HasHdmi}";
    }
}
