//This file contains fragments that You have to fulfill

using BigTask2.Api;
using BigTask2.Data;
using BigTask2.Interfaces;
using System.Collections.Generic;

namespace BigTask2.Algorithms
{
	public class DFS : IAlgorithm
    {
		public IEnumerable<Route> Solve(IGraphDatabase graph, City from, City to)
		{
			Dictionary<City, Route> routes = new Dictionary<City, Route>();
			routes[from] = null;
			Stack<City> stack = new Stack<City>();
			stack.Push(from);
			do
			{
				City city = stack.Pop();
                /*
				 * For each outgoing route from city...
				 */
                var it = graph.GetRoutesFrom(city);
                it.Start();
                while (it.IsNextExist())
                {
                    
					Route route = it.Next; /* Change to current Route*/
					if (routes.ContainsKey(route.To))
					{
						continue;
					}
					routes[route.To] = route;
					if (route.To == to)
					{
						break;
					}
					stack.Push(route.To);
				}
			} while (stack.Count > 0);
			if (!routes.ContainsKey(to))
			{
				return null;
			}
			List<Route> result = new List<Route>();
			for (Route route = routes[to]; route != null; route = routes[route.From])
			{
				result.Add(route);
			}
			result.Reverse();
			return result;
		}
	}
    class DFSAlgorithm : IAlgorithmCalculate
    {
        private DFS dfs;
        public IEnumerable<Route> CostProblem(IRouteProblem problem)
        {
            return dfs.Solve(problem.Graph, problem.Graph.GetByName(problem.From), problem.Graph.GetByName(problem.To));
        }

        public IEnumerable<Route> TimeProblem(IRouteProblem problem)
        {
            return dfs.Solve(problem.Graph, problem.Graph.GetByName(problem.From), problem.Graph.GetByName(problem.To));
        }
        public DFSAlgorithm()
        {
            dfs = new DFS();
        }

    }
}
