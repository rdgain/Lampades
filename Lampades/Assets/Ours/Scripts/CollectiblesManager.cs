using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesManager : MonoBehaviour {

    public bool rotating = true;
    public bool collected = false, stuck = false;
    public Vector3 stuckPosition;
    public float rotateSpeed = 5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (collected && stuck)
        {
            transform.localPosition = stuckPosition;
            print(name+" "+transform.position);
        }

        if(rotating)
            rotate();
	}

    void rotate()
    {
        transform.Rotate(new Vector3(0, 1, 0) * rotateSpeed);
    }
}
