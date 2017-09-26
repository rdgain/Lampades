using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collection : MonoBehaviour {

    public Dictionary<string,int> stuffs;
    public string[] stuffList;
	// Use this for initialization
	void Start () {
        stuffs = new Dictionary<string, int>();

        foreach(string s in stuffList)
        {
            stuffs.Add(s, 0);
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    

    void OnCollisionEnter2D(Collision2D coll)
    {
        //print("collide!");
        //print("tag other " + (coll.otherRigidbody.gameObject.tag));
        //print("tag mine " + (coll.rigidbody.gameObject.tag));


        if(gameObject.tag.Equals("Player") && coll.rigidbody.gameObject.tag.Equals("Collectibles"))
        {
            
            string name_cut = coll.rigidbody.gameObject.name.Split(' ')[0];
            Destroy(coll.rigidbody.gameObject);
            stuffs[name_cut] = stuffs[name_cut] + 1;
         
            print("added");

            print(stuffs[name_cut]);
        }

        Text stuffSizeText = GameObject.Find("StuffSize").GetComponent<Text>();

        stuffSizeText.text = "";
        foreach (string key in stuffList)
        {
             stuffSizeText.text += stuffs[key] + "\n";
        }
    }
    }
