namespace EquipmentRentalSystem.Database;

using EquipmentRentalSystem.Models;

public class Singleton
{
        private static Singleton? _instance;

        public List<Equipment> Equipments { get; set; } = new List<Equipment>();
        public List<User> Users { get; set; } = new List<User>();
        public List<Rental> Rentals { get; set; } = new List<Rental>();

        private Singleton()
        {
        }

        public static Singleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }

            return _instance;
        }
    }

