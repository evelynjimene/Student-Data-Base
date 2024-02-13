using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // when we want to test
            // TestMain();
            StudentDB app = new StudentDB();
            app.GoDatabase();
        }
    }
}
