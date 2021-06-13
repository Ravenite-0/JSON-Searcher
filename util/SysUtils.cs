using System;

namespace Utils {
    //This class handles system-related tasks such as errors and exceptions.
    public class SysUtils {
        public static void ThrowError(string errorMesaage, Exception e = null) {
            Console.WriteLine(errorMesaage);
            Console.ReadLine();
        }
    }
}