using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuDB
{
    internal class GradStudent : Student
    {
        // credit back on tuition bills for teaching
        public decimal TuitionCredit { get; set; }

        // fac advisor is assigned to do research - thesis
        public string FacultyAdvisor { get; set; }


        // need some subclass ctors

        public GradStudent(string first, string last, double gpa, string email,
                            decimal credit, string advisor)
            : base(first, last, gpa, email)
        {
            TuitionCredit = credit;
            FacultyAdvisor = advisor;
        }

        // override any output to string methods for student so that 
        // an undergrad knows how to print itself out (either to the shell or file)

        // for the user of the database
        // override the to string method(s)
        public override string ToString()
        {
            return base.ToString() + $" Credit: {TuitionCredit:C}\nAdvisor: {FacultyAdvisor}";
        }

        public override string ToStringForOutputFile()
        {
            // print the class name info
            string str = this.GetType().Name + "\n";
            return str + base.ToStringForOutputFile() + $"{TuitionCredit}\n{FacultyAdvisor}";
        }

    }
}
