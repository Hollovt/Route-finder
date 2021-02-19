//This file contains fragments that You have to fulfill

using BigTask2.Api;
using BigTask2.Data;
using BigTask2.Ui;
using System.Collections.Generic;
using BigTask2.Problems;
using BigTask2.Interfaces;
namespace BigTask2.Algorithms
{
	public class BFS : IAlgorithm
    {
		public IEnumerable<Route> Solve(IGraphDatabase graph, City from, City to)
		{
			Dictionary<City, Route> routes = new Dictionary<City, Route>();
			routes[from] = null;
			Queue<City> queue = new Queue<City>();
			queue.Enqueue(from);
			do
			{
				City city = queue.Dequeue();
                /*
				 * For each outgoing route from city...
				 */
                var it = graph.GetRoutesFrom(city);
                it.Start();
                while(it.IsNextExist())
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
					queue.Enqueue(route.To);
				}
			} while (queue.Count > 0);
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

    class BFSAlgorithm : IAlgorithmCalculate
    {
        private BFS bfs;
        public IEnumerable<Route> CostProblem(IRouteProblem problem)
        {
            return bfs.Solve(problem.Graph, problem.Graph.GetByName(problem.From), problem.Graph.GetByName(problem.To));
        }

        public IEnumerable<Route> TimeProblem(IRouteProblem problem)
        {
            return bfs.Solve(problem.Graph, problem.Graph.GetByName(problem.From), problem.Graph.GetByName(problem.To));
        }
        public BFSAlgorithm()
        {
            bfs = new BFS();
        }

    }
}
