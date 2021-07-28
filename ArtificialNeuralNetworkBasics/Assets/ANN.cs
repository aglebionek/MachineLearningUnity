using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANN {
    public int numInputs;
    public int numOutputs;
    public int numHidden;
    public int numPerHidden;
    public double alpha;
    List<Layer> layers = new List<Layer>();

    public ANN(int _numInputs, int _numOutputs, int _numHidden, int _numPerHidden, double _alpha) {
        numInputs = _numInputs;
        numOutputs = _numOutputs;
        numHidden = _numHidden;
        numPerHidden = _numPerHidden;
        alpha = _alpha;

        if (numHidden > 0) {
            layers.Add(new Layer(numPerHidden, numInputs));

            for (int i = 0; i < numHidden-1; i++) {
                layers.Add(new Layer(numPerHidden, numPerHidden));
            }
        }
    }
}
