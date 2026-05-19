//AED7 출력

class ClassA
{
    ClassA()
    {
        System.out.print('A'); //A출력 
        this.prn(); //
    }

    void prn()
    {
        System.out.print('B');
    }
}

class ClassB extends ClassA
{
    ClassB()
    {
       super();// 부모 클래스의 생성자 호출 ClassA() 호출
       System.out.print('D');
    }

    void prn()
    {
        System.out.print('E');
    }
    void prn(int x)
    {
        System.out.print(x);
    }
}







public class Test14 {
    public static void main(String [] args)
    {
        int x = 7;
        ClassB cal = new ClassB();
        cal.prn(x);
    }
    
}
