using System;
using System.Runtime.Intrinsics.Arm;
using MarioHash;
using ILGPU;
using ILGPU.Runtime;
using ILGPU.Runtime.Cuda;

namespace Hashbrowns
{
    class Program
    {
        static void Main(string[] args)
        {
                MainContainer RM = new MainContainer();
            MainContainer.RunMain();
        }
    }

    //This Class may be unnecessary.
    class ParseFile
    {
        public string WordList;

        public ParseFile()
        {

        }
        public ParseFile(string WList)
        {
            WordList = WList;

        }
    }

    class MainContainer
    {
        public static void RunMain()
        {
            string textToEnter = @"



 __  __     ______     ______     __  __     ______     ______     ______     __     __     __   __     ______    
/\ \_\ \   /\  __ \   /\  ___\   /\ \_\ \   /\  == \   /\  == \   /\  __ \   /\ \  _ \ \   /\ ""-.\ \   /\  ___\   
\ \  __ \  \ \  __ \  \ \___  \  \ \  __ \  \ \  __<   \ \  __<   \ \ \/\ \  \ \ \/ "".\ \  \ \ \-.  \  \ \___  \  
 \ \_\ \_\  \ \_\ \_\  \/\_____\  \ \_\ \_\  \ \_____\  \ \_\ \_\  \ \_____\  \ \__/"".~\_\  \ \_\\""\_\  \/\_____\ 
  \/_/\/_/   \/_/\/_/   \/_____/   \/_/\/_/   \/_____/   \/_/ /_/   \/_____/   \/_/   \/_/   \/_/ \/_/   \/_____/ 
                                                                                                                    

                ";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnter.Length / 2)) + "}", textToEnter));


            //ILGPU List Cuda Accelerators - "https://ilgpu.net/docs/02-beginner/01-context-and-accelerators/"
            Context context = Context.Create(builder => builder.Cuda());

            foreach (Device device in context)
            {
                Console.WriteLine(device);
            }


            Console.WriteLine("\nWhat hash would you like to fry? (No Salt)");
            string Hash = Console.ReadLine();

            Console.WriteLine("And what is the algorithm?");

            textToEnter = @"

(1) SHA-1
(2) SHA-2 (256)
(3) SHA-2 (512)
(4) SHA-3 (256) (Coming Soon!)
(5) MD4
(6) MD5
(7) BCRYPT (Coming Soon!)
(8) Whirlpool
(9) PBKDF2 (Coming Soon!)
                                        
                ";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnter.Length / 2)) + "}", textToEnter));
            String Algorithm = Console.ReadLine();

            Console.WriteLine("Hash:" + Hash + " Algorithm:" + Algorithm);
            Console.WriteLine("Is this correct? (Y/n)");
            string IsCorrect = Console.ReadLine();

            if (IsCorrect == "Y")
            {
                Console.WriteLine("What is the location of your wordlist?");
                string wordList = Console.ReadLine();

                // Read each line of the file into a string array. Each element
                // of the array is one line of the file.
                string[] lines = File.ReadAllLines(wordList);

                foreach (string line in lines)
                {
                    string hashedLine = "";

                    switch (Algorithm)
                    {
                        case "1":
                            hashedLine = MarioHash.MarioHash.Hash_SHA1(line);
                            break;
                        case "2":
                            hashedLine = MarioHash.MarioHash.Hash_SHA256(line);
                            break;
                        case "3":
                            hashedLine = MarioHash.MarioHash.Hash_SHA2(line);
                            break;
                        case "4":
                            hashedLine = MarioHash.MarioHash.Hash_SHA3(line);
                            break;
                        case "5":
                            hashedLine = MarioHash.MarioHash.Hash_MD4(line);
                            break;
                        case "6":
                            hashedLine = MarioHash.MarioHash.Hash_MD5(line);
                            break;
                        case "7":
                            hashedLine = MarioHash.MarioHash.Hash_BCRYPT(line);
                            break;
                        case "8":
                            hashedLine = MarioHash.MarioHash.Hash_Whirlpool(line);
                            break;
                        case "9":
                            hashedLine = MarioHash.MarioHash.Hash_PBKDF2(line);
                            break;
                    }

                    Console.WriteLine(hashedLine);

                    if (Hash.Equals(hashedLine, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine(line + " is served!");
                        break;
                    }
                }
            }
            else if (IsCorrect == "n")
            {
                RunMain();
            }
            else
            {
                Console.WriteLine("Invalid Input. Is " + Hash + ":" + Algorithm + " correct?");
                IsCorrect = Console.ReadLine();
            }
        }
    }
}
