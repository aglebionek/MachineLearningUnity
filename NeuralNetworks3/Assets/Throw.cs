using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {
    public GameObject spherePrefab;
    public GameObject cubePrefab;
    public Material red;
    public Material green;

    Perceptron perceptron;
    
    void Start() {
        perceptron = GetComponent<Perceptron>();
    }

    void Update() {
        if (Input.GetKeyDown("1")) {
            GameObject redSphere = Instantiate(spherePrefab, Camera.main.transform.position, Camera.main.transform.rotation);
            redSphere.GetComponent<Renderer>().material = red;
            redSphere.GetComponent<Rigidbody>().AddForce(0, 0, 500);
            perceptron.SendInput(0, 0, 0);
        } else if (Input.GetKeyDown("2")) {
            GameObject greenSphere = Instantiate(spherePrefab, Camera.main.transform.position, Camera.main.transform.rotation);
            greenSphere.GetComponent<Renderer>().material = green;
            greenSphere.GetComponent<Rigidbody>().AddForce(0, 0, 500);
            perceptron.SendInput(0, 1, 1);
        } else if (Input.GetKeyDown("3")) {
            GameObject redCube = Instantiate(cubePrefab, Camera.main.transform.position, Camera.main.transform.rotation);
            redCube.GetComponent<Renderer>().material = red;
            redCube.GetComponent<Rigidbody>().AddForce(0, 0, 500);
            perceptron.SendInput(1, 0, 1);
        } else if (Input.GetKeyDown("4")) {
            GameObject redSphere = Instantiate(cubePrefab, Camera.main.transform.position, Camera.main.transform.rotation);
            redSphere.GetComponent<Renderer>().material = green;
            redSphere.GetComponent<Rigidbody>().AddForce(0, 0, 500);
            perceptron.SendInput(1, 1, 1);
        }
    }
}
