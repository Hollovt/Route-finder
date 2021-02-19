using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigTask2.Ui;
using BigTask2.Api;

namespace BigTask2.Systems
{
    class XMLSystem : ISystem
    {
        class XMLForm : IForm
        {
            Dictionary<string, string> map { get; set; }
            public XMLForm ()
            {
                map = new Dictionary<string, string>();
            }
            public bool GetBoolValue(string name)
            {
                bool result = false;
                if (map.ContainsKey(name))
                {
                    if (map[name] == "True")
                        result = true;
                    else if(map[name] == "False")
                        result = false;
                    else
                    {
                        Console.WriteLine("zla wartosc boola");
                    }

                }

                return result;
            }

            public int GetNumericValue(string name)
            {
                int result = 0;
                if (map.ContainsKey(name))
                {
                    result = int.Parse(map[name]);
                }
                return result;
            }

            public string GetTextValue(string name)
            {
                string result = "";
                if (map.ContainsKey(name))
                {
                    result = map[name];
                }
                return result;
            }

            public void Insert(string command)
            {

                string name = "";
                string value = "";
                int j = 0;
                for (int i = 0; i < command.Length && j < 3; i++)
                {
                    if (command[i] == '<' || command[i] == '>')
                    {
                        j++;
                        continue;
                    }
                    if (j == 1)
                    {
                        name += command[i];
                    }
                    if (j == 2)
                    {
                        value += command[i];
                    }
                }
                if (map.ContainsKey(name))
                    map[name] = value;
                else
                    map.TryAdd(name, value);
                //Console.WriteLine(name + " " + value);
            }
        }
        class XMLDisplay : IDisplay
        {
            public void Print(IEnumerable<Route> routes)
            {
                double totalCost = 0;
                double totalTime = 0;
                if(routes==null)
                {
                    Console.WriteLine("<>");
                    return;
                }
                foreach (var r in routes)
                {
                    Console.WriteLine("<City/>");
                    Console.WriteLine("<From>" + r.From.Name + "</From>");
                    Console.WriteLine("<Population>" + r.From.Population + "</Population>");
                    Console.WriteLine("<HasRestaurant>" + r.From.HasRestaurant + "</HasRestaurant>");
                    Console.WriteLine();
                    // Console.WriteLine("<To>" + r.To.Name + "</To>");
                    Console.WriteLine("<Route/>");

                    Console.WriteLine("<Cost>" + r.Cost + "</Cost>");
                    
                    Console.WriteLine("<TravelTime>" + r.TravelTime + "</TravelTime>");
                    Console.WriteLine("<VehicleType>" + r.VehicleType + "</VehicleType>");
                    Console.WriteLine();
                    totalCost += r.Cost;
                    totalTime += r.TravelTime;
                    
                }
                var r2 = routes.Last();
                Console.WriteLine("<City/>");
                Console.WriteLine("<From>" + r2.To.Name + "</From>");
                Console.WriteLine("<Population>" + r2.To.Population + "</Population>");
                Console.WriteLine("<HasRestaurant>" + r2.To.HasRestaurant + "</HasRestaurant>");
                Console.WriteLine();
                Console.WriteLine("<totalTime>" + totalTime + "</totalTime>");
                Console.WriteLine("<totalCost>" + totalCost + "</totalCost>");
                


                Console.WriteLine();
            }
        }

        public IForm Form { get; }

        public IDisplay Display {get;}

        public XMLSystem ()
        {
            Form = new XMLForm();
            Display = new XMLDisplay();
        }
    }
   
  
}
//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Tomasz Potomski