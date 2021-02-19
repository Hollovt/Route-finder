//This file contains fragments that You have to fulfill

using BigTask2.Api;
using System.Collections.Generic;
using BigTask2.Ui;

namespace BigTask2.Data
{
	class AdjacencyListDatabase : IGraphDatabase
    {
		private Dictionary<string, City> cityDictionary = new Dictionary<string, City>();
		private Dictionary<City, List<Route>> routes = new Dictionary<City, List<Route>>();
        private class AdjacencyListDatabaseIterator : IEdgesIterator
        {
            private List<Route> list=null;
            private int i = 0;
            public Route Next
            { get
                {
                    Route tmp = new Route();
                    if (IsNextExist())
                    {
                        tmp = list[i];
                        i++;
                       
                    }
                    return tmp;
                 }
            }

            public void Start()
            {
                i = 0;
            }


            public bool IsNextExist()
            {
                if (i >= list.Count)
                    return false;
                return true;
            }

            public AdjacencyListDatabaseIterator(List<Route> l)
            {
                i = 0;
                list = l;
                
            }
            public AdjacencyListDatabaseIterator( AdjacencyListDatabaseIterator a )
            {
                this.i = a.i;
                this.list = a.list;
            }
        }
        
        private void AddCity(City city)
		{
			if (!cityDictionary.ContainsKey(city.Name))
				cityDictionary[city.Name] = city;
		}
		public AdjacencyListDatabase(IEnumerable<Route> routes)
		{
			foreach(Route route in routes)
			{
				AddCity(route.From);
				AddCity(route.To);
				if (!this.routes.ContainsKey(route.From))
				{
					this.routes[route.From] = new List<Route>();
				}
				this.routes[route.From].Add(route);
			}
		}
		public AdjacencyListDatabase()
		{
		}
		public void AddRoute(City from, City to, double cost, double travelTime, VehicleType vehicle)
		{
			AddCity(from);
			AddCity(to);
			if (!routes.ContainsKey(from))
			{
				routes[from] = new List<Route>();
			}
			routes[from].Add(new Route { From = from, To = to, Cost = cost, TravelTime = travelTime, VehicleType = vehicle});
		}

		public IEdgesIterator GetRoutesFrom(City from)
		{
            /*
			 * Fill this fragment and return Type.
			 * Modyfing existing code in this class is forbidden.
			 * Adding new elements (fields, private classes) to this class is allowed.
			 */
            AdjacencyListDatabaseIterator iterator = new AdjacencyListDatabaseIterator(routes[from]);
            return iterator;

        }

		public City GetByName(string cityName)
		{
			return cityDictionary.GetValueOrDefault(cityName);
		}

	}
   
}
//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Tomasz Potomski