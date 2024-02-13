using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuDB
{
    public enum YearRank
    {
        Freshman = 1,
        Sophomore = 2,
        Junior = 3,
        Senior = 4
    }
    // class to hold the props and methods that are specific to an undergrad
    internal class Undergrad : Student
    {
        // properties that define what an undergrad "knows" about itself
        public string DegreeMajor { get; set; }
        public YearRank Rank { get; set; }

        // ctors - how to make undergrad objects
        public Undergrad(string first, string last, double gpa, string email,
                            YearRank year, string major)
            : base(first, last, gpa, email)
        {
            Rank = year;
            DegreeMajor = major;
        }

        // override any output to string methods for student so that 
        // an undergrad knows how to print itself out (either to the shell or file)

        // for the user of the database
        public override string ToString()
        { 
            return base.ToString() + $" Year: {Rank}\nMajor: {DegreeMajor}";
        }

        public override string ToStringForOutputFile()
        {
            // print the class name info
            string str = this.GetType().Name + "\n";
            return str + base.ToStringForOutputFile() + $"{Rank}\n{DegreeMajor}";
        }
    }
}
