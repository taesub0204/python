using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2_6_1
{
    internal class Program // 접근제어자
    {
        static void Main(string[] args)
        {

            Console.WriteLine("hellow");

            Student s1 = new Student();
            Student s2 = new Student();



            s1.Name = "홍길동";
            s2.Name = "김고은";
            s1.Year = 3;
            s1.Id = 20260001;
            Console.WriteLine($"{s1.Name}, {s1.Year}, {s1.Id}, {s1.Addr}");
            Console.WriteLine(s1.Addr);

            s1.Addr = "폴리텍대학";



            //s1.name = "홍길동"; // private 때문에 접근 안됨
            //s1.setName("홍길동");
            //s1.setYear(1);
            //s1.setId(20260000);
            //Console.WriteLine($"{s1.getName()}, {s1.getYear()}, {s1.getId()}" );
            s1.showinfo();
            s2.showinfo();



        }

        public class Student // 외부에서도 맘대로 슬 쑤 있음 public
        {

            //private string name; //main 도 private 변수 접근 안됨  그래서 get set 매서드로 접근해야댐
            //private int id;
            //private int year;
            //// 그래서 get set 매서드로 접근해야댐
            //// 캡슐약 뜯기 전에 무슨 색인지 모름 캠슐화, 정보은닉

            //public int getId()
            //{
            //    return id;
            //}

            //public int getYear()
            //{ 
            //    return year;
            //}

            //public string getName()
            //{ 
            //    return name;
            //}

            //public void setId(int id) // 지역변수 아이디
            //{ 
            //    this.id = id;
            //}

            //public void setYear(int year)
            //{ 
            //    this.year = year; // 안에서 지역변수
            //}


            //public void setName(string name)
            //{ 
            //    this.name = name;
            //}

            public string Name { get; set; } // 대문자 변수
            public int Year { get; set; }
            public int Id { get; set; }




            private string addr;
            //private int myVar;
            public string Addr
            {
                get {
                    if (addr != null)
                        return addr;
                    else return "";
                    }
                set { 
                    if(value != null)
                    addr = value; 
                    }
            }





            public void showinfo()
            {
                Console.WriteLine($"학생이름 : {Name}, 학년 : {Year}, 학번 : {Id}");
            }


        }





    }
}
