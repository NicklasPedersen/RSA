using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            string answer;
            RSACryptoServiceProvider crypto = new RSACryptoServiceProvider();
            do
            {
                Console.WriteLine("Options:");
                Console.WriteLine("1: Generate keypair");
                Console.WriteLine("2: Send message encrypted");
                Console.WriteLine("3: Receive message encrypted");
                Console.WriteLine("exit: exit the program");
                answer = Console.ReadLine();
                
                switch (answer)
                {
                    case "1":
                        Console.WriteLine("Generating keypair...");
                        crypto = new RSACryptoServiceProvider();
                        RSAParameters param = crypto.ExportParameters(false);
                        Console.WriteLine("Modulus:");
                        Console.WriteLine(Convert.ToBase64String(param.Modulus));
                        Console.WriteLine("Exponent:");
                        Console.WriteLine(Convert.ToBase64String(param.Exponent));
                        break;
                    case "2":
                        Console.Write("Write a message: ");
                        string message = Console.ReadLine();
                        Console.Write("What is the Modulus?: ");
                        string mod = Console.ReadLine();
                        Console.Write("What is the Exponent?: ");
                        string exp = Console.ReadLine();

                        Console.WriteLine("Encrypting...");
                        using (RSACryptoServiceProvider c = new RSACryptoServiceProvider())
                        {
                            c.ImportParameters(new RSAParameters { Exponent = Convert.FromBase64String(exp), Modulus = Convert.FromBase64String(mod) });
                            byte[] encrypted = c.Encrypt(Encoding.ASCII.GetBytes(message), false);
                            Console.WriteLine("Encrypted message:");
                            Console.WriteLine(Convert.ToBase64String(encrypted));
                        }
                        break;
                    case "3":
                        Console.Write("What is the encrypted message?: ");
                        string msg = Console.ReadLine();
                        Console.WriteLine("Decrypting with stored private key...");
                        Console.WriteLine("Message is:");
                        Console.WriteLine(Encoding.ASCII.GetString(crypto.Decrypt(Convert.FromBase64String(msg), false)));
                        break;
                }
            } while (answer.ToLower() != "exit");
        }
    }
}
