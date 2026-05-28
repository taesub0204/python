class Test1{
    Test1(){
        System.out.println("x");
    }
    Test1(char a){
        this();
        System.out.println("a");
    }
}
class Test2 extends Test1{
    Test2(){
        super();
        System.out.println("y");
    }
    Test2(char a){
        this();
        System.out.println("a");
    }
}





public class Test20 {
    public static void main(String[] args){
        Test1 t1 = new Test2('z');
    }
}
