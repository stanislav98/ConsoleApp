using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Class1> persons=new List<Class1>();
            int year = DateTime.Now.Year;
            int inputYear = Int32.Parse(args[1]);
            double rating = Double.Parse(args[2]);
            string path = args[3];
            var csv = new StringBuilder();
            csv.AppendLine("Name,Rating");

            try
            {
                using (StreamReader r = new StreamReader(args[0]))
                {
                    string json = r.ReadToEnd();
                    persons = JsonConvert.DeserializeObject<List<Class1>>(json);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
            }
            
            List<Class1> ordered = persons.OrderByDescending(o => o.rating).ThenBy(o => o.name).ToList();
            foreach (var data in ordered)
            {
                if (rating <= data.rating)
                {
                    if (year-data.playerSince < inputYear)
                    {
                        Console.WriteLine("added new person");
                        var first = data.name;
                        var second = data.rating;
                        var newLine = string.Format("{0}, {1}", first, second);
                        csv.AppendLine(newLine);
                    }
                }
            }
            File.WriteAllText(path, csv.ToString());
        }

    }
}
