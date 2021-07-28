using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
    public float r;
    public float g;
    public float b;
    public float s;
    public float timeToDie = 0;
    SpriteRenderer sRenderer;
    Collider2D sCollider;

    void OnMouseDown() {
        timeToDie = PopulationManager.elapsed;
        sRenderer.enabled = false;
        sCollider.enabled = false;
    }
    void Start() {
        sRenderer = GetComponent<SpriteRenderer>();
        sCollider = GetComponent<Collider2D>();
        sRenderer.color = new Color(r, g, b);
        this.transform.localScale = new Vector3(s, s, s);
    }

    
    void Update() {
        
    }
}
