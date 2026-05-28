public class Main3{
    static class A{
        static String nu(){return "A";}
        String mo(){return "a";}

    }
    static class B extends A{
        static String nu(){return "B";}
        String mo(){return "b";}

}


    public static void main(String[] args){
        A tester = new B();
        System.out.println(tester.nu()+tester.mo());
    }
    
}
