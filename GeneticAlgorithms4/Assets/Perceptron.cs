using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[System.Serializable]
public class TrainingSet {
    public double[] input;
    public double output;
}

public class Perceptron : MonoBehaviour {

    public TrainingSet[] ts;
    double[] weights = { 0, 0 };
    double bias = 0;
    double totalError = 0;

    double DotProductBias(double[] inputs, double[] weights) {
        if (inputs == null || weights == null) return -1;
        if (inputs.Length != weights.Length) return -1;

        double d = 0;
        for (int i = 0; i < inputs.Length; i++) {
            d += inputs[i] * weights[i];
        }
        d += bias;
        return d;
    }

    double CalcOutput(int i) {
        double dp = DotProductBias(weights, ts[i].input);
        if (dp > 0) return 1;
        return 0;
    }

    double CalcOutput(double i1, double i2) {
        double[] inp = new double[] { i1, i2 };
        double dp = DotProductBias(weights, inp);
        if (dp > 0) return 1;
        return 0;
    }

    void InitializeWeights() {
        for (int i = 0; i < weights.Length; i++) {
            weights[i] = Random.Range(-1f, 1f);
        }
        bias = Random.Range(-1f, 1f);
    }

    void Train() {
        InitializeWeights();

        while (true) { 
            totalError = 0;
            for (int j = 0; j < ts.Length; j++) {
                UpdateWeights(j);
                Debug.Log(string.Format("W1: {0}, W2: {1}, B: {2}", weights[0], weights[1], bias));
            }
            Debug.Log("Total error: " + totalError);
            if (totalError == 0) break;
        }
    }

    void UpdateWeights(int j) {
        double error = ts[j].output - CalcOutput(j);
        totalError += Mathf.Abs((float)error);
        for (int i = 0; i < weights.Length; i++) {
            weights[i] += error * ts[j].input[i];
        }
        bias += error;
    }

    void Start() {
        Train();
        Debug.Log("Test: 0 || 0 " + CalcOutput(0, 0));
        Debug.Log("Test: 0 || 1 " + CalcOutput(0, 1));
        Debug.Log("Test: 1 || 0 " + CalcOutput(1, 0));
        Debug.Log("Test: 1 || 1 " + CalcOutput(1, 1));
    }
}
