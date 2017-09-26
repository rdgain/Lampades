using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour {

    public GameObject lastFloor;
    public GameObject preFloor;
    public Camera camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (camera.transform.position.x > lastFloor.transform.position.x)
        {
            float offset = lastFloor.GetComponent<BoxCollider2D>().size.x / 2;

            Vector3 newPos = new Vector3(lastFloor.transform.position.x + offset, lastFloor.transform.position.y, lastFloor.transform.position.z);

            Transform tmpParent = lastFloor.transform.parent;
            lastFloor = Instantiate(preFloor,newPos, lastFloor.transform.rotation);
            lastFloor.transform.SetParent(tmpParent);

        }

    }
}
