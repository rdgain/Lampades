using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    public GameObject lastFloor;
    public GameObject preFloor;
    public Camera camera;

    public GameObject stickModel;
    public GameObject boxModel;
    public float probStick = 0.8f;
    public float probBox = 0.5f;
    public int numRegion = 10;

    public string[] stuffList = { "Stick", "Box" };

    // Use this for initialization
    void Start () {
    
    }
	
	// Update is called once per frame
	void Update () {

        if (camera.transform.position.x > lastFloor.transform.position.x)
        {
            GenerateNewFloor();

            if(tag.Equals("Real"))
                GenerateStuffOnNewFloor();
        }

    }

    void GenerateNewFloor()
    {
        float offset = lastFloor.GetComponent<BoxCollider2D>().size.x / 2 ;

        Vector3 newPos = new Vector3(lastFloor.transform.position.x + offset, lastFloor.transform.position.y, lastFloor.transform.position.z);

        Transform tmpParent = lastFloor.transform.parent;
        lastFloor = Instantiate(preFloor, newPos, lastFloor.transform.rotation);
        lastFloor.transform.SetParent(tmpParent);
        
    }

    void GenerateStuffOnNewFloor()
    {
        float floorLength = lastFloor.GetComponent<BoxCollider2D>().size.x / 2;
        Vector3 floorPos = lastFloor.transform.position;
        float floorPosX = floorPos.x;

        float eachRegionLength = floorLength / (float) numRegion;
        float initialPos = floorPosX - floorLength/2;

        //print("floorL " + floorLength);
        //print("eachRegionL " + eachRegionLength);
        //print("floorX " + floorPosX);
        //print("initPos " + initialPos);

        for (int i = 0; i < numRegion; i++)
        {
            float currentXPos = initialPos + (i * eachRegionLength) + eachRegionLength/2;
            float randomValue = Random.Range(0f, 1f);
            float randOffset = Random.Range(0, eachRegionLength);

            //print("curX " + currentXPos);
            //print("offset " + randOffset);

            if (randomValue < probBox)
            {
                InstantiateObject(boxModel, currentXPos + randOffset, floorPos);
            }

            randOffset = Random.Range(0, eachRegionLength);

            if (randomValue < probStick)
            {
                InstantiateObject(stickModel, currentXPos + randOffset, floorPos);
            }
        }
    }

    void InstantiateObject(GameObject model, float xPos, Vector3 floorPos)
    {
        Vector3 position = new Vector3(xPos, floorPos.y, floorPos.z);
        Instantiate(model, position, lastFloor.transform.rotation);
    }
}
