using System;
using System.Collections.Generic;
using System.Linq;

namespace Neural_Ant_Colony_Classification
{
    class OutputNeuron : INeuron
    {
        private int _actualOutputSign = 1;
        public int ActualOutputSign
        {
            get => _actualOutputSign;
            set
            {
                if (value == 1 || value == -1)
                    _actualOutputSign = value;
                else
                    throw new ArgumentOutOfRangeException("Sign must be either 1 or -1.");
            }
        }
        private List<IAnt> ants = new List<IAnt>();       

        public int OutputSign
        {
            get
            {
                return Sign.GetSign(ants.Sum(x => x.Sign));
            }
        }

        public double OutputValue
        {
            get
            {
                return ants.Sum(x => Math.Max(0, x.Sign)) / ants.Count;
            }
        }

        public void AddAnt(IAnt ant)
        {
            throw new NotImplementedException();
        }

        public void DistributeReward(double learningRate)
        {
            double reward = ActualOutputSign * learningRate;

            foreach (IAnt ant in ants)
            {
                ant.DistributeReward(reward);
            }

            ants.Clear();
        }

        public void ClearAnts()
        {
            ants.Clear();
        }

        public void AddNeighbour(INeuron neuron)
        {
        }

        public void PrepareToFire()
        {
        }

        public void Fire()
        {           
        }

    }
}
