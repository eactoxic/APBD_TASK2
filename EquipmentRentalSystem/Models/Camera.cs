namespace EquipmentRentalSystem.Models;

public class Camera : Equipment
{
    public int MegaPixels { get; set; }
    public string LensType { get; set; } = null!;

    public Camera(string name, string description, int megaPixels, string lensType)
        : base(name, description)
    {
        MegaPixels = megaPixels;
        LensType = lensType;
    }

    public override string ToString()
    {
        return base.ToString() + $", MegaPixels: {MegaPixels}, LensType: {LensType}";
    }
}