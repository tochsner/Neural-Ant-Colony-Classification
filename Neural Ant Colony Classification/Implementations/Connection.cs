using System;

namespace Neural_Ant_Colony_Classification
{
    class Connection : IConnection
    {
        public INeuron Neuron { get; set; }
        public double Weight { get; private set; }

        public Connection(INeuron neuron, double maxWeight)
        {
            Neuron = neuron;
            Weight = new Random().NextDouble() * maxWeight;
        }

        public void DistributeReward(double reward)
        {
            Weight += Sign.GetSign(Weight) * reward;
        }
    }
}
