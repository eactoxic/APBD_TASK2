# APBD Task 2 - University Equipment Rental System

## How to Run

```bash
dotnet run
```

## Project Structure

```
EquipmentRentalSystem/
├── Database/
│   └── Singleton.cs
├── Enum/
│   ├── EquipmentStatus.cs
│   └── UserType.cs
├── Interfaces/
│   └── IRentalService.cs
├── Models/
│   ├── Equipment.cs
│   ├── Laptop.cs
│   ├── Projector.cs
│   ├── Camera.cs
│   ├── User.cs
│   └── Rental.cs
├── Services/
│   └── RentalService.cs
└── Program.cs
```

## Why I organized it this way

I put the models in a separate folder because they only hold data, they dont do anything else. The business logic is all in RentalService so if something needs to change I know exactly where to look.

I used a Singleton for the database because I wanted one place where all the lists live. At first I just had lists in the service class but then I thought if I ever add another service it wouldnt have access to the same data, so Singleton made more sense.

I made IRentalService interface because in the task it said to think about coupling. Program.cs uses the interface type so it doesnt depend directly on RentalService. I learned this idea from Java where we also used interfaces to reduce coupling between classes.

The Enum folder has EquipmentStatus and UserType. I could have used strings like "Available" or "Student" but enums are safer because you cant pass a wrong value by accident.

## Cohesion and Coupling

For cohesion - each class has one job. Equipment and its subclasses just store data about the equipment. RentalService handles all the rental operations. Rental stores info about one rental transaction.

For coupling - Program.cs only talks to IRentalService, not directly to RentalService. Models dont know anything about services.

## Business Rules

- Student max 2 active rentals
- Employee max 5 active rentals
- Late penalty is 5.00 per day
- Unavailable equipment cannot be rented
