using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    public GameObject lastFloor;
    public GameObject preFloor;
    public GameObject preLight;
    public GameObject realLight;
    public Camera camera;

    int countPlatform;
   
    public GameObject player;
    float moveForce;

    public string[] stuffList = Constants.stuffsList;

    public float[] probList;

    public int numRegion = 1;

    // Use this for initialization
    void Start () {
        countPlatform = Constants.platform_num;
        moveForce = player.GetComponent<PlayerControl>().moveForce;
    }
	
	// Update is called once per frame
	void Update () {
        ManageFloor();

        if (isPlayerInsideLight())
        {
            player.GetComponent<JointManager>().insideLight = true;
        }

        else
        {
            player.GetComponent<JointManager>().insideLight = false;
        }
    }

    bool isPlayerInsideLight()
    {

        if (realLight == null)
            return false;

        Bounds playerBound = player.GetComponent<Collider2D>().bounds;
        Bounds lightBound = realLight.GetComponent<Collider2D>().bounds;

        //if (!tag.Equals("Real"))
        //{
        //    print("light: " + lightBound);
        //    print("player: min " + playerBound.min + ", max " + playerBound.max);

        //    print(lightBound.Contains(playerBound.max) && lightBound.Contains(playerBound.min));
        //}
        return lightBound.Contains(playerBound.max) && lightBound.Contains(playerBound.min);
    }

    void ManageFloor()
    {
        if (camera.transform.position.x > lastFloor.transform.position.x)
        {
            if (countPlatform > 1)
            {
                GenerateNewFloor();
                if (tag.Equals(Constants.REAL_TAG))
                    GenerateStuffOnNewFloor();

                countPlatform--;
            }

            //end of level
            else if(countPlatform == 1)
            {
                GenerateNewFloor();
                PutLightOnNewFloor();
                countPlatform = 0;
            }

        }
    }

    void PutLightOnNewFloor()
    {
        
        if(tag.Equals(Constants.REAL_TAG))
        {
            Transform tmpParent = lastFloor.transform.parent;
            realLight = Instantiate(preLight, lastFloor.transform.position, preLight.transform.rotation);

            realLight.transform.SetParent(tmpParent);
            //realLight.GetComponent<SpriteRenderer>().color = Color.black;
        }

    }

    void GenerateNewFloor()
    {
        float offset = lastFloor.GetComponent<BoxCollider2D>().size.x * lastFloor.transform.localScale.x ;

        Vector3 newPos = new Vector3(lastFloor.transform.position.x + offset, lastFloor.transform.position.y, lastFloor.transform.position.z);

        Transform tmpParent = lastFloor.transform.parent;
        lastFloor = Instantiate(preFloor, newPos, lastFloor.transform.rotation);
        lastFloor.transform.SetParent(tmpParent);

        player.GetComponent<PlayerControl>().floor = lastFloor;
        
    }

    void GenerateStuffOnNewFloor()
    {

        float floorLength = lastFloor.GetComponent<BoxCollider2D>().size.x * lastFloor.transform.localScale.x;
        Vector3 floorPos = lastFloor.transform.position;
        float floorPosX = floorPos.x;

        float eachRegionLength = floorLength / (float) numRegion;
        float initialPos = floorPosX - floorLength/2;

        for (int i = 0; i < numRegion; i++)
        {
            float currentXPos = initialPos + (i * eachRegionLength) + eachRegionLength/2;
            float randomValue = Random.Range(0f, 1f);
            float randOffset = Random.Range(0, eachRegionLength);

            for(int j=0;j<stuffList.Length;j++)
            {
                if(randomValue < probList[j])
                {
                    InstantiateObject(Constants.getModel(Constants.AVATAR_NAME, stuffList[j]), currentXPos + randOffset, floorPos);
                }
            }
        }
    }

    void InstantiateObject(GameObject model, float xPos, Vector3 floorPos)
    {
        Vector3 position = new Vector3(xPos, floorPos.y+0.5f, floorPos.z);
        GameObject newBox = Instantiate(model, position, lastFloor.transform.rotation);
        newBox.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value, 1.0f);
    }
}
