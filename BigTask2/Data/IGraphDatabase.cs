//This file contains fragments that You have to fulfill

using BigTask2.Api;
using BigTask2.Ui;
namespace BigTask2.Data
{
    public interface IGraphDatabase
    {
        //Fill the return type of the method below
        /* */IEdgesIterator GetRoutesFrom(City from);
        City GetByName(string cityName);
    }
   
}
