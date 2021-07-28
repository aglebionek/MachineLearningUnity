using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrainingData {
    public int[] inputs;
    public int output;
}

public class Perceptron : MonoBehaviour {
    TrainingData[] trainingData = new TrainingData[4];
    double[] weights = new double[2];
    double bias;
    int totalError;

    void Start() {
        trainingData[0].inputs = new int[] { 0, 0 };
        trainingData[0].output = 0;
        trainingData[1].inputs = new int[] { 0, 1 };
        trainingData[1].output = 1;
        trainingData[2].inputs = new int[] { 1, 0 };
        trainingData[2].output = 1;
        trainingData[3].inputs = new int[] { 1, 1 };
        trainingData[3].output = 1;
        Train();
    }

    void GenerateWeightsAndBias() {
        for (int i = 0; i < weights.Length; i++) {
            weights[i] = Random.Range(-1f, 1f);
        }
        bias = Random.Range(-1f, 1f);
    }

    double CalculateDotProduct(int[] inputs, double[] weights) {
        if (inputs == null || weights == null) return -1;
        if (inputs.Length != weights.Length) return -1;

        double product = 0;
        for (int i = 0; i < inputs.Length; i++) {
            product += inputs[i] * weights[i];
        }
        product += bias;
        return product;
    }

    int CalculateOutput(int index) {
        double dotProduct = CalculateDotProduct(trainingData[index].inputs, weights);
        if (dotProduct > 0) return 1;
        return 0;
    }

    void Train() {
        GenerateWeightsAndBias();
        while (true) {
            totalError = 0;
            for (int i = 0; i < trainingData.Length; i++) {
                UpdateWeights(i);
                Debug.Log(string.Format("W1: {0}, W2: {1}, B: {2}", weights[0], weights[1], bias));
            }
            if (totalError == 0) break;
        }
        Debug.Log("Total error: " + totalError);
    }

    void UpdateWeights(int index) {
        int error = trainingData[index].output - CalculateOutput(index);
        totalError += Mathf.Abs(error);
        for (int i = 0; i < weights.Length; i++) {
            weights[i] += error * trainingData[index].inputs[i];
        }
        bias += error;
    }
}
