using System;

#region Problem1_StructPoint
namespace CsharpG01Day06.StructDemo
{
    // Struct يمثل نقطة في بعدين (X , Y)
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        // Constructor 1: Initializing X to a specific value, Y = 0
        public Point(int x)
        {
            X = x;
            Y = 0;
        }

        // Constructor 2: Initializing both X and Y to specific values
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Overriding ToString for readability
        public override string ToString() => $"({X}, {Y})";
    }

    class Program
    {
        static void Main()
        {
            Point p1 = new Point(5);
            Point p2 = new Point(10, 20);
            Point p3 = new Point(); // Default constructor

            Console.WriteLine($"Point 1: {p1}");
            Console.WriteLine($"Point 2: {p2}");
            Console.WriteLine($"Point 3: {p3}");
        }
    }

    /*
    Question:
    Why can't a struct inherit from another struct or class in C#?

    Answer:
    1) Memory Layout & Predictability:
       Structs are Value Types and need fixed memory size.
       Inheritance would make size unpredictable.

    2) The Slicing Problem:
       Copying derived structs to base structs would lose data (slicing).

    3) Performance Overhead:
       Structs are lightweight; inheritance adds v-tables and metadata.
    */
}
#endregion

#region Problem2_AccessModifiers
namespace CsharpG01Day06.AccessModifierDemo
{
    public class TypeA
    {
        private int F = 10;      // Accessible only within this class
        internal int G = 20;     // Accessible within the same project
        public int H = 30;       // Accessible from anywhere

        public void DisplayPrivate()
        {
            Console.WriteLine($"Accessing Private F inside class: {F}");
        }
    }

    class Program
    {
        static void Main()
        {
            TypeA obj = new TypeA();

            Console.WriteLine($"Public H: {obj.H}");
            Console.WriteLine($"Internal G: {obj.G}");

            // Private cannot be accessed directly
            // Console.WriteLine(obj.F); // ERROR

            obj.DisplayPrivate(); // Accessing private indirectly
        }

        /*
        Question:
        How do access modifiers impact the scope and visibility of a class member?

        Answer:
        - private: Only visible within same class.
        - internal: Visible within same project (assembly).
        - public: Visible everywhere.

        Key Concepts:
        - Scope of modification: Limits where a variable/method can change.
        - Assembly boundaries: internal hides implementation from other assemblies.
        - Inheritance impact: protected allows derived classes to access members.
        */
    }
}
#endregion

#region Problem3_Encapsulation
namespace CsharpG01Day06.EncapsulationDemo
{
    public struct Employee
    {
        private int empId;
        private string name;
        private double salary;

        public Employee(int id, string name, double salary)
        {
            this.empId = id;
            this.name = name;
            this.salary = 0;
            Salary = salary; // use property for validation
        }

        public string GetName() => name;
        public void SetName(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                name = value;
            else
                Console.WriteLine("Invalid Name: cannot be empty");
        }

        public double Salary
        {
            get => salary;
            set
            {
                if (value >= 0)
                    salary = value;
                else
                    Console.WriteLine("Salary cannot be negative. Ignored.");
            }
        }

        public int EmpId => empId; // Read-only property

        public override string ToString() => $"ID: {empId}, Name: {name}, Salary: {salary:C}";
    }

    class Program
    {
        static void Main()
        {
            Employee emp = new Employee(101, "Alice", 5000);

            emp.Salary = 6000;   // Valid
            emp.SetName("");     // Invalid, blocked
            emp.Salary = -1000;  // Invalid, blocked

            Console.WriteLine("Final Employee Data:");
            Console.WriteLine(emp);
        }

        /*
        Data Integrity & Validation: Prevents invalid data entry.
        Abstraction: User doesn't need internal logic details.
        Flexibility & Maintenance: Internal changes don't break external code.
        Security: Limits where bugs can happen.
        Analogy: Car's steering wheel is public interface, engine is private state.
        */
    }
}
#endregion

#region Problem4_ValueVsReference
namespace CsharpG01Day06.ValueVsReferenceDemo
{
    public struct Point
    {
        public int X, Y;
        public Point(int x, int y) { X = x; Y = y; }
    }

    public class Employee
    {
        public string Name;
        public Employee(string name) { Name = name; }
    }

    class Program
    {
        static void Main()
        {
            Point myPoint = new Point(10, 10);
            Employee myEmp = new Employee("Alice");

            Console.WriteLine("--- Before Method Calls ---");
            Console.WriteLine($"Point: ({myPoint.X}, {myPoint.Y})");
            Console.WriteLine($"Employee: {myEmp.Name}");

            TryToModifyPoint(myPoint);
            TryToModifyEmployee(myEmp);

            Console.WriteLine("\n--- After Method Calls ---");
            Console.WriteLine($"Point: ({myPoint.X}, {myPoint.Y}) (No change!)");
            Console.WriteLine($"Employee: {myEmp.Name} (Changed!)");
        }

        static void TryToModifyPoint(Point p) { p.X = 99; }
        static void TryToModifyEmployee(Employee e) { e.Name = "Bob"; }

        /*
        Analysis:
        - Struct (Value Type): Passed by value, copy modified, original unchanged.
        - Class (Reference Type): Passed by reference, original object modified.
        Memory Allocation:
        - Stack: Structs, fast, lifetime tied to scope.
        - Heap: Classes, slower, managed by Garbage Collector.
        */
    }
}
#endregion
