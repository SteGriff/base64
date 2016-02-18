using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace base64
{
    class Program
    {
        static void Main(string[] args)
        {
            // Defaults:
            // Encode, from UTF8 String, to Base64 String
            //
            // Options:
            //  Decode
            //  from Binary 

            bool encode = true;

            string stringInput = null;
            var binaryInput = new List<byte> ();

            Encoding stringEncoding = Encoding.UTF8;
            Encoding outputEncoding = null;

            var switches = new List<string>();

            //Gather input from args or readline
            if (args.Length > 0)
            {

                foreach (var a in args)
                {
                    if (a.Length > 2 && a.Substring(0, 2) == "--")
                    {
                        switches.Add(a.Substring(2));
                    }
                }
                args = args.Skip(switches.Count).ToArray();

                //Process switches
                foreach (var s in switches)
                {
                    switch (s)
                    {
                        case "binary":
                            binaryInput = ReadArgsAsBinary(args);
                            if (binaryInput.Count == 0)
                            {
                                var content = GetContent();
                                binaryInput = ReadStringAsBinary(content);
                            }
                            break;

                        case "ascii":
                            stringEncoding = Encoding.ASCII;
                            break;

                        case "decode":
                            encode = false;
                            break;

                        case "to-ascii":
                            outputEncoding = Encoding.ASCII;
                            break;

                        case "to-utf8":
                            outputEncoding = Encoding.UTF8;
                            break;

                    }

                }

                if (binaryInput.Count == 0)
                {
                    stringInput = ReadArgsAsString(args);
                    if (stringInput == null)
                    {
                        stringInput = GetContent();
                    }
                }

            }
            else
            {
                stringInput = GetContent();
            }

            //Choose which input to use
            byte[] input;

            if (stringInput != null)
            {
                input = stringEncoding.GetBytes(stringInput);
            }
            else if (binaryInput.Count > 0)
            {
                input = binaryInput.ToArray();
            }
            else
            {
                Console.WriteLine("Not enough data");
                return;
            }

            //Do the thing
            
            try
            {
                string result = "";

                if (encode)
                {
                    result = Convert.ToBase64String(input);
                }
                else
                {
                    var bytes = Convert.FromBase64String(stringInput);
                    if (outputEncoding == null)
                    {
                        result = BitConverter.ToString(bytes);
                    }
                    else
                    {
                        result = outputEncoding.GetString(bytes);
                    }
                }

                Console.WriteLine(result);
            }
            catch (Exception)
            {
                Console.WriteLine("Could not encode/decode");
            }

        }

        private static string GetContent()
        {
            Console.Write("Enter content: ");
            return Console.ReadLine();
        }

        private static string ReadArgsAsString(string[] args)
        {
            if (args.Length == 0)
            {
                return null;
            }
            else if (args.Length == 1)
            {
                return args[0];
            }
            else {
                return args.Aggregate((a, b) => a + " " + b);
            }
        }

        private static List<byte> ReadArgsAsBinary(string[] args)
        {
            if (args.Length == 0)
            {
                return new List<byte>();
            }
            else
            {
                return args.Select(a => byte.Parse(a)).ToList();
            }
            
        }

        private static List<byte> ReadStringAsBinary(string content)
        {
            var args = content.Split(new []{' '});
            return ReadArgsAsBinary(args);
        }
    }
}
