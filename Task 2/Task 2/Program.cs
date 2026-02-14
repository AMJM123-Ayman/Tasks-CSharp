using System;
using System.Runtime.InteropServices;

namespace CsharpDay02Giza01
{
    class Program
    {
       
    


        class Person
        {
            public string Name;
        }
        static void Main()
        {
            #region problem1

            // Declare variable x and assign value 10
            int x = 10;

            /* 
             Declare variable y
             and assign value 20 
            */
            int y = 20;

            // Calculate the sum of x and y
            int sum = x + y;

            // Print the result
            Console.WriteLine(sum);

            // Comments
            // single line comment
            /*
             multi line comment 
             */
            // cltr + K > C
            // cltr + K > U
            #endregion

            #region Problem2
            int A = 10;
            int S = 5;
            Console.WriteLine(A + S);

            //Runtime vs Logical Error 
            //Runtime Error : This happens while the program is running
            //Example: Division by zero 
            //Logical Error:The program works, but the result is wrong.Example: It calculates a total instead of an average

            #endregion

            #region Problem3
            // Full Name
            Console.Write("Enter your full name: ");
            string FullName = Console.ReadLine();

            // Age
            Console.Write("Enter your age: ");
            int Age = int.Parse(Console.ReadLine());

            // Monthly Salary
            Console.Write("Enter your monthly salary: ");
            double MonthlySalary = double.Parse(Console.ReadLine());

            // Is Student
            Console.Write("Are you a student? (true/false): ");
            bool IsStudent = bool.Parse(Console.ReadLine());

            // Display values
            Console.WriteLine("\n--- User Information ---");
            Console.WriteLine("Full Name: " + FullName);
            Console.WriteLine("Age: " + Age);
            Console.WriteLine("Monthly Salary: " + MonthlySalary);
            Console.WriteLine("Is Student: " + IsStudent);

            //Naming Conventions 
            // - بتخلي الكود مقروء وسهل الفهم
            // - موحّدة بين المبرمجين
            // - بتتبع المعايير الرسمية للغة C#
            // - بتقلل الأخطاء وبتحسّن جودة الكود

            #endregion

            #region Problem4
            Person p1 = new Person();
            p1.Name = "Ayman";

            Person p2 = p1;
            p2.Name = "Ahmed";

            Console.WriteLine(p1.Name); // Ahmed

            // الفرق في الذاكرة:
            // Value Type: بيتم تخزينه مباشرة في الـ Stack
            // Reference Type: المرجع في الـ Stack
            // والكائن نفسه في الـ Heap

            #endregion

            #region Problem5
            int xC = 15, yC = 4;

            Console.WriteLine(xC + yC);
            Console.WriteLine(xC - yC);
            Console.WriteLine(xC * yC);
            Console.WriteLine(xC / yC);
            Console.WriteLine(xC % yC);
            int aS = 2, bS = 7;
            Console.WriteLine(aS % bS);
            #endregion

            #region Problem6
            int num = 12;

            if (num > 10 && num % 2 == 0)
            {
                Console.WriteLine("Valid Number");
            }
            // الفرق بين && و &:
            // && (Logical AND): لو الشرط الأول غلط، الشرط التاني مش بيتنفّذ
            // &  (Bitwise AND): بينفّذ الشرطين دايمًا حتى لو الأول غلط

            #endregion

            #region Problem7
            double d = 9.7;

            int a = (int)d;     // Explicit
            double b = a;      // Implicit

            Console.WriteLine(a);
            Console.WriteLine(b);
            // نستخدم Explicit Casting لأن التحويل من double إلى int
            // يؤدي إلى فقدان جزء من البيانات (الكسور)

            #endregion

            #region Problem8
            try
            {
                Console.Write("Enter age: ");
                string input = Console.ReadLine();

                int age = int.Parse(input);

                if (age > 0)
                    Console.WriteLine("Valid age");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input");
            }
            #endregion

            #region Problem9
            int xQ = 5;
            int yQ = ++x + x++;
            #endregion







        }
    }
}
