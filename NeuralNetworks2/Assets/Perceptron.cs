using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[System.Serializable]
public class TrainingData {
    public double[] inputs;
    public double output;
}

public class Perceptron : MonoBehaviour {

    public TrainingData[] trainingData;
    double[] weights = new double[2];
    double bias;
    double totalError;
    int numberOfLoops = 0;

    public SimpleGrapher simpleGrapher;
    //Input
    //0 - edibleness
    //1 - sharpness
    //Output
    //0 - weapon
    //1 - food
    
    void Start() {
        DrawAllPoints();
        Train();

        if (TestOutput(0.3, 0.9) == 0) simpleGrapher.DrawPoint(0.3f, 0.9f, Color.red);
        else simpleGrapher.DrawPoint(0.3f, 0.9f, Color.yellow);
    }

    void Train() {
        GenerateWeightsAndBias();
        while (true) {
            totalError = 0;
            numberOfLoops++;
            for (int i = 0; i < trainingData.Length; i++) {
                UpdateWeights(i);
                Debug.Log(string.Format("W1: {0}, W2: {1}, B: {2}", weights[0], weights[1], bias));
            }
            Debug.Log("Total error: " + totalError);
            Debug.Log(numberOfLoops);
            simpleGrapher.DrawRay((float)(-(bias / weights[1]) / (bias / weights[0])), (float)(-bias / weights[1]), Color.red);
            if (totalError == 0) break;
        }
    }

    void GenerateWeightsAndBias() {
        for (int i = 0; i < weights.Length; i++) {
            weights[i] = Random.Range(-1f, 1f);
        }
        bias = Random.Range(-1f, 1f);
        simpleGrapher.DrawRay((float)(-(bias / weights[1]) / (bias / weights[0])), (float)(-bias / weights[1]), Color.red);
    }
    void UpdateWeights(int index) {
        double error = trainingData[index].output - CalculateOutput(index);
        totalError += Mathf.Abs((float)error);
        for (int i = 0; i < weights.Length; i++) {
            weights[i] += error * trainingData[index].inputs[i];
        }
        bias += error;
    }

    int CalculateOutput(int index) {
        double dotProduct = CalculateDotProduct(trainingData[index].inputs, weights);
        if (dotProduct > 0) return 1;
        return 0;
    }
    double CalculateDotProduct(double[] inputs, double[] weights) {
        if (inputs == null || weights == null) return -1;
        if (inputs.Length != weights.Length) return -1;

        double product = 0;
        for (int i = 0; i < inputs.Length; i++) {
            product += inputs[i] * weights[i];
        }
        product += bias;
        return product;
    }

    double TestOutput(double input1, double input2) {
        double[] inputs = new double[] { input1, input2 };
        double dotProduct = CalculateDotProduct(inputs, weights);
        if (dotProduct > 0) return 1;
        return 0;
    }

    void DrawAllPoints() {
        for (int i = 0; i < trainingData.Length; i++) {
            if (trainingData[i].output == 0) simpleGrapher.DrawPoint(
                (float)trainingData[i].inputs[0],
                (float)trainingData[i].inputs[1],
                Color.magenta
                );
            else simpleGrapher.DrawPoint(
                (float)trainingData[i].inputs[0],
                (float)trainingData[i].inputs[1],
                Color.green
                );
        }
    }
}
