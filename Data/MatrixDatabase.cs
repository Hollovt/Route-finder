//This file contains fragments that You have to fulfill

using BigTask2.Api;
using System.Collections.Generic;
using System.Linq;
using BigTask2.Ui;
namespace BigTask2.Data
{
	class MatrixDatabase : IGraphDatabase
	{
		private Dictionary<City, int> cityIds = new Dictionary<City, int>();
		private Dictionary<string, City> cityDictionary = new Dictionary<string, City>();
		private List<List<Route>> routes = new List<List<Route>>();
        private class MatrixDatabaseIteraror : IEdgesIterator
        {
            private List<Route> list;
            private int i = 0;
            readonly private int index;
            public Route Next
            {
                get
                {
                    if(IsNextExist())
                        for (int j = i + 1; j < list.Count; j++)
                        {
                            if (list[j] != null && list != null)
                            {
                                i = j;
                                return list[j];
                            }
                        }
                    return null;
                }
            }

            public void Start()
            {
                i = 0;
            }

            public bool IsNextExist()
            {
                for(int j=i+1; j<list.Count; j++)
                 {
                     if (list[j] != null && list != null)
                         return true;
                 }
                 return false;
                
            }
            public MatrixDatabaseIteraror(List <Route> l, int index)
            {
                list = l;
                i = 0;
                this.index = index;
            }

        }
        private void AddCity(City city)
		{
			if (!cityDictionary.ContainsKey(city.Name))
			{
				cityDictionary[city.Name] = city;
				cityIds[city] = cityIds.Count;
				foreach (var routes in routes)
				{
					routes.Add(null);
				}
				routes.Add(new List<Route>(Enumerable.Repeat<Route>(null, cityDictionary.Count)));
			}
		}
		public MatrixDatabase(IEnumerable<Route> routes)
		{
            foreach (var route in routes)
			{
				AddCity(route.From);
				AddCity(route.To);
			}
			foreach (var route in routes)
			{
				this.routes[cityIds[route.From]][cityIds[route.To]] = route;
			}
		}

		public void AddRoute(City from, City to, double cost, double travelTime, VehicleType vehicle)
		{
			AddCity(from);
			AddCity(to);
			routes[cityIds[from]][cityIds[to]] = new Route { From = from, To = to, Cost = cost, TravelTime = travelTime, VehicleType = vehicle };
		}

		public IEdgesIterator GetRoutesFrom(City from)
		{
            /*
			* Fill this fragment and return Type.
			* Modyfing existing code in this class is forbidden.
			* Adding new elements (fields, private classes) to this class is allowed.
			*/
            MatrixDatabaseIteraror iterator = new MatrixDatabaseIteraror(routes[cityIds[from]], cityIds[from]);
            return iterator;
		}

		public City GetByName(string cityName)
		{
            City c= new City();
            cityDictionary.TryGetValue(cityName,out c);
            return c;
		}
	}
    
}
//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Tomasz Potomski