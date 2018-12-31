using System.Collections.Generic;

namespace Neural_Ant_Colony_Classification
{
    class InputNeuron : Neuron
    {
        private int inputValue = 0;

        public InputNeuron(double maxWeight) : base(0, maxWeight)
        {
        }

        public int InputValue
        {
            get => inputValue;

            set
            {
                inputValue = value;

                ClearAnts();
                for (int i = 0; i < inputValue; i++)
                {
                    AddAnt(new Ant());
                }
            }           
        }        
    }
}
