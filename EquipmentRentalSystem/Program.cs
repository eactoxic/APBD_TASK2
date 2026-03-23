using EquipmentRentalSystem.Enum;
using EquipmentRentalSystem.Interfaces;
using EquipmentRentalSystem.Models;
using EquipmentRentalSystem.Services;
 
IRentalService service = new RentalService();
 
Console.WriteLine("-- 1. Adding equipment --");
 
var laptop1 = new Laptop("Dell XPS 15", "Portable laptop", 16, "Intel i7");
var laptop2 = new Laptop("MacBook Pro", "Apple laptop", 32, "Apple M3");
var projector = new Projector("Epson EB-X", "Classroom projector", 3500, true);
var camera = new Camera("Canon EOS R50", "Mirrorless camera", 24, "Wide-angle");
 
service.AddEquipment(laptop1);
service.AddEquipment(laptop2);
service.AddEquipment(projector);
service.AddEquipment(camera);
 
Console.WriteLine("Added 4 equipment items.");
 
Console.WriteLine("\n-- 2. Adding users --");
 
var student1 = new User("Alice", "Smith", UserType.Student);
student1.StudentNumber = "STU-001";
 
var student2 = new User("Bob", "Jones", UserType.Student);
student2.StudentNumber = "STU-002";
 
var employee1 = new User("Carol", "White", UserType.Employee);
employee1.Department = "IT Department";
 
service.AddUser(student1);
service.AddUser(student2);
service.AddUser(employee1);
 
Console.WriteLine("Added 3 users.");
 
Console.WriteLine("\n-- 3. All equipment --");
foreach (var e in service.GetAllEquipments())
{
    Console.WriteLine(e);
}
 
Console.WriteLine("\n-- 4. Available equipment --");
foreach (var e in service.GetAvailableEquipments())
{
    Console.WriteLine(e);
}
 
Console.WriteLine("\n-- 5. Renting equipment --");
 
var rental1 = service.RentEquipment(student1, laptop1, 7);
Console.WriteLine("Rented: " + rental1);
 
var rental2 = service.RentEquipment(student1, projector, 3);
Console.WriteLine("Rented: " + rental2);
 
Console.WriteLine("\n-- 6. Invalid operations --");
 
Console.WriteLine("Trying to rent already rented laptop...");
try
{
    service.RentEquipment(employee1, laptop1, 5);
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}
 
Console.WriteLine("Trying to exceed student limit for Alice...");
try
{
    service.RentEquipment(student1, camera, 7);
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}
 
Console.WriteLine("Marking camera as unavailable and trying to rent...");
service.MarkAsUnavailable(camera);
try
{
    service.RentEquipment(student2, camera, 3);
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}
 
Console.WriteLine("\n-- 7. On-time return --");
 
var rental3 = service.RentEquipment(student2, laptop2, 5);
Console.WriteLine("Rented: " + rental3);
 
var returned = service.ReturnEquipment(laptop2);
Console.WriteLine($"Returned on time. Penalty: {returned.Penalty}");
 
Console.WriteLine("\n-- 8. Late return --");
 
camera.Status = EquipmentStatus.Available;
var rental4 = service.RentEquipment(student2, camera, 3);
Console.WriteLine("Rented: " + rental4);
 
var lateDate = rental4.DueDate.AddDays(10);
var lateReturn = ((RentalService)service).ReturnEquipmentWithDate(camera, lateDate);
Console.WriteLine($"Returned 10 days late. Penalty: {lateReturn.Penalty}");
 
Console.WriteLine("\n-- 9. Active rentals for Alice --");
var aliceRentals = service.GetActiveRentalsForUser(student1);
if (aliceRentals.Count == 0)
{
    Console.WriteLine("No active rentals.");
}
else
{
    foreach (var r in aliceRentals)
        Console.WriteLine(r);
}
 
Console.WriteLine("\n-- 10. Overdue rentals --");
var overdue = service.GetOverdueRentals();
if (overdue.Count == 0)
{
    Console.WriteLine("No overdue rentals.");
}
else
{
    foreach (var r in overdue)
        Console.WriteLine(r);
}
 
service.PrintReport();