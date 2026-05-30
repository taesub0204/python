public class Main2{
    public static void main(String[] args)
    {
        String t1 = new String("ASDF"); //메모리에 참조 되는 주소 위치비교
        String t2 = new String("ASDF");
        if(t1 == t2)
            System.out.print(t1);
        else
            System.out.print(t1+t2);

    }
}