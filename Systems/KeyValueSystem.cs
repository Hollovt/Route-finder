using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigTask2.Ui;
using BigTask2.Api;
namespace BigTask2.Systems
{
    class KeyValueSystem : ISystem
    {
        private class KeyValueForm : IForm
        {
            Dictionary<string, string> map { get; set; }
            //Dictionary<string, string> IForm.map { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public KeyValueForm()
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
                    else if (map[name] == "False")
                        result = false;
                    else
                        Console.WriteLine("zla wartosc boola");

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
                for (int i = 0; i < command.Length && j < 2; i++)
                {
                    if (command[i] =='=')
                    {
                        j++;
                        continue;
                    }
                    if (j ==0)
                    {
                        name += command[i];
                    }
                    if (j == 1)
                    {
                        value += command[i];
                    }
                }
                //Console.WriteLine(name + " " + value);
                if (map.ContainsKey(name))
                    map[name] = value;
                else
                    map.TryAdd(name, value);

            }
        }
        private class KeyValueDisplay : IDisplay
        {
            public void Print(IEnumerable<Route> routes)
            {
                double totalTime = 0;
                double totalCost = 0;
                if (routes==null)
                {
                    Console.WriteLine("=");
                    return;
                }
                foreach (var r in routes)
                {
                    Console.WriteLine("=City=");
                    Console.WriteLine("Name=" + r.From.Name);
                    Console.WriteLine("Populaton=" + r.From.Population);
                    Console.WriteLine("HasRestaurant=" + r.From.HasRestaurant);
                    Console.WriteLine();
                    Console.Write("=Route=");
                    Console.WriteLine("Cost=" + r.Cost);
                    Console.WriteLine("TravelTime=" + r.TravelTime);
                    Console.WriteLine("VehicleType=" + r.VehicleType);
                    Console.WriteLine();
                    totalCost += r.Cost;
                    totalTime += r.TravelTime;
                }
                var r2 = routes.Last();
                Console.WriteLine("=City=");
                Console.WriteLine("Name=" + r2.To.Name);
                Console.WriteLine("Populaton=" + r2.To.Population);
                Console.WriteLine("HasRestaurant=" + r2.To.HasRestaurant);
                Console.WriteLine();
                Console.WriteLine("totalCost=" + totalCost);
                Console.WriteLine("totalTime=" + totalTime);
                Console.WriteLine();
            }
        }

        public IForm Form { get; }

        public IDisplay Display { get; }

        public KeyValueSystem()
        {
            Form = new KeyValueForm();
            Display = new KeyValueDisplay();
        }

    }
}
//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Tomasz Potomski