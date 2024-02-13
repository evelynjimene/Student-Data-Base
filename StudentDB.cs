using System.Collections.Generic;
using System;
using System.IO;
using System.Globalization;
using System.Threading;

namespace StuDB
{
    internal class StudentDB
    {
        public const bool _DEBUG_MODE_ONLY_ = false;
        private List<Student> students = new List<Student>();

        public StudentDB()
        {
            if (_DEBUG_MODE_ONLY_) TestMain();
            ReadDataFromInputFile();
        }

        public void GoDatabase()
        {
            // read in the data from the storage
            // store it in the List
           string email = string.Empty;

            while (true)
            {
                // display a menu to the user for selections or
                // user indicates quit application
                // CRUD operations - 
                DisplayMainMenu();
                Console.Write("ENTER selection: ");
                char selection = GetUserSelection();

                switch (selection)
                {
                    case 'A':
                    case 'a':
                        break;
                    case 'E':
                    case 'e':
                        ModifyStudentRecord();
                        break;
                    case 'F':
                    case 'f':
                        FindStudentRecord(out email);
                        break;
                    case 'P':
                    case 'p':
                        PrintOutAllRecords();
                        break;
                    case 'Q':
                    case 'q':
                    case 'X':
                    case 'x':
                        QuitAppAfterSave();
                        break;
                    default:
                        Console.WriteLine("ERROR: " +
                            "That wasn't an allowable action in the database");
                        break;
                }
            }

        }

        private void ModifyStudentRecord()
        {
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);

            if (stu != null)
            {

                ModifyStudent(stu);
            }
            else
            {
                Console.WriteLine($"******** RECORD NOT FOUND.\nCan't edit record for user: {email}");
            }
        }

        private void ModifyStudent(Student stu)
        {
            string studentType = stu.GetType().Name;
            Console.WriteLine(stu);
            Console.WriteLine($"Editing student type: {studentType}");

            DisplayModifyMenu();
            char selection = GetUserSelection();

            if (studentType == "Undergrad")
            {
                Undergrad undergrad = stu as Undergrad;
                switch (selection)
                {
                    case 'Y':
                    case 'y':
                        Console.Write("\nENTER new year/rank in school from the following choices. ");
                        Console.Write("[1] Freshman, [2] Sophomore, [3] Junior, [4] Senior: ");
                        undergrad.Rank = (YearRank)int.Parse(Console.ReadLine());
                        break;
                    case 'D':
                    case 'd':
                        Console.Write("\nENTER the new degree major: ");
                        undergrad.DegreeMajor = Console.ReadLine();
                        break;
                }
            }
            else if (studentType == "GradStudent")
            {
                GradStudent grad = stu as GradStudent;
                switch (selection)
                {
                    case 'T':
                    case 't':
                        Console.Write("\nENTER the new tuition credit amount: ");
                        grad.TuitionCredit = decimal.Parse(Console.ReadLine());
                        break;
                    case 'A':
                    case 'a':
                        Console.Write("\nENTER the new advisors name: ");
                        grad.FacultyAdvisor = Console.ReadLine();
                        break;
                }
            }

            // for the student part of the object

            switch (selection)
            {
                case 'F':
                case 'f':
                    Console.Write("\nENTER the new first name: ");
                    stu.FirstName = Console.ReadLine();
                    break;
                case 'L':
                case 'l':
                    Console.Write("\nENTER the new last name: ");
                    stu.LastName = Console.ReadLine();
                    break;
                case 'G':
                case 'g':
                    Console.Write("\nENTER the new grade pt average: ");
                    stu.GradePtAvg = double.Parse(Console.ReadLine());
                    break;
                default:
                    break;
            }
            Console.WriteLine($"\nEDIT operation complete. Current record info:\n{stu}\nPress any letter to continue. ");
            Console.ReadKey();
        }

        private void DisplayModifyMenu()
        {
            Console.WriteLine(@"
            ***********************************************
            ********* Modify Student Record Menu *********
            ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            [F]irst name
            [L]ast name
            [G]rade Pt Avg
            [Y]ear in school
            [D]egree major
            [T]uition credit
            Faculty [A]dvisor
            ** Email address can never be modified. See admin.
            ");
        }

        private Student FindStudentRecord(out string email)
        {
            Console.Write("\nEnter the email address of the student to search: ");
            email = Console.ReadLine();

            foreach (var stu in students) 
            {
                if (email == stu.EmailAddress)
                {
                    Console.WriteLine($"FOUND email address: {stu.EmailAddress}");
                    return stu;
                }
            }
            //arrive here somehow without finding
            Console.WriteLine($"{email} NOT FOUND!");
            return null;
        }

        private char GetUserSelection()
        {
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            return keyPressed.KeyChar;
        }

        private void PrintOutAllRecords()
        {
            foreach (Student stu in students)
            {
                Console.WriteLine(stu);
            }
        }

        // more will be added when we figure out how to save
        private void QuitAppAfterSave()
        {
            SaveAllRecordsToOutputFile();
            Environment.Exit(0);
        }

        private const string STUDENT_DATA_OUTPUTFILE = "STUDENT_DATA_OUTPUTFILE.txt";
        private const string STUDENT_DATA_INPUTFILE = "STUDENT_DATA_INPUTFILE.txt";

        private void ReadDataFromInputFile()
        {
            StreamReader inFile = new StreamReader(STUDENT_DATA_INPUTFILE);

            string studentType = string.Empty;
            
            while ((studentType = inFile.ReadLine()) != null)
            {
                // gather all the data for a new undergrad
                string first = inFile.ReadLine();
                string last = inFile.ReadLine();
                double gpa = double.Parse(inFile.ReadLine());
                string email = inFile.ReadLine();

                if (studentType == "Undergrad")
                {
                    YearRank year = (YearRank)Enum.Parse(typeof(YearRank), inFile.ReadLine());
                    string major = inFile.ReadLine();

                    Undergrad stu = new Undergrad(first, last, gpa, email, year, major);
                    students.Add(stu);
                }
                else if (studentType == "GradStudent")
                {
                    decimal credit = decimal.Parse(inFile.ReadLine());
                    string advisor = inFile.ReadLine();

                    students.Add(new GradStudent(first, last, gpa, email, credit, advisor));
                }
                else
                {
                    Console.WriteLine($"ERROR: {studentType} is not a known student type!");
                }
                
            }
                // close the resource once, when all reading is done
                inFile.Close();
        }
        private void SaveAllRecordsToOutputFile()
        {
            // create the output file
            StreamWriter outFile = new StreamWriter(STUDENT_DATA_OUTPUTFILE);

            // write to the output file
            foreach (Student stu in students)
            {
                outFile.WriteLine(stu.ToStringForOutputFile());
                Console.WriteLine(stu.ToStringForOutputFile());
                //Console.WriteLine(stu);

            }

            // close the output file
            outFile.Close();
        }

        private void DisplayMainMenu()
        {
            Console.WriteLine(@"
            ************************************
            **** Student DataBase Main Menu ****
            ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            [A]dd a new student record
            [E]dit an existing student record
            [D]elete a student record
            [F]ind a student in the database
            [P]rint out all records
            [Q]uit the app after saving
            ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            ");
        }
        public void TestMain()
        {

            // creates 3 test students
            Student stu1 = new Student("Alice", "Anderson", 3.9, "aanderson@uw.edu");
            Student stu2 = new Student("Bob", "Bradshaw", 3.7, "bbradshaw@uw.edu");
            Student stu3 = new Student("Carlos", "Castaneda", 3.5, "ccastaneda@uw.edu");

            // put the students into the list
            students.Add(stu1);
            students.Add(stu2);
            students.Add(stu3);
            students.Add(new Undergrad("David", "Jim", 2.5, "djim@uw.edu",
                                        YearRank.Junior, "BSIT"));


            // print them back out

            //Console.WriteLine(stu1);
            //Console.WriteLine(stu2);
            //Console.WriteLine(stu3);

        }
    }
}