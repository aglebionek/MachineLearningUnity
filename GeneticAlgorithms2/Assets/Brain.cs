using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent (typeof (ThirdPersonCharacter))]
public class Brain : MonoBehaviour {
    public int DNALength = 1;
    public float timeAlive;
    public float distanceRun;
    public DNA dna;

    private ThirdPersonCharacter mCharacter;
    private Vector3 mMove;
    private bool mJump;
    private float startingZ;
    bool alive = true;

    void OnCollisionEnter (Collision obj) {
        if (obj.gameObject.tag == "dead") alive = false;
    }

    public void Init () {
        //init DNA
        //0 forward
        //1 back
        //2 left
        //3 right
        //4 jump
        //5 crouch
        dna = new DNA (DNALength, 6);
        mCharacter = GetComponent<ThirdPersonCharacter>();
        timeAlive = 0;
        distanceRun = 0;
        startingZ = this.transform.position.z;
        alive = true;
    }

    // Update is called once per frame
    private void FixedUpdate () {
        float v = 0;
        float h = 0;
        bool crouch = false;

        if (dna.GetGene (0) == 0) v = 1;
        else if (dna.GetGene (0) == 1) v = -1;
        else if (dna.GetGene (0) == 2) h = -1;
        else if (dna.GetGene (0) == 3) h = 1;
        else if (dna.GetGene (0) == 4) mJump = true;
        else if (dna.GetGene (0) == 5) crouch = true;

        mMove = v * Vector3.forward + h * Vector3.right;
        mCharacter.Move (mMove, crouch, mJump);
        mJump = false;
        if (alive) {
            timeAlive += Time.deltaTime;
            distanceRun = startingZ - this.transform.position.z;
        }
    }

}