//This file Can be modified

using BigTask2.Data;
using BigTask2.Api;
using System.Collections.Generic;
using BigTask2.Algorithms;

namespace BigTask2.Interfaces
{
    interface IRouteProblem
	{
        string From { get; }
        string To { get; }
        IGraphDatabase Graph { get; set; }
        IEnumerable <Route> Calculate(IAlgorithmCalculate algorithm);
        void SetGraph(IGraphDatabase g);
    }
}
//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Tomasz Potomski