using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxConditionChecker : ConditionChecker
{
    public GameObject shieldSprite;

    public static string SHIELD_COND_KEY = "shield";
    public static string SH_RIGHT_AV = "rightOfAvatar";
    public static string SH_STICKING = "isSticking";

    // Use this for initialization
    void Start () {
        base.Start();
    }
	
	// Update is called once per frame
	void Update () {
        // so damn annoying so you can just go away after the whole of this is done
        if (finishedSticking)
            DestroyImmediate(gameObject.GetComponent<BoxConditionChecker>());
        base.Update();
	}

    public override void InitializeConditions()
    {
        conditions.Add(SHIELD_COND_KEY, Shield.initialize());
    }

    public override void UpdateConditions()
    {
        // shield
        conditions[SHIELD_COND_KEY] = Shield.updateCondition(avatar, name, transform, conditions[SHIELD_COND_KEY]);
    }

    public override void PerformPostUpdate()
    {
        if (avatar.GetComponent<JointManager>().isSticking)
            UpdateSprite();
    }

    void UpdateSprite()
    {
        GameObject newObj = SpritePicker();

        if (newObj != null)
        {
            sprite.sprite = newObj.GetComponent<SpriteRenderer>().sprite;
            sprite.color = newObj.GetComponent<SpriteRenderer>().color;
        }

        else
        {
            sprite.sprite = originalSprite;
            sprite.color = originalColor;
        }

    }

    GameObject SpritePicker()
    {
        GameObject newObject = null;

        if (Shield.satisfied(finishedSticking, conditions[SHIELD_COND_KEY]))
        {
            newObject = shieldSprite;
        }

        // add further conditions to pick a sprite

        return newObject;
    }

    public class Shield
    {
        public Shield() { }


        public static Condition initialize()
        {
            Condition ShieldCondition = new Condition();
            ShieldCondition.AddCondition(SH_RIGHT_AV);
            ShieldCondition.AddCondition(SH_STICKING);

            return ShieldCondition;
        }

        public static Condition updateCondition(GameObject avatar, string name, Transform transform, Condition condition)
        {

            bool isSticking = avatar.GetComponent<JointManager>().isSticking;
            bool rightOfAvatar = transform.position.x > avatar.transform.position.x;

            condition.setValue(SH_RIGHT_AV, rightOfAvatar);
            condition.setValue(SH_STICKING, isSticking);
            return condition;
        }

        public static bool satisfied(bool finishedSticking, Condition condition)
        {
            return !finishedSticking && condition.checkAll();
        }
    }
}
