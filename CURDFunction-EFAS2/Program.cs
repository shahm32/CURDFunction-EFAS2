using Core.Entities;
using Core;
using Core.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

class Program
{
    private static IPersonRepository _repository;

    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=persons.db"))
            .AddScoped<IPersonRepository, PersonRepository>()
        .BuildServiceProvider();

        _repository = serviceProvider.GetService<IPersonRepository>();
        using (var context = serviceProvider.GetService<AppDbContext>())
        {
            context.Database.Migrate();
        }

        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Add Person");
            Console.WriteLine("2. Get Person");
            Console.WriteLine("3. Get All Persons");
            Console.WriteLine("4. Update Person");
            Console.WriteLine("5. Delete Person");
            Console.WriteLine("6. Exit");

            switch (Console.ReadLine())
            {
                case "1":
                    AddPerson();
                    break;
                case "2":
                    GetPerson();
                    break;
                case "3":
                    GetAllPersons();
                    break;
                case "4":
                    UpdatePerson();
                    break;
                case "5":
                    DeletePerson();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    private static void AddPerson()
    {
        Console.Write("Enter name: ");
        string name = Console.ReadLine();
        Console.Write("Enter email: ");
        string email = Console.ReadLine();

        var person = new Person { Name = name, Email = email };
        _repository.AddPerson(person);
        Console.WriteLine("Person added successfully.");
    }

    private static void GetPerson()
    {
        Console.Write("Enter ID: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var person = _repository.GetPerson(id);
            if (person != null)
            {
                Console.WriteLine($"ID: {person.ID}, Name: {person.Name}, Email: {person.Email}");
            }
            else
            {
                Console.WriteLine("Person not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    private static void GetAllPersons()
    {
        var persons = _repository.GetAllPersons();
        foreach (var person in persons)
        {
            Console.WriteLine($"ID: {person.ID}, Name: {person.Name}, Email: {person.Email}");
        }
    }

    private static void UpdatePerson()
    {
        Console.Write("Enter ID: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var person = _repository.GetPerson(id);
            if (person != null)
            {
                Console.Write("Enter new name: ");
                person.Name = Console.ReadLine();
                Console.Write("Enter new email: ");
                person.Email = Console.ReadLine();

                _repository.UpdatePerson(person);
                Console.WriteLine("Person updated successfully.");
            }
            else
            {
                Console.WriteLine("Person not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    private static void DeletePerson()
    {
        Console.Write("Enter ID: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            _repository.DeletePerson(id);
            Console.WriteLine("Person deleted successfully.");
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }
}
