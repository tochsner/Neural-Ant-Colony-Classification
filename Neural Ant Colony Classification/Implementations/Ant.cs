using System;
using System.Collections.Generic;

namespace Neural_Ant_Colony_Classification
{
    class Ant : IAnt
    {
        public int Id;
        private static int instancesCount = 0;

        private List<IConnection> History = new List<IConnection>();

        private int _sign = 1;
        public int Sign
        {
            get => _sign;
            set
            {
                if (value == 1 || value == -1)
                    _sign = value;
                else
                    throw new ArgumentOutOfRangeException("Sign must be either 1 or -1.");
            }
        }

        public Ant()
        {
            Id = instancesCount;
            instancesCount++;
        }

        public void AddConnectionToHistory(IConnection connection)
        {
            History.Add(connection);
        }

        public void DistributeReward(double reward)
        {
            reward = reward * Sign / History.Count;
            foreach (IConnection connection in History)
            {
                connection.DistributeReward(reward);
            }
        }
    }
}
