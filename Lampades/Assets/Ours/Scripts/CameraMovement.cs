using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public GameObject Player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 playerPos = Player.transform.position;
        
        transform.position = new Vector3(playerPos.x, transform.position.y, transform.position.z);
        
	}
}
