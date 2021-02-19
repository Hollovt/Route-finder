//This file Can be modified

using BigTask2.Api;
using System.Collections.Generic;

namespace BigTask2.Ui
{
    interface IForm
    {
        void Insert(string command);
        bool GetBoolValue(string name);
        string GetTextValue(string name);
        int GetNumericValue(string name);
    }

    interface IDisplay
    {
        void Print(IEnumerable<Route> routes);
    }

    interface ISystem
    {
        IForm Form { get; }
        IDisplay Display { get; }
    }
    public interface IEdgesIterator
    {
        Route Next { get; }
        void Start();
        
        bool IsNextExist();
    }
}
//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Tomasz Potomski