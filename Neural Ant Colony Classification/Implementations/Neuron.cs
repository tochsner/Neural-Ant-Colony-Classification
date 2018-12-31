using System;
using System.Collections.Generic;
using System.Linq;

namespace Neural_Ant_Colony_Classification
{
    class Neuron : INeuron
    {
        private readonly Random random = new Random();

        private double maxWeight;

        protected int threshold;
        private bool thresholdReached = false;
        private int numberOfAntsToFire = 0;

        private List<IAnt> ants = new List<IAnt>();
        private List<IConnection> connections = new List<IConnection>();

        public Neuron(int maxThreshold, double maxWeight)
        {
            threshold = random.Next(maxThreshold);
            this.maxWeight = maxWeight;
        }

        public void ClearAnts()
        {
            ants.Clear();
        }

        public void AddNeighbour(INeuron neighbour)
        {            
            connections.Add(new Connection(neighbour, random.NextDouble() * maxWeight));
        }

        public void PrepareToFire()
        {
            if (ants.Count >= threshold)
            {
                thresholdReached = true;
                numberOfAntsToFire = ants.Count;
            }
            else
            {
                thresholdReached = false;
                numberOfAntsToFire = 0;
            }
        }

        public void Fire()
        {
            if (thresholdReached)
            {
                for (int i = 0; i < numberOfAntsToFire; i++)
                {
                    IAnt ant = ants[0];
                    ants.RemoveAt(0);

                    IConnection connection = ChooseConnection();
                    connection.Neuron.AddAnt(ant);
                    ant.AddConnectionToHistory(connection);
                    ant.Sign *= Sign.GetSign(connection.Weight);
                }

                thresholdReached = false;
                numberOfAntsToFire = 0;
            }
        }

        public void AddAnt(IAnt ant)
        {
            ants.Add(ant);            
        }

        private IConnection ChooseConnection()
        {
            double totalWeight = connections.Sum(x => x.Weight);
            double randomWeightedIndex = random.NextDouble() * totalWeight;
            double itemWeightedIndex = 0;

            foreach (IConnection connection in connections)
            {
                itemWeightedIndex += connection.Weight;
                if (randomWeightedIndex < itemWeightedIndex)
                    return connection;
            }

            return connections[random.Next(connections.Count)];
        }

    }

}
