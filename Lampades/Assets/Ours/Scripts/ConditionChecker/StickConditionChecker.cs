using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class StickConditionChecker : ConditionChecker
    {

        public GameObject swordSprite;

        public static string SWORD_COND_KEY = "sword";
        public static string SW_LEFT_AV = "leftOfAvatar";
        public static string SW_STICKING = "isSticking";



        // Use this for initialization
        void Start()
        {
            base.Start();
            finishedSticking = false;

        }
        // Update is called once per frame
        void Update()
        {
            // so damn annoying so you can just go away after the whole of this is done
            if (finishedSticking)
                DestroyImmediate(gameObject.GetComponent<StickConditionChecker>());
            
            base.Update();
        }

        public override void InitializeConditions()
        {
            conditions.Add(SWORD_COND_KEY, Sword.initialize());
        }

        public override void UpdateConditions()
        {
            // sword
            conditions[SWORD_COND_KEY] = Sword.updateCondition(avatar, name, transform, conditions[SWORD_COND_KEY]);
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

            if (Sword.satisfied(finishedSticking, conditions[SWORD_COND_KEY]))
            {
                newObject = swordSprite;
            }

            // add further conditions to pick a sprite

            return newObject;
        }

    public class Sword
    {
        public Sword() { }


        public static Condition initialize()
        {
            Condition SwordCondition = new Condition();
            SwordCondition.AddCondition(SW_LEFT_AV);
            SwordCondition.AddCondition(SW_STICKING);

            return SwordCondition;
        }

        public static Condition updateCondition(GameObject avatar, string name, Transform transform, Condition condition)
        {

            // print(transform.position.x + " " + avatar.transform.position.x);

            bool isSticking = avatar.GetComponent<JointManager>().isSticking;
            bool leftOfAvatar = transform.position.x < avatar.transform.position.x;

            condition.setValue(SW_LEFT_AV, leftOfAvatar);
            condition.setValue(SW_STICKING, isSticking);
            return condition;
        }

        public static bool satisfied(bool finishedSticking, Condition condition)
        {
            return !finishedSticking && condition.checkAll();
        }
    }

}

   