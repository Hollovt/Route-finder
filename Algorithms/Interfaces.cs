using System;
using System.Collections.Generic;
using System.Text;
using BigTask2.Api;
using BigTask2.Ui;
using BigTask2.Interfaces;
using BigTask2.Data;
namespace BigTask2.Algorithms
{
    interface IAlgorithm //początkowo chciałem w ten sposób wybierąc odpowiedni algorytm, ale musiałbym 
    {                     //zastosować użyć zagnieżdżonych ifów póżniej, żeby określić jeszcze czy zajmujemy się CostProblemem, czy 
                          //TimeProblemem, więc z pomysłu rezygnuje, ale interface zostawiam bo w sposób spójny uporządkowuje akrualne
                          //i przyszłe algoryty
        IEnumerable<Route> Solve(IGraphDatabase graph, City from, City to);
    }

    interface IAlgorithmCalculate
    {
        IEnumerable<Route> CostProblem(IRouteProblem problem);
        IEnumerable<Route> TimeProblem(IRouteProblem problem);
    }
    
}
//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Tomasz Potomski