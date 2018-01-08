using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collection : MonoBehaviour {

    public Dictionary<string,int> stuffs;
    public AudioClip collectedClip;
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
        {
            for(int i=0;i<touchingObjects.Count;i++)
                PickUpStuffs(touchingObjects[i]);

        }
    }
    

    void OnCollisionEnter2D(Collision2D coll)
    {

        if(gameObject.tag.Equals(Constants.PLAYER_TAG) 
            && coll.rigidbody.gameObject.tag.Equals(Constants.COLLECT_TAG))
        {
            touchingObjects.Add(coll.rigidbody.gameObject);
        }
        
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        touchingObjects.Remove(coll.rigidbody.gameObject);
    }

    void PickUpStuffs(GameObject pickUpObject)
    {
        if(collectedClip !=null)
            AudioSource.PlayClipAtPoint(collectedClip, transform.position);

        string name_cut = getKeyFromName(pickUpObject);
        Destroy(pickUpObject);
        stuffs[name_cut] = stuffs[name_cut] + 1;
        UpdateStuffSize();
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
        Text stuffSizeText = GameObject.Find(Constants.STUFF_SIZE_TEXTBOX).GetComponent<Text>();

        stuffSizeText.text = "";
        foreach (string key in stuffList)
        {
            stuffSizeText.text += stuffs[key] + "\n";
        }
    }
}
