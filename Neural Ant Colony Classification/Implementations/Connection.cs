using System;

namespace Neural_Ant_Colony_Classification
{
    class Connection : IConnection
    {
        private static readonly Random random = new Random();

        public INeuron Neuron { get; set; }
        public double Weight { get; private set; }

        public Connection(INeuron neuron, double maxWeight)
        {
            Neuron = neuron;
            Weight = 2.0 * (random.NextDouble() - 0.5) * maxWeight;
        }

        public void DistributeReward(double reward)
        {
            Weight += Sign.GetSign(Weight) * reward;
        }
    }
}
