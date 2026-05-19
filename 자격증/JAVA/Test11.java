public class Test11 {
    public static void main(String [] args)
    {
        int aa[][] = {{45, 50, 75},{89}};
        System.out.println(aa[0].length);
        System.out.println(aa[1].length);
        System.out.println(aa[0][0]);
        System.out.println(aa[0][1]);
        System.out.println(aa[0][2]);
        System.out.println(aa[1][0]);
        // System.out.println(aa[1][1]); // This line would cause an ArrayIndexOutOfBoundsException

    }
}
