using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelectorManager : MonoBehaviour {

    
    public Dictionary<string, Sprite> SpriteMapper;

	// Use this for initialization
	void Start () {

        string[] itemList = GameObject.Find("realworld").GetComponent<Generator>().stuffList;

        foreach(string key in itemList)
        {
            SpriteMapper.Add(key, null);
        }

       
    }
	
	// Update is called once per frame
	void Update () {

	}
}
