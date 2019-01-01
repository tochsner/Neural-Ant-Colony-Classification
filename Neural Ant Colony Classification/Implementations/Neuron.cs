using System;
using System.Collections.Generic;
using System.Linq;

namespace Neural_Ant_Colony_Classification
{
    class Neuron : INeuron
    {
        private static readonly Random random = new Random();
        private static int instancesCount = 0;

        public int Id;

        protected List<IAnt> ants  = new List<IAnt>();
        protected List<IConnection> connections = new List<IConnection>();        
        
        private double maxWeight;

        protected int threshold;
        private bool thresholdReached = false;
        private int numberOfAntsToFire = 0;        

        public Neuron(int maxThreshold, double maxWeight)
        {
            threshold = random.Next(maxThreshold);
            this.maxWeight = maxWeight;

            Id = instancesCount;
            instancesCount++;
        }

        public void AddNeighbour(INeuron neighbour)
        {            
            connections.Add(new Connection(neighbour, random.NextDouble() * maxWeight));
        }

        public void AddAnt(IAnt ant)
        {
            ants.Add(ant);
        }

        public virtual void PrepareToFire()
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

        public virtual void Fire()
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

        private IConnection ChooseConnection()
        {
            double totalWeight = connections.Sum(x => Math.Abs(x.Weight));
            double randomWeightedIndex = random.NextDouble() * totalWeight;
            double itemWeightedIndex = 0;

            foreach (IConnection connection in connections)
            {
                itemWeightedIndex += Math.Abs(connection.Weight);
                if (randomWeightedIndex < itemWeightedIndex)
                    return connection;
            }

            return connections[random.Next(connections.Count)];
        }
    }
}
