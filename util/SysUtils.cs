using System;

namespace Utils{
    public class SysUtils {
        public static void ThrowError(string errorMesaage, Exception e = null) {
            Console.WriteLine(errorMesaage);
            Console.ReadLine();
        }
    }
}