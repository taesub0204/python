using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    internal class Cat : Animal // 자바에서는 extends, C#에서는 : 으로 상속을 표현한다.
    {
       // public int Age { get; set; }
        public Cat() { this.Age = 0; }

       // public void Eat() { Console.WriteLine(" 냠냠 먹습니다."); }
       // public void Sleep() { Console.WriteLine("쿨쿨 잠을 잡니다."); }
        public void Meow() { Console.WriteLine("냥냥 웁니다."); }


    }
}
