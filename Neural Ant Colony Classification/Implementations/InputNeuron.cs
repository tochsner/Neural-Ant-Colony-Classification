using System.Collections.Generic;

namespace Neural_Ant_Colony_Classification
{
    class InputNeuron : Neuron
    {
        private int _inputValue = 0;

        public InputNeuron(double maxWeight) : base(0, maxWeight)
        {            
        }

        public int InputValue
        {
            get => _inputValue;

            set
            {
                _inputValue = value;

                ants.Clear();
                for (int i = 0; i < _inputValue; i++)
                {
                    AddAnt(new Ant());
                }
            }           
        }      
        
        public override void PrepareToFire()
        {            
            ants.Clear();
            for (int i = 0; i < _inputValue; i++)
            {
                AddAnt(new Ant());
            }

            base.PrepareToFire();
        }
    }
}
