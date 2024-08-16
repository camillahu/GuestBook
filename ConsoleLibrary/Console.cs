using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook
{
    public static class Console
    {
        public static void PrintStringToConsole(this string message)
        {
           System.Console.WriteLine(message);
        }

        public static string RequestUIString(this string message)
        {
            var input = "";

            while (string.IsNullOrWhiteSpace(input))
            {
                System.Console.Write(message);
                input = System.Console.ReadLine();
            }

            return input;
        }

        public static int RequestUIInt(this string message)
        {
            var input = "";
            var output = 0;
            bool isValidInput = false;

            while (string.IsNullOrWhiteSpace(input) || !isValidInput)
            {
                System.Console.Write(message);
                input = System.Console.ReadLine();
                isValidInput = int.TryParse(input, out output);

                if (string.IsNullOrWhiteSpace(input) || !isValidInput)
                {
                    "Invalid input, please try again".PrintStringToConsole();
                } //denne funker ikke helt optimalt
            }

            return output;
        }
    }
}
