using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pronounceable_Password_Test.JoelTheCoder;
using Zaretto.PasswordGenerator;

namespace Pronounceable_Password_Test
{
    class Program
    {
        delegate string GetPassword(int length);

        static void Main(string[] args)
        {
            var yetAnotherChrisSeed = 0;
            var yetAnotherChris = new YetAnotherChris.PronounceablePasswordGenerator();
                GenerateSample(i =>
                {
                    yetAnotherChrisSeed ++;
                    return yetAnotherChris.Generate(1, i, yetAnotherChrisSeed).First();
                }, 
                "Yet Another Chris", 
                "http://www.anotherchris.net/csharp/pronounceable-password-generator/");

            GenerateSample(
                i => RandomPassword.Generate((int)Math.Floor((double)i/3), 0, 0).ToLower(),
                "Joel The Coder",
                "https://joelthecoder.wordpress.com/2009/01/06/generating-random-human-readable-passwords/");
            
            GenerateSample(
               i => Creator.Generate(i),
               "ReadablePasswordGenerator",
               "https://www.nuget.org/packages/ReadablePasswordGenerator/");

            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }
        

        private static void GenerateSample(GetPassword generator, string name, string url)
        {
            Console.WriteLine();
            Console.WriteLine(name);
            Console.WriteLine(url);
            Console.WriteLine();

            // xxxx-xxxx-xxxx-xxxx
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"{generator(4)}-{generator(4)}-{generator(4)}-{generator(4)}");
            }

            // 8 long
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(generator(8));
            }
            
            // 15 long
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(generator(15));
            }

            //Uniqueness Test at length 8
            var setSize = 100000;
            var passwordLength = 8;
            var passwords = new List<string>();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < setSize; i++)
            {
                passwords.Add(generator(passwordLength));
            }
            stopwatch.Stop();
            var totalUnique = passwords.Distinct().Count();
            Console.WriteLine($"{totalUnique} unique passwords {passwordLength} characters long were generated in a set of {setSize}.");
            Console.WriteLine($"{stopwatch.Elapsed.TotalSeconds} to generate {setSize} passwords");
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------");
        }
    }
}
