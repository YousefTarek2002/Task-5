using Exam02.Classes;

namespace Task_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Subject subject = new Subject(10, ".Net Programming");

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n===== Main Menu =====");
                Console.WriteLine("1. Admin ");
                Console.WriteLine("2. Student ");
                Console.WriteLine("3. Exit");
                Console.Write("Choose option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        if (!AdminLogin())
                        {
                            Console.WriteLine(" Authentication failed.");
                            break;
                        }
                        AdminMenu(subject);
                        break;

                    case "2":
                        if (subject.Exams.Count == 0)
                        {
                            Console.WriteLine(" No exams available. Please ask admin to add exams.");
                            break;
                        }

                        Console.Write("Enter your name: ");
                        string studentName = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(studentName))
                        {
                            Console.WriteLine(" Invalid name.");
                            break;
                        }

                        bool retake;
                        do
                        {
                            subject.ShowAvailableExams();
                            int examChoice;
                            do
                            {
                                Console.Write("Enter exam number to start: ");
                            } while (!int.TryParse(Console.ReadLine(), out examChoice) || examChoice < 1 || examChoice > subject.Exams.Count);

                            var exam = subject.Exams[examChoice - 1];
                            var result = exam.Conduct(subject.Name, studentName);

                            subject.Results.Add(result);
                            subject.PrintResultSummary(result, showReview: true);

                            Console.Write("Do you want to retake this exam? (Y/N): ");
                            string again = Console.ReadLine();
                            retake = again.Equals("Y", System.StringComparison.OrdinalIgnoreCase);
                            if (retake) Console.WriteLine(" Retaking the same exam...\n");
                        } while (retake);
                        break;

                    case "3":
                        exit = true;
                        Console.WriteLine(" Exiting program...");
                        break;

                    default:
                        Console.WriteLine(" Invalid choice, please try again.");
                        break;
                }
            }
        }

        static bool AdminLogin()
        {
            const string USER = "admin";
            const string PASS = "123";

            for (int i = 1; i <= 3; i++)
            {
                Console.Write("Admin username: ");
                string u = Console.ReadLine();
                Console.Write("Admin password: ");
                string p = Console.ReadLine();

                if (u == USER && p == PASS)
                {
                    Console.WriteLine(" Login successful.");
                    return true;
                }
                Console.WriteLine($" Wrong credentials (Attempt {i}/3).");
            }
            return false;
        }

        static void AdminMenu(Subject subject)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n--- Admin Menu ---");
                Console.WriteLine("1. Add Exam");
                Console.WriteLine("2. View Results (Summary)");
                Console.WriteLine("3. Back");
                Console.Write("Choose: ");
                string c = Console.ReadLine();
                switch (c)
                {
                    case "1":
                        string cont;
                        do
                        {
                            subject.CreateExam();
                            Console.Write("Do you want to add another exam? (Y/N): ");
                            cont = Console.ReadLine();
                        } while (cont.Equals("Y", System.StringComparison.OrdinalIgnoreCase));
                        Console.WriteLine(" Exams created successfully!");
                        break;

                    case "2":
                        subject.ShowResultsSummary();
                        break;

                    case "3":
                        back = true;
                        break;

                    default:
                        Console.WriteLine(" Invalid choice.");
                        break;
                }
            }
        }

    }
}
