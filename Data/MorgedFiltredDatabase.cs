using System;
using System.Collections.Generic;
using System.Text;
using BigTask2.Interfaces;
using BigTask2.Data;
using BigTask2.Api;
using BigTask2.Ui;

namespace BigTask2.Data
{
    class MergedFiltredDatabase : IGraphDatabase
    {
        private List<IGraphDatabase> databases;
        
        readonly private Filter filter;
        private class MergedFiltredDatabaseIterator : IEdgesIterator
        {
            readonly private Filter filter;
            readonly private List<IEdgesIterator> iterators;
            private bool end = false;
           
            //private int i;
            
            private Route last_route;
            public Route Next
            {
                get
                {
                    /*if (!IsNextExist())
                    {
                        Console.WriteLine("there is no more routes");
                        return null;
                    }*/
                    if (last_route == null)
                        Console.WriteLine("null");
                    
                    return last_route;

                }
            }

            public bool IsNextExist()
            {
                if (end)
                    return false;
                IEdgesIterator it;
                Route r;
                for (int i = 0; i < iterators.Count; i++)
                {

                    it = iterators[i];

                    while (it.IsNextExist())
                    {
                        
                        r = it.Next;
                        
                        if (FilterRoute(r))
                        {
                            last_route = r;

                            return true;
                        }

                    }
                    
                }

                end = true;
                return false;
            }

            public bool FilterRoute(Route route)
            {
                if (route.To.HasRestaurant == false && filter.RestaurantRequired == true)
                    return false;
                if (route.To.Population < filter.MinPopulation)
                    return false;

                if ( route.VehicleType == VehicleType.Train && !filter.AllowedVehicles.Contains(VehicleType.Train))
                    return false;
                if (route.VehicleType == VehicleType.Car && !filter.AllowedVehicles.Contains(VehicleType.Car))
                    return false;
               
                return true;
               
            }
            public void Start()
            {
               
                
                foreach(var it in iterators)
                {
                    it.Start();
                }
               
            }
            public MergedFiltredDatabaseIterator(Filter filter, List<IEdgesIterator> iterators)
            {
                this.filter = filter;
                this.iterators = iterators;
                last_route = new Route();
               
            }
        }
        public City GetByName(string cityName)
        {
            foreach (IGraphDatabase database in databases)
            {
                City c = database.GetByName(cityName);
                if (c != null)
                    return c;
            }
            return null;
        }

        public IEdgesIterator GetRoutesFrom(City from)
        {
            
            List<IEdgesIterator> iterators = new List<IEdgesIterator>();
            foreach (IGraphDatabase database in databases)
                iterators.Add(database.GetRoutesFrom(from));
            
            return new MergedFiltredDatabaseIterator(filter, iterators);
            
        }
        public void Merge(params IGraphDatabase[] databases)
        {
            if (databases != null && databases.Length != 0)
            {
                for (int i = 0; i < databases.Length; i++)
                    this.databases.Add(databases[i]);
            }
        }
        public MergedFiltredDatabase(Filter filter, params IGraphDatabase[] databases)
        {
            this.databases = new List<IGraphDatabase>();
            if(databases!=null && databases.Length!=0)
            {
                for (int i = 0; i < databases.Length; i++)
                    this.databases.Add(databases[i]);
            }
            this.filter = filter;
        }
        

    }
}
//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Tomasz Potomski