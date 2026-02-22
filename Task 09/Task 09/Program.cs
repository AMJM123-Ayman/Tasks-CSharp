using System;
using System.Runtime.InteropServices;

#region  Enums: Weekdays, Grades & Gender
namespace EnumLab
{
    // Problem: Weekdays Enum
    public enum Weekdays { Monday = 1, Tuesday, Wednesday, Thursday, Friday }

    // Problem: Grades Enum with short underlying type
    public enum Grades : short { F = -1, D, C, B, A }

    // Problem: Gender Enum with byte underlying type
    public enum Gender : byte { Male, Female }

    public class EnumDemo
    {
        public static void Run()
        {
            Console.WriteLine("--- Weekdays ---");
            foreach (var day in Enum.GetValues(typeof(Weekdays)))
                Console.WriteLine($"{day} = {(int)day}");

            Console.WriteLine("\n--- Grades (short) ---");
            foreach (var grade in Enum.GetValues(typeof(Grades)))
                Console.WriteLine($"{grade} = {(short)grade}");

            Console.WriteLine("\n--- Gender Memory Usage ---");
            // Demonstrating memory usage using Marshal.SizeOf or simply checking the type
            Console.WriteLine($"Size of Gender (byte): {sizeof(Gender)} byte");
            Console.WriteLine($"Size of default Enum (int): {sizeof(int)} bytes");
        }
    }
}
/*
 * Question: Why is it recommended to explicitly assign values to enum members in some cases?
 * Answer: Explicit assignment prevents data corruption if you later add new members in the middle. 
 * It also ensures consistency when enum values are saved in databases or shared between different systems.
 * * Question: What happens if you assign a value to an enum member that exceeds the underlying type's range?
 * Answer: The compiler will throw an error (Compile-time error). You cannot assign a value that 
 * the chosen underlying type (e.g., byte, short) cannot hold.
 * * Question: When should you consider changing the underlying type of an enum?
 * Answer: Consider it when memory optimization is critical (using 'byte' for small sets) or when 
 * you need the enum to match the data type of an external system or database column.
 */
#endregion

#region  Classes: Inheritance, Virtual & Sealed
namespace InheritanceLab
{
    public class Person
    {
        public string Name { get; set; }
        // Problem: Add Department property
        public virtual string Department { get; set; }

        public void PrintDetails() => Console.WriteLine($"Name: {Name}, Dept: {Department}");
    }

    public class Parent
    {
        public virtual double Salary { get; set; }
    }

    public class Child : Parent
    {
        // Problem: Sealed property
        public sealed override double Salary { get; set; }

        // Problem: Method using sealed Salary
        public void DisplaySalary() => Console.WriteLine($"Child's Sealed Salary: {Salary}");
    }

    public class ProgramDemo
    {
        public static void Run()
        {
            // Instantiate two Person objects
            Person p1 = new Person { Name = "Ahmed", Department = "IT" };
            Person p2 = new Person { Name = "Sara", Department = "HR" };
            p1.PrintDetails();
            p2.PrintDetails();

            // Demonstrate Child with sealed property
            Child c = new Child { Salary = 5000 };
            c.DisplaySalary();
        }
    }
}
/*
 * Question: What is the purpose of the virtual keyword when used with properties?
 * Answer: It allows derived classes to override the property's implementation to provide 
 * specialized logic for that specific subclass while maintaining polymorphism.
 * * Question: Why can’t you override a sealed property or method?
 * Answer: 'Sealed' marks the implementation as final. It is a design choice to prevent 
 * further subclasses from altering the logic, ensuring consistency or security in the class hierarchy.
 */
#endregion

#region  Static Members: Utility Class
namespace StaticLab
{
    public static class Utility
    {
        // Problem: Static method for Perimeter
        public static double CalculatePerimeter(double length, double width) => 2 * (length + width);

        // Problem: Static method for Temperature Conversion
        public static double CelsiusToFahrenheit(double c) => (c * 9 / 5) + 32;
    }
}
/*
 * Question: What is the key difference between static and object members?
 * Answer: Static members belong to the Type (Class) itself and are shared by all instances, 
 * whereas object (instance) members belong to a specific instance of the class and store unique data for it.
 */
#endregion

#region  Operator Overloading: Complex Numbers
namespace OverloadingLab
{
    public class ComplexNumber
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        // Problem: Operator Overloading for Multiplication (*)
        // (a + bi) * (c + di) = (ac - bd) + (ad + bc)i
        public static ComplexNumber operator *(ComplexNumber c1, ComplexNumber c2)
        {
            return new ComplexNumber
            {
                Real = (c1.Real * c2.Real) - (c1.Imaginary * c2.Imaginary),
                Imaginary = (c1.Real * c2.Imaginary) + (c1.Imaginary * c2.Real)
            };
        }

        public override string ToString() => $"{Real} + {Imaginary}i";
    }

    public class OverloadDemo
    {
        public static void Run()
        {
            ComplexNumber c1 = new ComplexNumber { Real = 2, Imaginary = 3 };
            ComplexNumber c2 = new ComplexNumber { Real = 4, Imaginary = 5 };
            ComplexNumber result = c1 * c2;
            Console.WriteLine($"Result of {c1} * {c2} = {result}");
        }
    }
}
/*
 * Question: Can you overload all operators in C#? Explain why or why not.
 * Answer: No. Operators like assignment (=), member access (.), logical AND (&&), OR (||), 
 * and the ternary operator (?:) cannot be overloaded to preserve the fundamental logic 
 * and structure of the C# language.
 */
#endregion

#region  Enum Parsing (Grades)
namespace EnumParsingLab
{
    public enum Grades { F = -1, D, C, B, A }

    public class ParsingDemo
    {
        public static void Run()
        {
            // Problem: Try to parse a string to Grades enum
            string input = "B";
            if (Enum.TryParse(input, out Grades result))
            {
                Console.WriteLine($"Successfully parsed '{input}' to: {result} (Value: {(int)result})");
            }
            else
            {
                Console.WriteLine("Invalid input. Could not parse to Grades.");
            }
        }
    }
}
/*
 * Question: What are the advantages of using Enum.TryParse over direct parsing with int.Parse?
 * Answer: Enum.TryParse is much safer; it doesn't throw an exception if the parsing fails. 
 * Instead, it returns a boolean (true/false), allowing you to handle errors gracefully 
 * without crashing the application.
 */
#endregion

#region  Object Methods (Equals & ToString)
namespace ObjectOverridesLab
{
    public class Department
    {
        public string Name { get; set; }
        // Overriding Equals for Department to compare by Name
        public override bool Equals(object obj) => obj is Department d && d.Name == this.Name;
        public override int GetHashCode() => Name?.GetHashCode() ?? 0;
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Department Dept { get; set; }

        // Problem: Enhance Employee with Equals
        public override bool Equals(object obj) => obj is Employee e && e.Id == this.Id;
        public override int GetHashCode() => Id.GetHashCode();

        // Question: Why is overriding ToString beneficial?
        public override string ToString() => $"Employee: {Name} (ID: {Id}), Dept: {Dept?.Name}";
    }
}
/*
 * Question 1: What is the difference between overriding Equals and == for object comparison in C# struct and class?
 * Answer: For classes, '==' checks reference equality (memory location) by default. 
 * For structs, '==' is not implemented by default. 'Equals' is a method we override 
 * to define "Value Equality" (comparing data content instead of memory addresses).
 * * Question 2: Why is overriding ToString beneficial when working with custom classes?
 * Answer: It allows the developer to provide a meaningful string representation of the object, 
 * which is essential for debugging, logging, and displaying data in a readable format.
 */
#endregion

#region  Generics (Max & ReplaceArray)
namespace GenericsLab
{
    public class Helper
    {
        // Problem: Generic Max method
        public static T Max<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) > 0 ? a : b;
        }
    }

    public class Helper2
    {
        // Problem: Generic ReplaceArray
        public static void ReplaceArray<T>(T[] array, T oldValue, T newValue)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (EqualityComparer<T>.Default.Equals(array[i], oldValue))
                    array[i] = newValue;
            }
        }

        // SearchArray used for testing search accuracy
        public static int SearchArray<T>(T[] array, T target)
        {
            for (int i = 0; i < array.Length; i++)
                if (array[i].Equals(target)) return i;
            return -1;
        }
    }
}
/*
 * Question 1: Can generics be constrained to specific types in C#? Provide an example.
 * Answer: Yes, using the 'where' keyword. Example: 'where T : IComparable' restricts T 
 * to types that can be compared, or 'where T : class' restricts T to reference types.
 * * Question 2: What are the key differences between generic methods and generic classes?
 * Answer: A generic class applies the type parameter to all its members. 
 * A generic method defines the type parameter only for that specific method, 
 * even if the class itself is not generic.
 */
#endregion

#region  Structs & Swapping
namespace StructsLab
{
    public struct Rectangle
    {
        public double Length { get; set; }
        public double Width { get; set; }
    }

    public class SwapDemo
    {
        // Problem: Non-generic Swap for Rectangle
        public static void Swap(ref Rectangle r1, ref Rectangle r2)
        {
            Rectangle temp = r1;
            r1 = r2;
            r2 = temp;
        }
    }
}
/*
 * Question: Why might using a generic swap method be preferable to implementing custom methods for each type?
 * Answer: Reusability and Type Safety. A generic swap 'Swap<T>' is written once 
 * and works for any type (int, string, Rectangle), preventing code duplication (DRY Principle).
 */
#endregion

#region  Advanced Searching & Accuracy
/* * Problem: Create Department class and search an array of employees by department.
 * (Implementation is combined in ObjectOverridesLab and tested here)
 * * Question: How can overriding Equals for the Department class improve the accuracy of searches?
 * Answer: Without overriding Equals, the search would only find a department if it is 
 * the exact same instance in memory. By overriding it, we can find a department 
 * based on its Name (Value Equality), which is what we usually want in real apps.
 */
#endregion

#region  Struct Circle Comparison
namespace FinalLab
{
    public struct Circle
    {
        public double Radius { get; set; }
        public string Color { get; set; }

        // Note: For structs, you MUST overload == manually to use it
        public static bool operator ==(Circle c1, Circle c2) => c1.Equals(c2);
        public static bool operator !=(Circle c1, Circle c2) => !c1.Equals(c2);

        public override bool Equals(object obj) =>
            obj is Circle c && c.Radius == this.Radius && c.Color == this.Color;

        public override int GetHashCode() => HashCode.Combine(Radius, Color);
    }
}
/*
 * Question: Why is == not implemented by default for structs?
 * Answer: Because structs are value types, and the compiler would need to compare 
 * every single field. This is technically expensive (requires Reflection) and 
 * might not be what the developer wants (e.g., if some fields shouldn't be compared). 
 * C# leaves this implementation to the developer for performance and clarity.
 */
#endregion



#region Enums: Weekdays, Grades & Gender
namespace EnumLab
{
    // Problem: Define Enums with specific underlying types
    public enum Weekdays { Monday = 1, Tuesday, Wednesday, Thursday, Friday }
    public enum Grades : short { F = -1, D, C, B, A }
    public enum Gender : byte { Male, Female }

    public class EnumDemo
    {
        public static void Run()
        {
            Console.WriteLine("--- Weekdays ---");
            foreach (var day in Enum.GetValues(typeof(Weekdays)))
                Console.WriteLine($"{day} = {(int)day}");

            Console.WriteLine("\n--- Grades (short) ---");
            foreach (var grade in Enum.GetValues(typeof(Grades)))
                Console.WriteLine($"{grade} = {(short)grade}");

            Console.WriteLine("\n--- Gender Memory Usage ---");
            Console.WriteLine($"Size of Gender (byte): {sizeof(Gender)} byte");
            Console.WriteLine($"Size of default Enum (int): {sizeof(int)} bytes");
        }
    }
}
/*
 * Question 1: Why is it recommended to explicitly assign values to enum members in some cases?
 * Answer: It prevents data corruption if you later add new members in the middle. 
 * It also ensures consistency when enum values are saved in databases.
 * * Question 2: What happens if you assign a value that exceeds the underlying type's range?
 * Answer: The compiler will throw an error (Compile-time error). 
 * * Question 3: When should you consider changing the underlying type of an enum?
 * Answer: When memory optimization is critical (e.g., using 'byte' for small sets) or 
 * to match an external system/database column type.
 */
#endregion

#region  Classes: Inheritance, Virtual & Sealed
namespace InheritanceLab
{
    public class Person
    {
        public string Name { get; set; }
        public virtual string Department { get; set; }

        public void PrintDetails() => Console.WriteLine($"Name: {Name}, Dept: {Department}");
    }

    public class Parent
    {
        public virtual double Salary { get; set; }
    }

    public class Child : Parent
    {
        // Problem: Sealed property to prevent further overriding
        public sealed override double Salary { get; set; }

        public void DisplaySalary() => Console.WriteLine($"Child's Sealed Salary: {Salary}");
    }
}
/*
 * Question 4: What is the purpose of the virtual keyword when used with properties?
 * Answer: It allows derived classes to override the property's implementation to provide 
 * specialized logic while maintaining polymorphism.
 * * Question 5: Why can’t you override a sealed property or method?
 * Answer: 'Sealed' marks the implementation as final. It prevents further subclasses from 
 * altering logic, ensuring consistency or security in the class hierarchy.
 */
#endregion

#region  Static Members & Constructors
namespace StaticLab
{
    public static class Utility
    {
        public static double CalculatePerimeter(double length, double width) => 2 * (length + width);
        public static double CelsiusToFahrenheit(double c) => (c * 9 / 5) + 32;
        public static double FahrenheitToCelsius(double f) => (f - 32) * 5 / 9;
    }
}
/*
 * Question 6: What is the key difference between static and object members?
 * Answer: Static members belong to the Type (Class) itself and are shared by all instances, 
 * whereas object members belong to a specific instance.
 * * Question 7: Why can't a static class have instance constructors?
 * Answer: Because static classes cannot be instantiated. All members must be static, 
 * and instance constructors require creating an object.
 */
#endregion

#region  Operator Overloading: Complex Numbers
namespace OverloadingLab
{
    public class ComplexNumber
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        // Problem: Overload multiplication (*)
        public static ComplexNumber operator *(ComplexNumber c1, ComplexNumber c2)
        {
            return new ComplexNumber
            {
                Real = (c1.Real * c2.Real) - (c1.Imaginary * c2.Imaginary),
                Imaginary = (c1.Real * c2.Imaginary) + (c1.Imaginary * c2.Real)
            };
        }

        public override string ToString() => $"{Real} + {Imaginary}i";
    }
}
/*
 * Question 8: Can you overload all operators in C#?
 * Answer: No. Operators like assignment (=), member access (.), and logical short-circuits (&&, ||) 
 * cannot be overloaded to preserve the fundamental logic of the language.
 */
#endregion

#region Enum Parsing & Safe Handling
namespace ParsingLab
{
    public class ParsingDemo
    {
        public static void Run()
        {
            string input = "A";
            // Problem: Safe parsing
            if (Enum.TryParse(input, out EnumLab.Grades grade))
                Console.WriteLine($"Parsed Grade: {grade}");
            else
                Console.WriteLine("Invalid Grade");
        }
    }
}
/*
 * Question 9: What are the advantages of using Enum.TryParse over int.Parse?
 * Answer: It does not throw an exception on failure. It returns a boolean, 
 * making it safer and better for user input validation.
 */
#endregion

#region  Object Methods: Equals & ToString
namespace ObjectLab
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Problem: Logical equality override
        public override bool Equals(object obj) => obj is Employee e && this.Id == e.Id;
        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => $"Employee Name: {Name} (ID: {Id})";
    }
}
/*
 * Question 10: What is the difference between overriding Equals and == for classes?
 * Answer: '==' checks reference equality (memory location) by default. 'Equals' is 
 * overridden to define "Value Equality" (logical content comparison).
 * * Question 11: Why is overriding ToString beneficial?
 * Answer: It provides a meaningful string representation of the object, which is 
 * essential for debugging, logging, and clean output.
 */
#endregion

#region  Generics: Constraints & Methods
namespace GenericsLab
{
    public class Helper
    {
        // Problem: Generic Max with IComparable constraint
        public static T Max<T>(T x, T y) where T : IComparable<T>
        {
            return x.CompareTo(y) > 0 ? x : y;
        }

        // Problem: Generic ReplaceArray
        public static void ReplaceArray<T>(T[] arr, T oldValue, T newValue)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (EqualityComparer<T>.Default.Equals(arr[i], oldValue))
                    arr[i] = newValue;
            }
        }
    }
}
/*
 * Question 12: Can generics be constrained to specific types?
 * Answer: Yes, using the 'where' keyword (e.g., 'where T : class', 'where T : struct', 'where T : IComparable').
 * * Question 13: What are the key differences between generic methods and generic classes?
 * Answer: Generic methods define the type parameter only for that specific method, 
 * while generic classes apply it to all their members and fields at the object level.
 */
#endregion

#region  Structs & Swapping Logic
namespace StructsLab
{
    public struct Rectangle { public int Length, Width; }

    public class SwapDemo
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
}
/*
 * Question 14: Why might using a generic swap method be preferable?
 * Answer: Reusability. It works for all types (int, string, Rectangle) without 
 * duplicating code for each specific type.
 */
#endregion

#region Advanced Searching Accuracy
namespace SearchLab
{
    public class Department
    {
        public string Name { get; set; }
        public override bool Equals(object obj) => obj is Department d && Name == d.Name;
    }
}
/*
 * Question 15: How can overriding Equals for the Department class improve search accuracy?
 * Answer: It enables value-based comparison. Without it, searches only find a 
 * match if it's the exact same instance in memory.
 */
#endregion

#region  Struct vs Class Equality (Final Comparison)
namespace FinalLab
{
    public struct CircleStruct { public double Radius; public string Color; }
    public class CircleClass { public double Radius; public string Color; }
}
/*
 * Question 16: Why is == not implemented by default for structs?
 * Answer: Structs can have many fields. Comparing every field via reflection is expensive. 
 * C# requires the developer to define it explicitly for performance and clarity.
 * * Question 17: Summarize CircleStruct vs CircleClass comparison.
 * Answer: Struct Equals checks values field-by-field. Class Equals (by default) 
 * checks if they point to the same memory location.
 */
#endregion

#region Part 02
//Generic Method for Reversing an Array//
public static T[] ReverseArray<T>(T[] array)
{
    T[] reversed = new T[array.Length];
    for (int i = 0; i < array.Length; i++)
    {
        reversed[i] = array[array.Length - 1 - i];
    }
    return reversed;
}
//Generic Class for a Stack//
public class Stack<T>
{
    private List<T> items = new List<T>();

    public void Push(T item) => items.Add(item);
    public T Pop()
    {
        if (items.Count == 0) throw new InvalidOperationException("Stack is empty");
        T item = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        return item;
    }
    public T Peek()
    {
        if (items.Count == 0) throw new InvalidOperationException("Stack is empty");
        return items[items.Count - 1];
    }
}
//Generic Method for Swapping Elements//
public static void Swap<T>(T[] array, int i, int j)
{
    T temp = array[i];
    array[i] = array[j];
    array[j] = temp;
}
//Generic Method for Finding Maximum Element//
public static T MaxElement<T>(T[] array) where T : IComparable<T>
{
    T max = array[0];
    foreach (T item in array)
    {
        if (item.CompareTo(max) > 0)
            max = item;
    }
    return max;
}

#endregion