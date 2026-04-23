using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp04_33
{
    internal class Program
    {
        //static bool state = true; // true: 켜짐, false: 꺼짐
        static void Main(string[] args)
        {
            bool state = true; // true: 켜짐, false: 꺼짐
            while (state)
            {
                Console.WriteLine("방향키를 입력하세요.");
                ConsoleKeyInfo info = Console.ReadKey();

                switch (info.Key)
                {
                    case ConsoleKey.UpArrow:
                        Console.WriteLine("위쪽으로 이동");
                        break;

                    case ConsoleKey.RightArrow:
                        Console.WriteLine("오른쪽으로 이동");
                        break;
                    case ConsoleKey.LeftArrow:
                        Console.WriteLine("왼쪽으로 이동");
                        break;
                    case ConsoleKey.DownArrow:
                        Console.WriteLine("아래쪽으로 이동");
                        break;
                    case ConsoleKey.X:
                        state = false;
                        Console.WriteLine("종료");
                        break;

                    default:
                        Console.WriteLine("다른 키를 눌렀습니다.");
                        break;







                }

                }
            }
        }
}
