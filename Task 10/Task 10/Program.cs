using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvancedCSharpTasks
{
 
    #region Core Helper Classes for Problem Solving

    public class Employee : ICloneable, IComparable<Employee>
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }

        public Employee(string name, decimal salary)
        {
            Name = name;
            Salary = salary;
        }

        // For Problem 10
        public object Clone()
        {
            return new Employee(this.Name, this.Salary);
        }

        // For Problem 1 and 4
        public int CompareTo(Employee other)
        {
            if (other == null) return 1;
            return this.Salary.CompareTo(other.Salary);
        }

        public override string ToString() => $"Name: {Name}, Salary: {Salary:C}";
    }

    // For Problem 4
    public class Manager : Employee, IComparable<Manager>
    {
        public Manager(string name, decimal salary) : base(name, salary) { }

        public int CompareTo(Manager other)
        {
            if (other == null) return 1;
            return this.Salary.CompareTo(other.Salary);
        }
    }

    // For Problem 1, 7, and 10
    // Constraints added: ICloneable for Problem 10, IComparable for Problem 1
    public class SortingAlgorithm<T> where T : ICloneable, IComparable<T>
    {
        public static void Sort(T[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) > 0)
                    {
                        Swap(ref array[j], ref array[j + 1]);
                    }
                }
            }
        }

        // Problem 7 (Standalone generic Swap method)
        public static void Swap<U>(ref U a, ref U b)
        {
            U temp = a;
            a = b;
            b = temp;
        }
    }

    // For Problem 2, 3, and 8
    public class SortingTwo<T>
    {
        // Uses Func to return True if the first element is logically greater (to trigger a swap)
        public static void Sort(T[] array, Func<T, T, bool> shouldSwap)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (shouldSwap(array[j], array[j + 1]))
                    {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }
    }

    // Custom Delegates
    // For Problem 11
    public delegate string StringTransformerDelegate(string input);
    // For Problem 12
    public delegate int MathOperationDelegate(int a, int b);
    // For Problem 13
    public delegate R GenericTransformerDelegate<T, R>(T input); 
    #endregion

 
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Part 01: Generics, Delegates, and Lambdas ===\n");

            #region Problem 01
            // -----------------------------------------------------------------
            // Problem 1: SortingAlgorithm<T> for Employee array by salary
            // -----------------------------------------------------------------
            Console.WriteLine("--- Problem 1 ---");
            Employee[] employees = {
                new Employee("Ahmed", 5000),
                new Employee("Sara", 3000),
                new Employee("Omar", 7000)
            };
            SortingAlgorithm<Employee>.Sort(employees);
            foreach (var emp in employees) Console.WriteLine(emp);
            /*
             * Question: What are the benefits of using a generic sorting algorithm over a non-generic one?
             * Answer: 
             * 1. Reusability: Code can be written once and used for any data type (numbers, strings, or custom objects).
             * 2. Type Safety: The compiler catches errors during compile-time rather than throwing exceptions at runtime.
             * 3. Performance: It prevents the expensive boxing/unboxing operations that occur when using the base 'object' type.
             */ 
            #endregion

            #region Problem 02

            // -----------------------------------------------------------------
            // Problem 2: Modify SortingTwo<T>.Sort to dynamically sort integers descending using lambda
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Problem 2 ---");
            int[] numbersDesc = { 5, 2, 9, 1, 6 };
            SortingTwo<int>.Sort(numbersDesc, (x, y) => x < y); // Condition x < y for descending
            Console.WriteLine(string.Join(", ", numbersDesc));
            /*
             * Question: How do lambda expressions improve the readability and flexibility of sorting methods?
             * Answer: 
             * They make the code highly concise and readable by placing the comparison logic directly inline at the call site. 
             * This eliminates the need to create separate, named external methods for every different sorting criteria.
             */ 
            #endregion

            #region Problem 03
            // -----------------------------------------------------------------
            // Problem 3: Compare function to sort strings by length ascending using SortingTwo<T>.Sort
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Problem 3 ---");
            string[] words = { "Apple", "To", "Banana", "Car" };
            SortingTwo<string>.Sort(words, (s1, s2) => s1.Length > s2.Length);
            Console.WriteLine(string.Join(", ", words));
            /*
             * Question: Why is it important to use a dynamic compare function when sorting objects of various data types?
             * Answer:
             * Sorting criteria vary heavily depending on the data type (e.g., strings by length, numbers by value, objects by specific properties). 
             * A dynamic function allows the developer to inject the exact sorting logic externally without altering the core algorithm.
             */ 
            #endregion

            #region Problem 04

            // -----------------------------------------------------------------
            // Problem 4: Manager class inherits Employee, implements IComparable, update sorting logic
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Problem 4 ---");
            Manager[] managers = {
                new Manager("Khalid", 12000),
                new Manager("Mona", 9000)
            };
            SortingAlgorithm<Manager>.Sort(managers);
            foreach (var mgr in managers) Console.WriteLine(mgr);
            /*
             * Question: How does implementing IComparable<T> in derived classes enable custom sorting?
             * Answer:
             * It forces the derived class to provide a specific implementation of the `CompareTo` method. This allows 
             * the generic sorting algorithm to know exactly how to compare instances of that class natively, ensuring customized, built-in ordering.
             */ 
            #endregion

            #region Problem 05
            // -----------------------------------------------------------------
            // Problem 5: Create a delegate Func<T, T, bool> to compare Employee by Name length
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Problem 5 ---");
            Func<Employee, Employee, bool> compareNameLength = (e1, e2) => e1.Name.Length > e2.Name.Length;
            SortingTwo<Employee>.Sort(employees, compareNameLength);
            foreach (var emp in employees) Console.WriteLine(emp.Name);
            /*
             * Question: What is the advantage of using built-in delegates like Func<T, T, TResult> in generic programming?
             * Answer:
             * Built-in delegates provide standard, reusable signatures. They reduce boilerplate code (no need to define custom delegates), 
             * improve code readability, and ensure high compatibility with built-in .NET libraries like LINQ.
             */

            #endregion

            #region Problem 06
            // -----------------------------------------------------------------
            // Problem 6: Anonymous function to sort int array ascending, then demonstrate Lambda
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Problem 6 ---");
            int[] numsAsc = { 8, 3, 7, 2 };

            // Using Anonymous Function
            SortingTwo<int>.Sort(numsAsc, delegate (int a, int b)
            {
                return a > b;
            });

            // Using Lambda Expression (same logic)
            SortingTwo<int>.Sort(numsAsc, (a, b) => a > b);
            Console.WriteLine(string.Join(", ", numsAsc));
            /*
             * Question: How does the usage of anonymous functions differ from lambda expressions in terms of readability and efficiency?
             * Answer:
             * Efficiency: They are practically identical as the compiler translates both into similar intermediate code.
             * Readability: Lambda expressions are significantly more concise, modern, and readable because they leverage type inference 
             * and omit redundant keywords like `delegate`.
             */ 
            #endregion

            #region Problem 07
            // -----------------------------------------------------------------
            // Problem 7: Enhance SortingAlgorithm<T> with standalone generic Swap<T>
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Problem 7 ---");
            int x = 10, y = 20;
            Console.WriteLine($"Before Swap: x={x}, y={y}");
            SortingAlgorithm<int>.Swap(ref x, ref y);
            Console.WriteLine($"After Swap: x={x}, y={y}");
            /*
             * Question: Why is the use of generic methods beneficial when creating utility functions like Swap?
             * Answer:
             * It allows you to write the swapping logic exactly once, which can then be applied to any data type (integers, strings, objects). 
             * This prevents code duplication while maintaining strict compile-time type safety.
             */ 
            #endregion

            #region Problem 08
            // -----------------------------------------------------------------
            // Problem 8: Sort Employee first by Salary, then by Name using custom comparer
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Problem 8 ---");
            Employee[] tieEmployees = {
                new Employee("Zayed", 5000),
                new Employee("Ali", 5000),
                new Employee("Bassem", 6000)
            };
            SortingTwo<Employee>.Sort(tieEmployees, (e1, e2) =>
            {
                if (e1.Salary == e2.Salary)
                    return string.Compare(e1.Name, e2.Name) > 0; // Sort by name if salary is equal
                return e1.Salary > e2.Salary; // Primary sort by salary
            });
            foreach (var emp in tieEmployees) Console.WriteLine(emp);
            /*
             * Question: What are the challenges and benefits of implementing multi-criteria sorting logic in generic methods?
             * Answer:
             * Benefits: Provides the ability to execute highly precise, multi-layered data organization required by complex business rules.
             * Challenges: The comparison logic (lambda) can become complex, harder to read, and difficult to maintain, especially when dealing with multiple "tie-breaker" conditions.
             */ 
            #endregion

            #region Problem 09
            // -----------------------------------------------------------------
            // Problem 9: GetDefault method handling value and reference types
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Problem 9 ---");
            Console.WriteLine($"Default int: {GetDefault<int>()}"); // Value Type (0)
            Console.WriteLine($"Default Employee: {(GetDefault<Employee>() == null ? "null" : "not null")}"); // Ref Type (null)
            /*
             * Question: Why is the default(T) keyword crucial in generic programming, and how does it handle value and reference types differently?
             * Answer:
             * It is crucial because the generic type 'T' is unknown at compile time, meaning we cannot safely return 'null' for an unknown type (it might be a struct).
             * `default(T)` dynamically resolves this by returning `null` for reference types, and the default zeroed value (e.g., 0 or false) for value types.
             */ 
            #endregion

            #region Problem 10
            // -----------------------------------------------------------------
            // Problem 10: Add constraint ICloneable to SortingAlgorithm<T>, demonstrate cloning
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Problem 10 ---");
            Employee original = new Employee("Hassan", 8000);
            Employee cloned = (Employee)original.Clone(); // Demonstrating cloning ability
            Console.WriteLine($"Original: {original}, Cloned: {cloned}");
            /*
             * Question: How do constraints in generic programming ensure type safety and improve the reliability of generic methods?
             * Answer:
             * Constraints (like `where T : ICloneable`) restrict the generic type to only those that implement specific interfaces or inherit from specific classes. 
             * This guarantees that interface methods (like `Clone()`) are safely available inside the generic class, preventing runtime errors.
             */ 
            #endregion

            #region Problem 11
            // -----------------------------------------------------------------
            // Problem 11: Delegate for string transformation
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Problem 11 ---");
            List<string> myStrings = new List<string> { "hello", "world" };

            var upperList = ApplyStringTransformation(myStrings, s => s.ToUpper());
            var reverseList = ApplyStringTransformation(myStrings, s => new string(s.Reverse().ToArray()));

            Console.WriteLine("Upper: " + string.Join(", ", upperList));
            Console.WriteLine("Reversed: " + string.Join(", ", reverseList));
            /*
             * Question: What are the benefits of using delegates for string transformations in a functional programming style?
             * Answer:
             * Delegates decouple the iteration logic (looping through the list) from the transformation logic. 
             * This allows for highly reusable functions where behavior is passed as an argument, enabling easy swaps between operations like uppercase, lowercase, or reversing.
             */ 
            #endregion

            #region Problem 12
            // -----------------------------------------------------------------
            // Problem 12: Delegate for math operations (2 ints -> int)
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Problem 12 ---");
            int a1 = 20, b1 = 5;
            Console.WriteLine($"Add: {PerformMathOperation(a1, b1, (x, y) => x + y)}");
            Console.WriteLine($"Sub: {PerformMathOperation(a1, b1, (x, y) => x - y)}");
            Console.WriteLine($"Mul: {PerformMathOperation(a1, b1, (x, y) => x * y)}");
            Console.WriteLine($"Div: {PerformMathOperation(a1, b1, (x, y) => x / y)}");
            /*
             * Question: How does the use of delegates promote code reusability and flexibility in implementing mathematical operations?
             * Answer:
             * Delegates allow a single wrapper function to execute any mathematical logic passed to it. 
             * This avoids writing separate methods for Add, Subtract, Multiply, etc., providing high flexibility to introduce new operations dynamically without changing existing code.
             */ 
            #endregion

            #region Problem 13
            // -----------------------------------------------------------------
            // Problem 13: Generic delegate T -> R transformation
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Problem 13 ---");
            List<int> intList = new List<int> { 1, 2, 3 };
            List<string> strList = TransformList(intList, x => $"Number: {x}");
            Console.WriteLine(string.Join(" | ", strList));
            /*
             * Question: What are the advantages of using generic delegates in transforming data structures?
             * Answer:
             * Generic delegates provide absolute type safety across different input and output types. 
             * They allow the creation of universal mapping functions (like LINQ's Select) that can transform any data type into any other data type cleanly.
             */ 
            #endregion

            #region Problem 14
            // -----------------------------------------------------------------
            // Problem 14: Func<T, TResult> to square integers
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Problem 14 ---");
            List<int> baseList = new List<int> { 2, 4, 6 };
            List<int> squaredList = ApplyFuncToList(baseList, x => x * x);
            Console.WriteLine($"Squared: {string.Join(", ", squaredList)}");
            /*
             * Question: How does Func simplify the creation and usage of delegates in C#?
             * Answer:
             * Func eliminates the need to manually declare custom delegate signatures (like `public delegate int MyDelegate(...)`). 
             * It provides built-in, ready-to-use, strongly-typed generic signatures for any method that returns a value.
             */ 
            #endregion

            #region Problem 15
            // -----------------------------------------------------------------
            // Problem 15: Action<T> to print strings
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Problem 15 ---");
            List<string> messages = new List<string> { "System Boot", "Loading...", "Ready" };
            ApplyActionToList(messages, msg => Console.WriteLine($"[LOG]: {msg}"));
            /*
             * Question: Why is Action preferred for operations that do not return values?
             * Answer:
             * Action clearly communicates intent to developers. When you see Action, you immediately know the delegate is executed purely for its side effects 
             * (like writing to a console, modifying state, or updating a UI), and it returns `void`.
             */ 
            #endregion

            #region Problem 16
            // -----------------------------------------------------------------
            // Problem 16: Predicate<T> to filter even numbers
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Problem 16 ---");
            List<int> mixedNumbers = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<int> evens = FilterWithPredicate(mixedNumbers, x => x % 2 == 0);
            Console.WriteLine($"Evens: {string.Join(", ", evens)}");
            /*
             * Question: What role do predicates play in functional programming, and how do they enhance code clarity?
             * Answer:
             * Predicates specifically represent boolean conditions (returning true or false). 
             * They enhance clarity by providing a standardized, highly readable way to inject filtering criteria, making the intention of "evaluating a condition" explicit.
             */ 
            #endregion

            #region Problem 17
            // -----------------------------------------------------------------
            // Problem 17: Filter strings with anonymous function
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Problem 17 ---");
            List<string> rawStrings = new List<string> { "Start", "Stop", "Continue", "Pause", "State" };

            // Starts with 'S'
            var startsWithS = FilterStringsWithCondition(rawStrings, delegate (string s) { return s.StartsWith("S"); });
            // Contains 'in'
            var containsIn = FilterStringsWithCondition(rawStrings, delegate (string s) { return s.Contains("in"); });

            Console.WriteLine($"Starts with 'S': {string.Join(", ", startsWithS)}");
            Console.WriteLine($"Contains 'in': {string.Join(", ", containsIn)}");
            /*
             * Question: How do anonymous functions improve code modularity and customization?
             * Answer:
             * They allow developers to define inline, single-use, specialized logic exactly where it is needed. 
             * This prevents cluttering a class with numerous minor helper methods, enhancing local customization without sacrificing modularity.
             */ 
            #endregion

            #region Problem 18
            // -----------------------------------------------------------------
            // Problem 18: Math operation with anonymous function
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Problem 18 ---");
            int mathRes1 = PerformOperationAnon(10, 5, delegate (int x, int y) { return x + y; });
            int mathRes2 = PerformOperationAnon(10, 5, delegate (int x, int y) { return x * y; });
            Console.WriteLine($"Anon Add: {mathRes1}, Anon Mul: {mathRes2}");
            /*
             * Question: When should you prefer anonymous functions over named methods in implementing mathematical operations?
             * Answer:
             * You should prefer them for short, localized, single-use operations. If the mathematical logic is only ever used in one specific place, 
             * an anonymous function keeps the logic closely bound to the context and reduces unnecessary global method definitions.
             */ 
            #endregion

            #region Problem 19
            // -----------------------------------------------------------------
            // Problem 19: Filter strings with lambda expression
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Problem 19 ---");
            List<string> wordList = new List<string> { "hi", "hello", "net", "web", "eye" };

            var lenGreaterThan3 = FilterStringsWithCondition(wordList, s => s.Length > 3);
            var containsE = FilterStringsWithCondition(wordList, s => s.Contains('e'));

            Console.WriteLine($"Length > 3: {string.Join(", ", lenGreaterThan3)}");
            Console.WriteLine($"Contains 'e': {string.Join(", ", containsE)}");
            /*
             * Question: What makes lambda expressions an essential feature in modern C# programming?
             * Answer:
             * Their extreme conciseness, powerful type inference, and semantic clarity make them essential. 
             * Furthermore, they form the absolute foundation of LINQ, allowing functional-style data querying and manipulation.
             */ 
            #endregion

            #region Problem 20
            // -----------------------------------------------------------------
            // Problem 20: Math operation on doubles using lambda expression
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Problem 20 ---");
            double d1 = 10.0, d2 = 3.0;
            double division = PerformDoubleOpLambda(d1, d2, (x, y) => x / y);
            double exponent = PerformDoubleOpLambda(d1, d2, (x, y) => Math.Pow(x, y));

            Console.WriteLine($"Division: {division:F2}");
            Console.WriteLine($"Exponent: {exponent:F2}");
            /*
             * Question: How do lambda expressions enhance the expressiveness of mathematical computations in C#?
             * Answer:
             * Lambdas replace verbose delegate instantiations with clean, direct mathematical formulas. 
             * `(x, y) => x / y` directly models a mathematical equation, keeping the code tightly focused on the actual computation rather than the language syntax.
             */
        } 
            #endregion



        #region Helper Methods for Problem 9 to 20
        // Problem 9

        public static T GetDefault<T>()
        {
            return default(T);
        }

        // Problem 11
        static List<string> ApplyStringTransformation(List<string> list, StringTransformerDelegate transformer)
        {
            var result = new List<string>();
            foreach (var item in list) result.Add(transformer(item));
            return result;
        }

        // Problem 12
        static int PerformMathOperation(int a, int b, MathOperationDelegate op)
        {
            return op(a, b);
        }

        // Problem 13
        static List<R> TransformList<T, R>(List<T> list, GenericTransformerDelegate<T, R> transformer)
        {
            var result = new List<R>();
            foreach (var item in list) result.Add(transformer(item));
            return result;
        }

        // Problem 14
        static List<int> ApplyFuncToList(List<int> list, Func<int, int> func)
        {
            var result = new List<int>();
            foreach (var item in list) result.Add(func(item));
            return result;
        }

        // Problem 15
        static void ApplyActionToList(List<string> list, Action<string> action)
        {
            foreach (var item in list) action(item);
        }

        // Problem 16
        static List<int> FilterWithPredicate(List<int> list, Predicate<int> predicate)
        {
            var result = new List<int>();
            foreach (var item in list)
            {
                if (predicate(item)) result.Add(item);
            }
            return result;
        }

        // Problem 17 & 19 (Shared helper for filtering strings)
        static List<string> FilterStringsWithCondition(List<string> list, Func<string, bool> condition)
        {
            var result = new List<string>();
            foreach (var item in list)
            {
                if (condition(item)) result.Add(item);
            }
            return result;
        }

        // Problem 18
        static int PerformOperationAnon(int a, int b, Func<int, int, int> op)
        {
            return op(a, b);
        }

        // Problem 20
        static double PerformDoubleOpLambda(double a, double b, Func<double, double, double> op)
        {
            return op(a, b);
        }
    }
} 
        #endregion