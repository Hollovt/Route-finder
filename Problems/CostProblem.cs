//This file Can be modified

using BigTask2.Data;
using BigTask2.Interfaces;
using BigTask2.Algorithms;
using System.Collections.Generic;
using BigTask2.Api;
namespace BigTask2.Problems
{
	class CostProblem : IRouteProblem
	{
        public string From { get; }
        public string To { get; }
        public CostProblem(string from, string to)
		{
			From = from;
			To = to;
		}
        
        public IGraphDatabase Graph { get; set; }

        public IEnumerable<Route> Calculate(IAlgorithmCalculate algorithm)
        {
            return algorithm.CostProblem(this);
        }

        public void SetGraph(IGraphDatabase g)
        {
            this.Graph=g;
        }
    }
}
//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Tomasz Potomski