using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;

namespace Stream
{  
    /*
    [Serializable]
    public class Binary
    {
        public int[] a = new int[10];
    }
    */
   
    class Program
    {
        static void Main(string[] args)
        {
 
            Random rnd = new Random();

            int[] b = new int[1000000];

            string t;

            for (int i = 0; i < b.Length; i++)
            {
                b[i] = rnd.Next(0, 9);
            }

            try
            {
                using (StreamWriter sw = new StreamWriter("text.txt", false, System.Text.Encoding.Default))
                {
                    for (int i = 0; i < b.Length; i++)
                    {
                        sw.Write(b[i] + "\n");

                    }

                }
                Console.WriteLine("Запись выполнена");

                string[] Arr = new string[1000000];
                using (StreamReader sr = new StreamReader(File.Open("text.txt", FileMode.OpenOrCreate)))
                {
                    
                    for (int i = 0; i < 1000000; i++)
                    {
                        Arr[i] = sr.ReadLine();
                    }
                }
                
                using (StreamReader sr2 = new StreamReader(File.Open("text.txt", FileMode.OpenOrCreate)))
                {
                    Console.WriteLine(sr2.ReadToEnd());

                }


                using (StreamWriter sw2 = new StreamWriter(File.Open("text.txt", FileMode.OpenOrCreate)))
                {
                    t = Arr[699999];
                    Arr[699999] = Arr[199999];
                    Arr[199999] = t;

                    for (int i = 0; i < Arr.Length; i++)
                    {
                        sw2.Write(Arr[i] + "\n");
                    }
                }

                using (StreamReader sr2 = new StreamReader(File.Open("text.txt", FileMode.OpenOrCreate)))
                {
                    Console.WriteLine(sr2.ReadToEnd());

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            


            /*
            Binary binary = new Binary();

            Random rnd = new Random();


            int t;
            for (int i = 0; i < binary.a.Length; i++)
            {
                binary.a[i] = rnd.Next(0, 9);
            }

            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open("text.txt", FileMode.OpenOrCreate)))
                {
                    for (int i = 0; i < binary.a.Length; i++)
                    {
                        writer.Write(binary.a[i]);
                    }

                    
                }

                using (BinaryReader reader = new BinaryReader(File.Open("text.txt", FileMode.Open)))
                {

                    while (reader.PeekChar() > -1)
                    {
                        Console.Write(reader.ReadInt32());
                    }
                }
                Console.WriteLine();
                Console.WriteLine("---------------------------------");

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            
            BinaryFormatter formatter = new BinaryFormatter();
           
            using (FileStream ff = new FileStream("text.txt", FileMode.OpenOrCreate))
            {
                formatter.Serialize(ff, binary);

                Console.WriteLine("Cериализован");
                
            }
            */

            using (FileStream source = new FileStream("text.txt", FileMode.OpenOrCreate))
            {
                
                using (FileStream target = File.Create("text2.txt"))
                {
                   
                    using (GZipStream compression = new GZipStream(target, CompressionMode.Compress))
                    {
                        source.CopyTo(compression); 
                        Console.WriteLine("Сжатие файла");
                    }
                }
            }

            using (FileStream source = new FileStream("text2.txt", FileMode.OpenOrCreate))
            {
                
                using (FileStream target = File.Create("text3.txt"))
                {
                    
                    using (GZipStream decompression = new GZipStream(source, CompressionMode.Decompress))
                    {
                        decompression.CopyTo(target);
                        Console.WriteLine("Восстановлен");
                    }
                }
            }


            using (StreamReader sr4 = new StreamReader(File.Open("text3.txt", FileMode.OpenOrCreate)))
            {
                Console.WriteLine(sr4.ReadToEnd());

            }
        }
    }
}
