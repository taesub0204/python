using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp06_14
{
    internal class Program
    {
        class Test // 내부 자식클래스
        { 
         public void TestMethod() 
             {
                Program.Main(new string[] { "" });
             }
        }

        public void TestMethod()  // 자신의 클래스가진  private 메서드 접근
        {
            Program.Main(new string[] { ""});
        }




        static void Main(string[] args)
        {
        }
    }
}
