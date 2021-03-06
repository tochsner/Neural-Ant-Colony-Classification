﻿using System.Collections.Generic;

namespace Neural_Ant_Colony_Classification
{
    interface IClassificationNeuralAntColony
    { 
        void SetInputOutput(List<int> inputValues, List<int> outputSigns);
        void RunIteration();
        double EvaluateAccuracy(int numberIterations);
        double Accuracy { get; }
        List<int> OutputSigns { get; }
        List<double> OutputValues { get; }
    }

    interface INeuron
    {        
        void PrepareToFire();
        void Fire();
        void AddAnt(IAnt ant);
        void AddNeighbour(INeuron neighbour);
    }

    interface IAnt
    {
        void AddConnectionToHistory(IConnection connection);
        void DistributeReward(double reward);
        int Sign { get; set; }
    }    

    interface IConnection
    {
        INeuron Neuron { get; }
        double Weight { get; }
        void DistributeReward(double reward);
    }
}
