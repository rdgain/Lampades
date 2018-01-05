using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionChecker : MonoBehaviour {

    protected Sprite originalSprite;
    protected Color originalColor;
    protected Collider2D originalCollider;

    protected SpriteRenderer sprite;

    protected GameObject avatar;
    public bool finishedSticking;

    protected Dictionary<string, Condition> conditions;
	// Use this for initialization
	protected void Start () {

        finishedSticking = false;

        if (name.Contains("hadow"))
            avatar = GameObject.Find("shadow");
        else
            avatar = GameObject.Find("avatar");


        sprite = gameObject.GetComponent<SpriteRenderer>();
        originalSprite = sprite.sprite;
        originalColor = sprite.color;
        originalCollider = gameObject.GetComponent<Collider2D>();

        conditions = new Dictionary<string, Condition>();
        InitializeConditions();

	}

    // Update is called once per frame
    protected void Update () {

        if(!finishedSticking)
            UpdateConditions();
            PerformPostUpdate();
	}

    public virtual void PerformPostUpdate()
    {}


    public virtual void InitializeConditions()
    {}

    public virtual void UpdateConditions()
    {}

}

public class Condition
{
    Dictionary<string, bool> conditions;

    public Condition()
    {
        conditions = new Dictionary<string, bool>();
    }

    public Condition(string[] keys)
    {
        conditions = new Dictionary<string, bool>();

        foreach(string key in keys)
        {
            conditions.Add(key, false);
        }
    }

    public void setValue(string key, bool value)
    {
        conditions[key] = value;
    }

    public bool checkAll()
    {
        bool checker = true;

        foreach(string key in conditions.Keys)
        {
            checker = checker & conditions[key];
        }

        return checker;
    }

    public bool checkSomeFromKey(string[] keys)
    {
        bool checker = true;

        foreach (string key in keys)
        {
            if (conditions.ContainsKey(key))
                checker = checker & conditions[key];
        }

        return checker;
    }

    public void AddCondition(string key)
    {
        if(!conditions.ContainsKey(key))
            conditions.Add(key,false);
    }

    public void RemoveCondition(string key)
    {
        if (conditions.ContainsKey(key))
            conditions.Remove(key);
    }

}
