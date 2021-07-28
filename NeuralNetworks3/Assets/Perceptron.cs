using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class TrainingData {
    public double[] inputs;
    public double output;
}

public class Perceptron : MonoBehaviour {

    List<TrainingData> trainingData = new List<TrainingData>();
    double[] weights = new double[2];
    double bias;
    double totalError = 0;

    public GameObject npc;

    void Start() {
        GenerateWeightsAndBias();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            GenerateWeightsAndBias();
            trainingData.Clear();
        } else if (Input.GetKeyDown(KeyCode.S)) {
            SaveWeights();
        } else if (Input.GetKeyDown(KeyCode.L)) {
            LoadWeights();
        } else if (Input.GetKeyDown(KeyCode.P)) {
            Show();
        }
    }

    public void SendInput(double input1, double input2, double output) {
        double result = TestOutput(input1, input2);
        Debug.Log(result);
        if (result == 0) {
            npc.GetComponent<Animator>().SetTrigger("Crouch");
            npc.GetComponent<Rigidbody>().isKinematic = false;
        } else {
            npc.GetComponent<Rigidbody>().isKinematic = true;
        }
        TrainingData td = new TrainingData();
        td.inputs = new double[2] { input1, input2 };
        td.output = output;
        trainingData.Add(td);
        Train();
    }

    void Train() {
        for (int i = 0; i < trainingData.Count; i++) {
            UpdateWeights(i);
        }
    }
    void GenerateWeightsAndBias() {
        for (int i = 0; i < weights.Length; i++) {
            weights[i] = Random.Range(-1f, 1f);
        }
        bias = Random.Range(-1f, 1f);
    }
    void UpdateWeights(int index) {
        double error = trainingData[index].output - CalculateOutput(index);
        totalError += Mathf.Abs((float)error);
        for (int i = 0; i < weights.Length; i++) {
            weights[i] += error * trainingData[index].inputs[i];
        }
        bias += error;
    }
    double CalculateOutput(int index) {
        return (ActivationFunction(CalculateDotProduct(trainingData[index].inputs, weights)));
    }
    double ActivationFunction(double dotProduct) {
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
        return (ActivationFunction(CalculateDotProduct(inputs, weights)));
    }

    void LoadWeights() {
        string path = Application.dataPath + "/weights.txt";
        if (File.Exists(path)) {
            var sr = File.OpenText(path);
            string line = sr.ReadLine();
            string[] readWeights = line.Split(',');
            weights[0] = System.Convert.ToDouble(readWeights[0]);
            Debug.Log(weights[0]);
            weights[1] = System.Convert.ToDouble(readWeights[1]);
            Debug.Log(weights[1]);
            bias = System.Convert.ToDouble(readWeights[2]);
            Debug.Log(bias);
            Debug.Log("Loading");
            sr.Close();
        }
    }

    void SaveWeights() {
        string path = Application.dataPath + "/weights.txt";
        var sr = File.CreateText(path);
        sr.WriteLine(weights[0].ToString() + ',' + weights[1].ToString() + ',' + bias.ToString());
        sr.Close();
    }

    void Show() {
        Debug.Log(weights[0].ToString() + ',' + weights[1].ToString() + ',' + bias.ToString());
    }
}