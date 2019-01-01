using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_Ant_Colony_Classification
{
    public class ClassificationNAC : IClassificationNeuralAntColony
    {
        private int maxThreshold;
        private double maxWeight;
        private double learningRate;

        private List<InputNeuron> inputNeurons = new List<InputNeuron>();        
        private List<List<INeuron>> hiddenNeurons = new List<List<INeuron>>();
        private List<OutputNeuron> outputNeurons = new List<OutputNeuron>();      

        public List<int> OutputSigns => outputNeurons.Select(x => x.OutputSign).ToList();                    
        public List<double> OutputValues => outputNeurons.Select(x => x.OutputValue).ToList();        
        public double Accuracy
        {
            get
            {
                int correct = 0;

                foreach (OutputNeuron neuron in outputNeurons)
                {
                    if (neuron.OutputSign == neuron.ActualOutputSign)
                        correct++;
                }

                return 1.0 * correct / outputNeurons.Count;
            }

        }

        public ClassificationNAC(int maxThreshold, double maxWeight, double learningRate)
        {
            this.maxThreshold = maxThreshold;
            this.maxWeight = maxWeight;
            this.learningRate = learningRate;
        }

        public void InitializeColony(int numberInputNeurons, int hiddenLayersCount, int numberHiddenNeuronsPerLayer, int numberOutputNeurons)
        {
            for (int i = 0; i < numberInputNeurons; i++)
            {
                inputNeurons.Add(new InputNeuron(maxWeight));
            }

            for (int i = 0; i < hiddenLayersCount; i++)
            {
                hiddenNeurons.Add(new List<INeuron>());
                for (int j = 0; j < numberHiddenNeuronsPerLayer; j++)
                {
                    hiddenNeurons.Last().Add(new Neuron(maxThreshold, maxWeight));
                }
            }

            for (int i = 0; i < numberOutputNeurons; i++)
            {
                outputNeurons.Add(new OutputNeuron());
            }

            ConnectNeurons();
        }

        private void ConnectNeurons()
        {
            // Connect the neurons:
            // Every neuron is connected with its direct neighbours in its own layer and its precending and successive layers.
            // The input and output neurons are connected with all neurons in the layer next to them

            foreach (INeuron inputNeuron in inputNeurons)
            {
                foreach (INeuron neuron in hiddenNeurons.First())
                {
                    inputNeuron.AddNeighbour(neuron);
                }
            }

            for (int l = 0; l < hiddenNeurons.Count; l++)
            {
                for (int n = 0; n < hiddenNeurons[l].Count; n++)
                {
                    INeuron neuron = hiddenNeurons[l][n];

                    // neighbours in its own layer
                    if (n > 0)
                        neuron.AddNeighbour(hiddenNeurons[l][n - 1]);
                    if (n < hiddenNeurons[l].Count - 1)
                        neuron.AddNeighbour(hiddenNeurons[l][n + 1]);

                    // neighbours in the precending and successive layers
                    if (l > 0)
                        neuron.AddNeighbour(hiddenNeurons[l - 1][n]);
                    if (l < hiddenNeurons.Count - 1)
                        neuron.AddNeighbour(hiddenNeurons[l + 1][n]);
                }
            }

            foreach (INeuron outputNeuron in outputNeurons)
            {
                foreach (INeuron neuron in hiddenNeurons.Last())
                {
                    neuron.AddNeighbour(outputNeuron);
                }
            }
        }

        public void SetInputOutput(List<int> inputValues, List<int> outputSigns)
        {
            for (int i = 0; i < inputValues.Count; i++)
            {
                inputNeurons[i].InputValue = inputValues[i];
            }

            for (int i = 0; i < outputSigns.Count; i++)
            {
                outputNeurons[i].ActualOutputSign = outputSigns[i];
            }
        }

        public void RunIteration()
        {
            RunIteration(true);
        }
        public void RunIteration(bool distributeRewards)
        {
            // Prepare neurons to fire

            foreach (INeuron inputNeuron in inputNeurons)
            {
                inputNeuron.PrepareToFire();                
            }
            foreach (List<INeuron> layer in hiddenNeurons)
            {
                foreach (INeuron neuron in layer)
                {
                    neuron.PrepareToFire();
                }
            }
            foreach (INeuron outputNeuron in outputNeurons)
            {
                outputNeuron.PrepareToFire();
            }

            // Fire
            foreach (INeuron inputNeuron in inputNeurons)
            {
                inputNeuron.Fire();
            }
            foreach (List<INeuron> layer in hiddenNeurons)
            {
                foreach (INeuron neuron in layer)
                {
                    neuron.Fire();
                }
            }
            foreach (INeuron outputNeuron in outputNeurons)
            {
                outputNeuron.Fire();
            }

            // Distribute rewards
            if (distributeRewards)
            {
                foreach (OutputNeuron neuron in outputNeurons)
                {
                    neuron.DistributeReward(learningRate);
                }
            }
        }

        public double EvaluateAccuracy(int numberIterations)
        {
            for (int i = 0; i < numberIterations; i++)
            {
                RunIteration(false);
            }

            return Accuracy;
        }
    }
}
