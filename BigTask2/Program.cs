using BigTask2.Api;
using BigTask2.Data;
using BigTask2.Ui;
using System;
using System.Collections.Generic;
using System.IO;
using BigTask2.Systems;
using BigTask2.Problems;
using BigTask2.Algorithms;
using BigTask2.Interfaces;
namespace BigTask2
{
	class Program
	{
		/*static IEnumerable<Route> Calculate(Request request, IGraphDatabase database)
		{
			IRouteProblem problem = GetProblem(request, database);
			IAlgorithmCalculate algorithm = GetAlgorithm(request);
			return problem.Calculate(algorithm);
		}
		static IRouteProblem GetProblem(Request request, IGraphDatabase database)
		{
			IRouteProblem problem=null;
			if (request.Problem == "Cost")
				problem = new CostProblem(request.From, request.To);
			if (request.Problem == "Time")
				problem = new TimeProblem(request.From, request.To);

			if (problem == null)
			{
				Console.WriteLine("Nieznany problem");
				return null;
			}
			problem.SetGraph(database);
			return problem;
			
		}
		static IAlgorithmCalculate GetAlgorithm(Request request)
		{
			if (request.Solver == "BFS")   //nie wiem czy else jest dozwolone, ale używam tylko po to, by sprawdzać błędy.
				return new BFSAlgorithm(); //Można to zrobić bez elsów chociażby używając dodatkowej zmiennej, ale taki kod jest
			if (request.Solver == "DFS")  //czytelniejszy 
				return new DFSAlgorithm();
			if (request.Solver == "Dijkstra")
				return  new DijkstraAlgorithm();
		   
			Console.WriteLine("nieznany algorytm");
			return null;
			
		}*/ //porzucony pomysl rozbicia ServeRequest na funkcje 
		static IEnumerable<Route> ServeRequest(Request request)
		{
			(IGraphDatabase cars, IGraphDatabase trains) = MockData.InitDatabases();
			MergedFiltredDatabase database = new MergedFiltredDatabase(request.Filter, cars, trains);
			ICheckArguments check = new CheckFrom();
			if (!check.Check(request))
			{
				return null;
			}

			IAlgorithmCalculate algorithm = new GetAlgorithm().getAlgorithm(request);
			IRouteProblem problem = new GetPoblemType().GetProblem(request, database);

			return problem.Calculate(algorithm);

		}
		static void Main(string[] args)
		{
			Console.WriteLine("---- Xml Interface ----");
			/*
			 * Create XML System Here
			 * and execute prepared strings
			 */
			ISystem xmlSystem = CreateSystem("xml");
			Execute(xmlSystem, "xml_input.txt");
			Console.WriteLine();

			Console.WriteLine("---- KeyValue Interface ----");
			/*
			 * Create INI System Here
			 * and execute prepared strings
			 */
			ISystem keyValue = CreateSystem("keyvalue");
			KeyValueSystem keyValueSystem = new KeyValueSystem();
			Execute(keyValueSystem, "key_value_input.txt");
			Console.WriteLine();
		}

		/* Prepare method Create System here (add return, arguments and body)*/
		static ISystem CreateSystem(string name)
		{
			if (name == "xml")
				return new XMLSystem();
			if (name == "keyvalue")
				return new KeyValueSystem();
			return new XMLSystem();
		}
	
		static void Execute(ISystem system, string path)
		{
			IEnumerable<IEnumerable<string>> allInputs = ReadInputs(path);
			foreach (var inputs in allInputs)
			{
				foreach (string input in inputs)
				{
					//Console.WriteLine("input = " + input);
					system.Form.Insert(input);
				}
				var request = RequestMapper.Map(system.Form);
				var result = ServeRequest(request);
				system.Display.Print(result);
				Console.WriteLine("==============================================================");
			}
		}

		private static IEnumerable<IEnumerable<string>> ReadInputs(string path)
		{
			using (StreamReader file = new StreamReader("..\\..\\..\\" + path))
			{
				List<List<string>> allInputs = new List<List<string>>();
				while (!file.EndOfStream)
				{
					string line = file.ReadLine();
					List<string> inputs = new List<string>();
					while (!string.IsNullOrEmpty(line))
					{
						inputs.Add(line);
						line = file.ReadLine();
					}
					if (inputs.Count > 0)
					{
						allInputs.Add(inputs);
					}
				}
				return allInputs;
			}
		}
	}
}
