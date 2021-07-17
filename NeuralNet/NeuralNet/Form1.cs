using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralNet
{
    public partial class Form1 : Form
    {
        private List<MNISTImage> training_data = new List<MNISTImage>();
        private List<MNISTImage> test_data = new List<MNISTImage>();
        private static Random r = new Random();
        private static NeuralNetwork NN;
        private int epochs = 100;

        class NeuralNetwork
        {
            public int[] layers;
            public double[][] neurons;
            public double[][] biases;
            public double[][][] weights;

            public double fitness = 0;

            public double learningRate = 0.01;
            public double cost = 0;

            public NeuralNetwork(int[] layers)
            {
                //initialize layers array
                //create array for layers pointer and populate with passed in array values
                this.layers = new int[layers.Length];
                for (int i = 0; i < layers.Length; i++)
                    this.layers[i] = layers[i];

                //initialize neurons - possibly change as it's kinda jank
                List<double[]> neuronsList = new List<double[]>();
                for (int i = 0; i < layers.Length; i++)
                    neuronsList.Add(new double[layers[i]]);
                neurons = neuronsList.ToArray();

                //initialize biases
                List<double[]> biasList = new List<double[]>();
                for (int i = 0; i < layers.Length; i++)
                {
                    double[] bias = new double[layers[i]];
                    for (int j = 0; j < layers[i]; j++)
                        bias[j] = r.NextDouble();
                    biasList.Add(bias);
                }
                biases = biasList.ToArray();

                //initialize weights
                List<double[][]> weightsList = new List<double[][]>();
                for (int i = 1; i < layers.Length; i++)
                {
                    List<double[]> layerWeightsList = new List<double[]>();
                    int previousLayerNeurons = layers[i - 1];
                    for (int j = 0; j < neurons[i].Length; j++)
                    {
                        double[] neuronWeights = new double[previousLayerNeurons];
                        for (int k = 0; k < previousLayerNeurons; k++)
                            neuronWeights[k] = r.NextDouble();
                        layerWeightsList.Add(neuronWeights);
                    }
                    weightsList.Add(layerWeightsList.ToArray());
                }
                weights = weightsList.ToArray();
            }

            public double[] FeedForward(double[] inputs)
            {
                for (int i = 0; i < inputs.Length; i++)
                {
                    neurons[0][i] = inputs[i];
                }
                for (int i = 1; i < layers.Length; i++)
                {
                    int layer = i - 1;
                    for (int j = 0; j < neurons[i].Length; j++)
                    {
                        double value = 0.0;
                        for (int k = 0; k < neurons[i - 1].Length; k++)
                        {
                            double value1 = weights[i - 1][j][k];
                            double value2 = neurons[i - 1][k];
                            value += value1 * value2;
                        }
                        neurons[i][j] = sigmoid_output(value + biases[i][j]);
                    }
                }
                return neurons[neurons.Length - 1];
            }

            public void BackPropagate(double[] inputs, int[] expected)
            {
                double[] output = FeedForward(inputs);

                //find cost of NN (not used for anything but analysis
                cost = 0;
                for (int i = 0; i < output.Length; i++)
                    cost += Math.Pow(output[i] - expected[i], 2);
                cost = cost / 2;

                double[][] error;

                //initialize gamma
                List<double[]> errorList = new List<double[]>();
                for (int i = 0; i < layers.Length; i++)
                    errorList.Add(new double[layers[i]]);
                error = errorList.ToArray();

                //calculate error for the output layer of neurons
                int layer = layers.Length - 2;
                for (int i = 0; i < output.Length; i++)
                    error[layers.Length - 1][i] = (output[i] - expected[i]) * sigmoid_derivative(output[i]);


                for (int i = 0; i < layers[layers.Length-1]; i++)
                {
                    biases[layers.Length - 2][i] -= error[layers.Length - 1][i] * learningRate;
                    for (int j = 0; j < layers[layers.Length - 2]; j++)
                        weights[layers.Length - 2][i][j] -= error[layers.Length - 1][i] * neurons[layers.Length - 2][j] * learningRate;
                }

                //hidden layers
                for (int i = layers.Length - 2; i > 0; i--)
                {
                    layer = i - 1;
                    for (int j = 0; j < layers[i]; j++)
                    {
                        error[i][j] = 0;
                        for (int k = 0; k < error[i + 1].Length; k++)
                            error[i][j] += error[i + 1][k] * weights[i][k][j];

                        error[i][j] *= sigmoid_derivative(neurons[i][j]);
                    }

                    for (int j = 0; j < layers[i]; j++)
                    {
                        biases[i - 1][j] -= error[i][j] * learningRate;
                        for (int k = 0; k < layers[i - 1]; k++)
                            weights[i - 1][j][k] -= error[i][j] * neurons[i - 1][k] * learningRate;  //change weights
                    }
                }
            }

            
        }

        class MNISTImage
        {
            //holds the ACTUAL value of the image
            public int result;
            //holds the pixels of the image, range from 0 - 1
            public List<double> pixel = new List<double>();
        }

        //calculates sigmoid value an returns as a double
        private static double sigmoid_output(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }

        private static double sigmoid_derivative(double x)
        {
            double y = sigmoid_output(x);
            return y * (1.0 - y);
        }

        private void loadData()
        {
            string line;
            int counter = 0;

            Console.WriteLine("STARTED LOADING TRAINING-DATA");
            TextReader reader = File.OpenText("mnist_train.csv");

            while ((line = reader.ReadLine()) != null)
            {
                string[] tokens = line.Split(',');
                MNISTImage tempImage = new MNISTImage();
                tempImage.result = int.Parse(tokens[0]);

                for (int i = 1; i < tokens.Count(); i++)
                {
                    tempImage.pixel.Add((int.Parse(tokens[i])) / 255.0);
                }
                training_data.Add(tempImage);

                counter++;
                if (counter % 5000 == 0)
                    Console.WriteLine(counter + " items loaded");
            }
            Console.WriteLine("DONE LOADING TRAINING-DATA");

            Console.WriteLine("STARTED LOADING TESTING-DATA");
            reader = File.OpenText("mnist_test.csv");

            while ((line = reader.ReadLine()) != null)
            {
                string[] tokens = line.Split(',');
                MNISTImage tempImage = new MNISTImage();
                tempImage.result = int.Parse(tokens[0]);

                for (int i = 1; i < tokens.Count(); i++)
                {
                    tempImage.pixel.Add((int.Parse(tokens[i])) / 255.0);
                }
                test_data.Add(tempImage);

                counter++;
                if (counter % 5000 == 0)
                    Console.WriteLine(counter + " items loaded");
            }
        }

        private void testNet()
        {
            int correct_results = 0;
            int total_tests = 0;

            Console.WriteLine("TESTING");
            double[] result_neurons;
            for (int i = 0; i < test_data.Count(); i++)
            {
                result_neurons = NN.FeedForward(test_data[i].pixel.ToArray());

                double max_value = 0.0;
                int max_index = 99;
                for (int x = 0; x < result_neurons.Length; x++)
                {
                    if (result_neurons[x] > max_value)
                    {
                        max_value = result_neurons[x];
                        max_index = x;
                    }
                }

                if (max_index == test_data[i].result)
                {
                    correct_results++;
                }
                total_tests++;
            }
            string printline = correct_results + " / " + total_tests;
            ResultInfo.Items.Add(printline);
            Console.WriteLine(printline);
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void createNN_Click(object sender, EventArgs e)
        {
            //create NN object, should be persistant through all button pushes
            //parse total epochs from textbox
            epochs = Int32.Parse(EpochTextBox.Text);

            //parse layers into array to pass to NN on creation
            string[] words = LayerTextBox.Text.Split(',');
            int[] layersArr = new int[words.Length + 2];
            layersArr[0] = 784;  //trivial values (input values)
            layersArr[words.Length + 1] = 10; //trival values (output nodes)
            int i = 1;
            foreach (var word in words)
            {
                layersArr[i] = Int32.Parse(word);
                i++;
            }
            
            //creating NN
            NN = new NeuralNetwork(layersArr);
            Console.WriteLine("done creating and randomizing NN");
        }

        private void trainNN_Click(object sender, EventArgs e)
        {
            int counter = 0;

            loadData();

            int[] expected_result = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < epochs; i++)
            {
                Console.WriteLine("training: " + (i + 1) + " / " + epochs);

                counter = 0;
                for (int j = 0; j < training_data.Count(); j++)
                {
                    if (counter % 10000 == 0)
                        Console.WriteLine(counter + "processed (train)");
                    expected_result[training_data[j].result] = 1;
                    NN.BackPropagate(training_data[j].pixel.ToArray(), expected_result);
                    expected_result[training_data[j].result] = 0;
                    counter++;
                }

                testNet();
            }
        }

        private void testNN_Click(object sender, EventArgs e)
        {
            int counter = 0;
            int correct_results = 0;
            int total_tests = 0;

            counter = 0;
            double[] result_neurons;
            for (int i = 0; i < test_data.Count(); i++)
            {
                if (counter % 1000 == 0)
                    Console.WriteLine(counter + "processed (test)");
                result_neurons = NN.FeedForward(test_data[i].pixel.ToArray());

                double max_value = 0.0;
                int max_index = 99;
                for (int x = 0; x < result_neurons.Length; x++)
                {
                    if (result_neurons[x] > max_value)
                    {
                        max_value = result_neurons[x];
                        max_index = x;
                    }
                }

                if (max_index == test_data[i].result)
                {
                    correct_results++;
                }

                total_tests++;
                counter++;
            }
            string printline = correct_results + " / " + total_tests;
            ResultInfo.Items.Add(printline);
        }
    }
}
