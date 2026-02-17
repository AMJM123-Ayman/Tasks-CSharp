using System;
using System.Collections.Generic;

#region Problem1: Interfaces and Polymorphism
// Define the interface
public interface IVehicle
{
    void StartEngine();
    void StopEngine();
}

// Implement the interface in the Car class
public class Car : IVehicle
{
    public void StartEngine()
    {
        Console.WriteLine("Car engine started with a smooth purr.");
    }

    public void StopEngine()
    {
        Console.WriteLine("Car engine stopped.");
    }
}

// Implement the interface in the Bike class
public class Bike : IVehicle
{
    public void StartEngine()
    {
        Console.WriteLine("Bike engine started with a roar.");
    }

    public void StopEngine()
    {
        Console.WriteLine("Bike engine stopped.");
    }
}

// Demonstration Program
class Program1
{
    public static void Execute()
    {
        // Using IVehicle objects to demonstrate polymorphism
        IVehicle myCar = new Car();
        IVehicle myBike = new Bike();

        Console.WriteLine("--- Testing Car ---");
        myCar.StartEngine();
        myCar.StopEngine();

        Console.WriteLine("\n--- Testing Bike ---");
        myBike.StartEngine();
        myBike.StopEngine();
    }
}
/*
 * Question: Why is it better to code against an interface rather than a concrete class?
 * --------------------------------------------------------------------------------
 * 1. Decoupling: The program remains independent of specific implementation details.
 * 2. Flexibility: You can easily swap implementations without changing the consumer code.
 * 3. Maintainability: Follows the principle of "coding against abstraction, not concreteness."
 */
#endregion

#region Problem2: Abstract Classes vs Interfaces
namespace ShapeApplication
{
    public abstract class Shape
    {
        public abstract double GetArea();

        public void Display()
        {
            Console.WriteLine($"[Display] This is a {this.GetType().Name}.");
        }
    }

    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public override double GetArea() => Width * Height;
    }

    public class Circle : Shape
    {
        public double Radius { get; set; }
        public override double GetArea() => Math.PI * Math.Pow(Radius, 2);
    }

    class ShapeDemo
    {
        public static void Execute()
        {
            Console.WriteLine("--- Abstract Class Approach ---");
            Shape myRect = new Rectangle { Width = 5, Height = 10 };
            myRect.Display();
            Console.WriteLine($"Rectangle Area: {myRect.GetArea()}");

            Shape myCircle = new Circle { Radius = 7 };
            myCircle.Display();
            Console.WriteLine($"Circle Area: {myCircle.GetArea():F2}");
        }
    }
}
/*
 * Question: When should you prefer an abstract class over an interface?
 * Answer: 
 * 1. To provide shared code (e.g., Display method).
 * 2. When an "Is-A" relationship exists.
 * 3. To define fields or non-static state.
 */
#endregion

#region Problem3: IComparable and Sorting
namespace ProductSorting
{
    public class Product : IComparable<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public Product(int id, string name, double price)
        {
            Id = id; Name = name; Price = price;
        }

        public int CompareTo(Product other)
        {
            if (other == null) return 1;
            return this.Price.CompareTo(other.Price);
        }

        public override string ToString() => $"ID: {Id}, Name: {Name}, Price: ${Price}";
    }
}
#endregion

#region Problem4: Copy Constructors (Deep Copy)
namespace StudentCopyDemo
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Grade { get; set; }

        public Student(int id, string name, double grade)
        {
            Id = id; Name = name; Grade = grade;
        }

        // Copy Constructor for Deep Copy
        public Student(Student other)
        {
            this.Id = other.Id;
            this.Name = other.Name;
            this.Grade = other.Grade;
        }

        public override string ToString() => $"[ID: {Id}, Name: {Name}, Grade: {Grade}]";
    }

    class CopyDemo
    {
        public static void Execute()
        {
            Student original = new Student(101, "Alice", 95.5);
            Student deepCopy = new Student(original); // Deep copy

            original.Name = "Bob";
            Console.WriteLine($"Original: {original}");
            Console.WriteLine($"Deep Copy: {deepCopy} (Unchanged)");
        }
    }
}
#endregion

#region Problem5: Explicit Interface Implementation
namespace ExplicitInterfaceDemo
{
    public interface IWalkable { void Walk(); }

    public class Robot : IWalkable
    {
        public void Walk() => Console.WriteLine("Robot moving on wheels.");

        // Explicit Implementation
        void IWalkable.Walk() => Console.WriteLine("Robot performing human walk.");
    }
}
#endregion

#region Problem6: Structs and Encapsulation
namespace StructEncapsulationDemo
{
    public struct Account
    {
        private double _balance;
        public double Balance
        {
            get => _balance;
            set => _balance = value >= 0 ? value : 0;
        }

        public Account(double initialBalance) => _balance = initialBalance >= 0 ? initialBalance : 0;
    }
}
#endregion

#region Problem7: Default Interface Methods
namespace LoggerDemo
{
    public interface ILogger
    {
        void Log(string message) => Console.WriteLine($"Default Log: {message}");
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string message) => Console.WriteLine($"Console: {message}");
    }
}
#endregion

#region Problem8: Constructor Overloading
namespace ConstructorOverloadingDemo
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }

        public Book() : this("Unknown", "Unknown") { }
        public Book(string title) : this(title, "Unknown") { }
        public Book(string title, string author)
        {
            Title = title; Author = author;
        }
    }
}
#endregion