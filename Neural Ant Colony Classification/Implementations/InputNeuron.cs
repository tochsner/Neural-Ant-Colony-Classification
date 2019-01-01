namespace Neural_Ant_Colony_Classification
{
    class InputNeuron : Neuron
    {
        public InputNeuron(double maxWeight) : base(0, maxWeight)
        {            
        }

        public int InputValue;
        
        public override void PrepareToFire()
        {
            CreateAnts();

            base.PrepareToFire();
        }

        private void CreateAnts()
        {
            ants.Clear();
            for (int i = 0; i < InputValue; i++)
            {
                AddAnt(new Ant());
            }
        }
    }
}