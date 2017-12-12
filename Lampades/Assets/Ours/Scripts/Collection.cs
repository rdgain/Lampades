using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collection : MonoBehaviour {

    public Dictionary<string,int> stuffs;
    string[] stuffList;
	// Use this for initialization
	void Start () {
        stuffs = new Dictionary<string, int>();

        GameObject parent = transform.parent.gameObject;
        stuffList = parent.GetComponent<Generator>().stuffList;

        foreach(string s in stuffList)
        {
            stuffs.Add(s, 0);
        }
        
	}

    List<GameObject> touchingObjects = new List<GameObject>();
	// Update is called once per frame
	void Update () {
        //if (Input.GetKeyDown(KeyCode.F))
        {
            //print("press f");
            for(int i=0;i<touchingObjects.Count;i++)
                PickUpStuffs(touchingObjects[i]);

        }
    }
    

    void OnCollisionEnter2D(Collision2D coll)
    {
        //print("collide!");
        //print("tag other " + (coll.otherRigidbody.gameObject.tag));
        //print("tag mine " + (coll.rigidbody.gameObject.tag));


        if(gameObject.tag.Equals("Player") 
            && coll.rigidbody.gameObject.tag.Equals("Collectibles"))
        {
            //print("collide!");
            touchingObjects.Add(coll.rigidbody.gameObject);
        }
        
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        touchingObjects.Remove(coll.rigidbody.gameObject);
    }

    void PickUpStuffs(GameObject pickUpObject)
    {
        string name_cut = getKeyFromName(pickUpObject);
        Destroy(pickUpObject);
        stuffs[name_cut] = stuffs[name_cut] + 1;
        UpdateStuffSize();
        //print("added");
        //print(stuffs[name_cut]);
    }

    public void StickedStuff(GameObject model)
    {
        string key = getKeyFromName(model);
        stuffs[key] = stuffs[key] - 1;
        UpdateStuffSize();
    }

    string getKeyFromName(GameObject model)
    {
        string name_cut = model.name.Split('(')[0];
        return name_cut;
    }

    void UpdateStuffSize()
    {
        Text stuffSizeText = GameObject.Find("StuffSize").GetComponent<Text>();

        stuffSizeText.text = "";
        foreach (string key in stuffList)
        {
            stuffSizeText.text += stuffs[key] + "\n";
        }
    }
}
