public class Test12 {
    public static void main(String [] args)
    {
        int number = 1234;
        int div =10, result = 0;

        while(number > 0)
        {
            result = result * div;
            result = result + (number % div);
            number = number / div;
        }
        System.out.printf("%d", result);
    }
    
}
