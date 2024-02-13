namespace StuDB
{
    internal class Student //: object
    {
        // storage for the attributes having to do with a student obj
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double GradePtAvg { get; set; }
        public string EmailAddress { get; set; }

        // fully specified ctor - all parameters are passed into the obj
        public Student(string firstName, string lastName, double gpa, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            GradePtAvg = gpa;
            EmailAddress = email;
        }

        // default ctor in replaced - no arg
        public Student()
        { 
            // do nothing
        }

        // converts a student to a string output for 
        // writing the student to the output datafile
        public virtual string ToStringForOutputFile()
        {
            string str = $"{FirstName}\n";
            str += $"{LastName}\n";
            str += $"{GradePtAvg:F2}\n";
            str += $"{EmailAddress}\n";

            return str;

        }
        public override string ToString()
        {
            string str = "\n*************************\n";
            str += $"First: {FirstName}\n";
            str += $" Last: {LastName}\n";
            str += $"  GPA: {GradePtAvg:F2}\n";
            str += $"Email: {EmailAddress}\n";

            return str;

        }
    }
}