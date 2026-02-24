using System;
using System.Collections.Generic;
using System.IO;

namespace ExaminationSystem
{
    // ---------------------------------------------------------
    // 1. الأساسيات والواجهات (Interfaces & Basic Classes)
    // ---------------------------------------------------------

    // كلاس لتمثيل الإجابة
    public class Answer : ICloneable
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }

        public Answer(int id, string text)
        {
            AnswerId = id;
            AnswerText = text;
        }

        public override string ToString()
        {
            return $"{AnswerId}. {AnswerText}";
        }

        public object Clone()
        {
            return new Answer(this.AnswerId, this.AnswerText);
        }
    }

    public class AnswerList : List<Answer>
    {
        // قائمة الإجابات
    }

    // ---------------------------------------------------------
    // 2. نظام الأسئلة (The Question Hierarchy)
    // ---------------------------------------------------------

    public abstract class Question : ICloneable, IComparable<Question>
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public int Marks { get; set; }
        public AnswerList AnswerList { get; set; } // الخيارات المتاحة
        public Answer CorrectAnswer { get; set; } // الإجابة الصحيحة

        public Question()
        {
            AnswerList = new AnswerList();
        }

        public abstract void Display(); // دالة لعرض السؤال تختلف حسب نوعه

        // تطبيق ToString كما هو مطلوب
        public override string ToString()
        {
            return $"[{Header}] Mark({Marks}): {Body}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Question q)
                return this.Header == q.Header && this.Body == q.Body;
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Header, Body, Marks);
        }

        public int CompareTo(Question other)
        {
            // المقارنة بناء على الدرجات
            if (other == null) return 1;
            return this.Marks.CompareTo(other.Marks);
        }

        public abstract object Clone();
    }

    public class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion()
        {
            Header = "True / False Question";
        }

        public override void Display()
        {
            Console.WriteLine(this.ToString());
            foreach (var ans in AnswerList)
            {
                Console.WriteLine(ans);
            }
        }

        public override object Clone()
        {
            // تنفيذ النسخ (Deep Copy بشكل مبسط)
            var q = new TrueFalseQuestion { Header = this.Header, Body = this.Body, Marks = this.Marks, CorrectAnswer = this.CorrectAnswer };
            q.AnswerList.AddRange(this.AnswerList); // نسخ القائمة
            return q;
        }
    }

    public class ChooseOneQuestion : Question
    {
        public ChooseOneQuestion()
        {
            Header = "Choose One Answer";
        }

        public override void Display()
        {
            Console.WriteLine(this.ToString());
            foreach (var ans in AnswerList)
            {
                Console.WriteLine(ans);
            }
        }

        public override object Clone()
        {
            var q = new ChooseOneQuestion { Header = this.Header, Body = this.Body, Marks = this.Marks, CorrectAnswer = this.CorrectAnswer };
            q.AnswerList.AddRange(this.AnswerList);
            return q;
        }
    }

    // ---------------------------------------------------------
    // 3. قائمة الأسئلة المخصصة (Question List with File Logging)
    // ---------------------------------------------------------

    public class QuestionsList : List<Question>
    {
        private string _logFileName;

        public QuestionsList(string fileName)
        {
            _logFileName = fileName;
        }

        // استخدام 'new' لإخفاء دالة Add الأصلية وإضافة منطق التسجيل
        public new void Add(Question item)
        {
            base.Add(item);
            LogQuestion(item);
        }

        private void LogQuestion(Question q)
        {
            try
            {
                // فتح الملف وإضافة النص في النهاية (Append)
                using (StreamWriter sw = File.AppendText(_logFileName))
                {
                    sw.WriteLine($"Log Time: {DateTime.Now} | Added Question: {q.ToString()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
    }

    // ---------------------------------------------------------
    // 4. الأحداث والمواد الدراسية (Events, Subjects & Students)
    // ---------------------------------------------------------

    public enum ExamMode { Starting, Queued, Finished }

    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Exam Exam { get; set; }

        // الحدث (Event) لإشعار الطلاب
        public event EventHandler<string> ExamStartedEvent;

        public Subject(int id, string name)
        {
            SubjectId = id;
            SubjectName = name;
        }

        // دالة لإنشاء الامتحان وتعيينه للمادة
        public void CreateExam(Exam exam)
        {
            this.Exam = exam;
            // ربط تغيير وضع الامتحان بإشعار الطلاب
            // عندما يتغير وضع الامتحان إلى Starting، سنقوم باستدعاء الحدث في المادة
            this.Exam.OnModeChanged += (mode) =>
            {
                if (mode == ExamMode.Starting)
                {
                    OnExamStarted();
                }
            };
        }

        protected virtual void OnExamStarted()
        {
            Console.WriteLine($"\n[Notification System]: Exam for subject '{SubjectName}' is starting!");
            // استدعاء الحدث لإبلاغ جميع المشتركين (الطلاب)
            ExamStartedEvent?.Invoke(this, $"Attention Students: The exam for {SubjectName} has started!");
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Student(int id, string name)
        {
            Id = id;
            Name = name;
        }

        // الدالة التي تستجيب للحدث (Event Handler)
        public void ConfirmExamStart(object sender, string message)
        {
            Console.WriteLine($"Student {Name} received notification: {message}");
        }
    }

    // ---------------------------------------------------------
    // 5. نظام الامتحانات (The Exam Hierarchy)
    // ---------------------------------------------------------

    public abstract class Exam
    {
        public int TimeMinutes { get; set; }
        public int NumberOfQuestions { get; set; }
        public Subject AssociatedSubject { get; set; }

        // القاموس المطلوب لربط السؤال بالإجابات (للتصحيح)
        public Dictionary<Question, Answer> ExamCorrectionMap { get; set; }
        public QuestionsList Questions { get; set; }

        // إدارة وضع الامتحان والأحداث
        private ExamMode _mode;
        public Action<ExamMode> OnModeChanged; // Delegate داخلي لإبلاغ المادة

        public ExamMode Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                // إطلاق إشعار عند تغيير الوضع
                OnModeChanged?.Invoke(_mode);
            }
        }

        public Exam(int time, int numQuestions)
        {
            TimeMinutes = time;
            NumberOfQuestions = numQuestions;
            ExamCorrectionMap = new Dictionary<Question, Answer>();
            Mode = ExamMode.Queued; // الوضع الافتراضي
        }

        public abstract void ShowExam();
    }

    public class PracticeExam : Exam
    {
        public PracticeExam(int time, int numQuestions) : base(time, numQuestions) { }

        public override void ShowExam()
        {
            Console.WriteLine("\n--- Practice Exam Started ---");
            int score = 0;
            int totalMarks = 0;

            foreach (var question in Questions)
            {
                question.Display();
                Console.Write("Enter your answer ID: ");
                // (محاكاة لإدخال المستخدم)
                // في التطبيق الحقيقي سنقرأ الإدخال، هنا سنفترض حلاً للعرض فقط
                Console.WriteLine("[User inputs answer...]");

                Console.WriteLine($"Correct Answer is: {question.CorrectAnswer.AnswerText}");
                Console.WriteLine("-----------------------------");
                totalMarks += question.Marks;
            }
            Console.WriteLine("Practice Exam Finished. Answers shown immediately.");
        }
    }

    public class FinalExam : Exam
    {
        public FinalExam(int time, int numQuestions) : base(time, numQuestions) { }

        public override void ShowExam()
        {
            Console.WriteLine("\n--- Final Exam Started ---");
            foreach (var question in Questions)
            {
                question.Display();
                Console.Write("Your Answer: ");
                Console.WriteLine(" [Recorded]");
                Console.WriteLine("-----------------------------");
            }
            Console.WriteLine("Final Exam Finished. Results will be published later.");
        }
    }

    // ---------------------------------------------------------
    // 6. البرنامج الرئيسي (Main Program)
    // ---------------------------------------------------------

    class Program
    {
        static void Main(string[] args)
        {
            // 1. إنشاء مادة وطلاب
            Subject csharpSubject = new Subject(101, "C# Programming");
            Student s1 = new Student(1, "Ahmed");
            Student s2 = new Student(2, "Mona");

            // تسجيل الطلاب في حدث بدء الامتحان (الاشتراك في الإشعار)
            csharpSubject.ExamStartedEvent += s1.ConfirmExamStart;
            csharpSubject.ExamStartedEvent += s2.ConfirmExamStart;

            // 2. إعداد الأسئلة
            // ملاحظة: يتم تمرير اسم ملف السجل للقائمة
            QuestionsList qList = new QuestionsList("ExamLog.txt");

            // سؤال 1: صح وخطأ
            var q1 = new TrueFalseQuestion { Body = "C# supports multiple inheritance for classes?", Marks = 5 };
            q1.AnswerList.Add(new Answer(1, "True"));
            q1.AnswerList.Add(new Answer(2, "False"));
            q1.CorrectAnswer = q1.AnswerList[1]; // False

            // سؤال 2: اختيار
            var q2 = new ChooseOneQuestion { Body = "Which keyword is used to inherit a class?", Marks = 5 };
            q2.AnswerList.Add(new Answer(1, "this"));
            q2.AnswerList.Add(new Answer(2, "extends")); // Java uses this
            q2.AnswerList.Add(new Answer(3, ":")); // C# uses this
            q2.CorrectAnswer = q2.AnswerList[2];

            // إضافة الأسئلة (سيتم تفعيل LogQuestion هنا تلقائياً)
            qList.Add(q1);
            qList.Add(q2);

            // 3. سؤال المستخدم عن نوع الامتحان
            Console.WriteLine("Please select Exam Type: (1) Practice Exam, (2) Final Exam");
            string userChoice = "1"; // سنفترض أن المستخدم اختار 1 للتجربة، يمكنك استخدام Console.ReadLine()

            Exam exam;
            if (userChoice == "1")
            {
                exam = new PracticeExam(60, 2);
            }
            else
            {
                exam = new FinalExam(60, 2);
            }

            // ربط الأسئلة بالامتحان
            exam.Questions = qList;

            // ربط الامتحان بالمادة (مهم جداً لتفعيل الأحداث)
            csharpSubject.CreateExam(exam);

            // 4. بدء الامتحان (اختبار الحدث)
            Console.WriteLine("\nTeacher is changing exam mode to 'Starting'...");

            // *هنا السطر السحري*: تغيير الوضع سيطلق الحدث، والمادة ستخبر الطلاب
            exam.Mode = ExamMode.Starting;

            // 5. عرض الامتحان
            exam.ShowExam();

            // إظهار مكان ملف السجل
            Console.WriteLine($"\n[System]: Questions have been logged to 'ExamLog.txt' in the execution folder.");
        }
    }
}