using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesManager : MonoBehaviour {

    public bool rotating = true;
    public float rotateSpeed = 5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(rotating)
            rotate();
	}

    void rotate()
    {
        transform.Rotate(new Vector3(0, 1, 0) * rotateSpeed);
    }
}
