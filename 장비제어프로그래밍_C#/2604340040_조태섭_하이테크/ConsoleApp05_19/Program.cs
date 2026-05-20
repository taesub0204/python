using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp05_19
{
    internal class Program
    {




        static void Main(string[] args)
        {
            List<Student> list = new List<Student>();
            list.Add(new Student() { name = "윤인성", grade = 1 });
            list.Add(new Student() { name = "연하진", grade = 2 });
            list.Add(new Student() { name = "윤아린", grade = 3 });
            list.Add(new Student() { name = "윤명월", grade = 4 });
            list.Add(new Student() { name = "구지연", grade = 1 });
            list.Add(new Student() { name = "김연화", grade = 2 });

            foreach (var item in list)
            {
                Console.WriteLine(item.name + " : " + item.grade);
            }
            Console.WriteLine();

            foreach (var item in list)
                if (item.grade > 1)
                {
                    Console.WriteLine(item.name + " : " + item.grade);
                }

            //try
            //{
            //    foreach (var item in list)
            //        if (item.grade > 1)
            //        {
            //            list.Remove(item);
            //        }
            //}
            //catch (InvalidOperationException ex)
            //{
            //    Console.WriteLine("예외발생" + ex.Message);
            //}
            //Console.WriteLine();


            // 반복문으로 요소제거
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].grade > 1)
                {
                    list.RemoveAt(i);

                }

            }
            Console.WriteLine();
            foreach (var item in list)
            {
                Console.WriteLine(item.name + " : " + item.grade);
            }

            // 반목분요소제거 출력
            Console.WriteLine();

            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i].grade > 1)
                { 
                    list.RemoveAt(i);
                }
            }
            Console.WriteLine();
            foreach (var item in list)
            {
                Console.WriteLine(item.name + " : " + item.grade);
            }




        }
    }
}
