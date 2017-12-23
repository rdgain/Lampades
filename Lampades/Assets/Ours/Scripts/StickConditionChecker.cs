using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickConditionChecker : MonoBehaviour {

    public GameObject swordSprite;
    Sprite originalSprite;
    Color originalColor;
    SpriteRenderer sprite;

    public bool swordCondition;
    public bool finishedSticking;

	// Use this for initialization
	void Start () {
        finishedSticking = false;
        sprite = gameObject.GetComponent<SpriteRenderer>();
        originalSprite = sprite.sprite;
        originalColor = sprite.color;
    }
	
	// Update is called once per frame
	void Update () {

   //     print(SwordCondition());
        
        if(!finishedSticking)
        if(SwordCondition())
        {
            swordCondition = true;
            sprite.sprite = swordSprite.GetComponent<SpriteRenderer>().sprite;
            sprite.color = swordSprite.GetComponent<SpriteRenderer>().color;
        }

        else
        {
            swordCondition = false;
            sprite.sprite = originalSprite;
            sprite.color = originalColor;
        }
	}



    bool SwordCondition()
    {

        GameObject avatar;

        if (name.Contains("hadow"))
            avatar = GameObject.Find("shadow");
        else
            avatar = GameObject.Find("avatar");
       // print(transform.position.x + " " + avatar.transform.position.x);

        if (
            avatar.GetComponent<JointManager>().isSticking 
            &&
            //TODO change this to the condition you wish
            transform.position.x < avatar.transform.position.x
            )
            return true;

        else return false;
    }
}
