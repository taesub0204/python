interface Op{
    int calc(int a, int b);
}
class Add implements Op{
    public int calc(int a, int b) {
        return a + b;
    }
}

class Subtract implements Op{
    public int calc(int a, int b) {
        return a - b;
    }
}
class Multiply implements Op{
    public int calc(int a, int b) {
        return a * b;
    }
}
public class Main{
    public static void main(String[] args) {
        int a = 10, b = 5;
        Op add = new Add();
        Op sub = new Subtract();
        Op mul = new Multiply();

        

        System.out.print(add.calc(a, b) + ",");
        System.out.print(sub.calc(a, b) + ",");
        System.out.print(mul.calc(a, b));
        
    }
}