using System;
using System.Collections.Generic;
using System.Text;
using BigTask2.Api;
using BigTask2.Problems;
using BigTask2.Interfaces;
using BigTask2.Api;
using BigTask2.Data;
using BigTask2.Algorithms;
namespace BigTask2
{
    interface ICheckArguments
    {
        ICheckArguments Next { get; }
        bool Check(Request request);
    }
    class CheckFrom : ICheckArguments
    {
        public ICheckArguments Next { get; }

        public bool Check(Request request)
        {
            //Console.WriteLine(request.From == "");
            if (request.From == "")
                return false;
            return Next.Check(request);
        }
        public CheckFrom()
        {
            this.Next = new CheckTo();
        }
    }
    class CheckTo : ICheckArguments
    {
        public ICheckArguments Next { get; }

        public bool Check(Request request)
        {
            //Console.WriteLine(request.To == "");

            if (request.To == "")
                return false;
            return Next.Check(request);
        }
        public CheckTo()
        {
            this.Next = new CheckPopulation();
        }
    }
    class CheckPopulation : ICheckArguments
    {
        public ICheckArguments Next { get; }

        public bool Check(Request request)
        {
            //Console.WriteLine(request.Filter.MinPopulation < 0);

            if (request.Filter.MinPopulation < 0 )
                return false;
            return Next.Check(request);
        }
        public CheckPopulation()
        {
            this.Next = new CheckVehicles();
        }
    }
    class CheckVehicles : ICheckArguments
    {
        public ICheckArguments Next { get; }

        public bool Check(Request request)
        {
            //Console.WriteLine(request.Filter.AllowedVehicles.Count==0);
            
            if (request.Filter.AllowedVehicles.Contains(VehicleType.Car) || request.Filter.AllowedVehicles.Contains(VehicleType.Train))
                return true;
            return false;


        }
       
    }

    interface IGetProblem
    {
        IGetProblem Next { get; }
        IRouteProblem GetProblem(Request request, IGraphDatabase database);
    }
    class GetPoblemType : IGetProblem
    {
        public IGetProblem Next { get; }

        public IRouteProblem GetProblem(Request request, IGraphDatabase database)
        {
            if (request.Problem == "Time")
            {
                
                IRouteProblem problem = new TimeProblem(request.From, request.To);
                problem.SetGraph(database);
                return problem;
            }
            return Next.GetProblem(request, database);
        }
        public GetPoblemType()
        {
            Next = new GetCostProblem();
        }
    }
    class GetCostProblem : IGetProblem
    {
        public IGetProblem Next { get; }

        public IRouteProblem GetProblem(Request request, IGraphDatabase database)
        {
            if (request.Problem == "Cost")
            {

                IRouteProblem problem = new CostProblem(request.From, request.To);
                problem.SetGraph(database);
                return problem;
            }
            return null;
        }
        
    }
    interface IGetAlgorithm
    {
        IGetAlgorithm Next { get; }
        IAlgorithmCalculate getAlgorithm(Request request);
    }
    class GetAlgorithm : IGetAlgorithm
    {
        public IGetAlgorithm Next { get; }

        public IAlgorithmCalculate getAlgorithm(Request request)
        {
            if (request.Solver == "BFS")
                return new BFSAlgorithm();
            return Next.getAlgorithm(request);
        }
        public GetAlgorithm()
        {
            Next = new GetDFSAlgorithm();
        }
    }
    class GetDFSAlgorithm : IGetAlgorithm
    {
        public IGetAlgorithm Next { get; }

        public IAlgorithmCalculate getAlgorithm(Request request)
        {
            if (request.Solver == "DFS")
                return new DFSAlgorithm();
            return Next.getAlgorithm(request);
        }
        public GetDFSAlgorithm()
        {
            Next = new GetDijkstraAlgorithm();
        }
    }
    class GetDijkstraAlgorithm : IGetAlgorithm
    {
        public IGetAlgorithm Next { get; }

        public IAlgorithmCalculate getAlgorithm(Request request)
        {
            if (request.Solver == "Dijkstra")
                return new DijkstraAlgorithm();
            return null;
        }
    }

}
//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Tomasz Potomski