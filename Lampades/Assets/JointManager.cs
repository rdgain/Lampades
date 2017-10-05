using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointManager : MonoBehaviour {

    public GameObject boxModel;
    public GameObject stickModel;

    public float moveSpeed = 0.2f;
    public float rotateSpeed = 0.2f;

    bool isSticking = false;
    bool isTouching = true;
    GameObject stickingObject;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (!isSticking)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                //Need to decrease stuff num in collection down
                //Need to turn avatar to immovable mode
                Stick("Stick");
            }
        }

        else
        {
            //move right
            if(Input.GetKey(KeyCode.D))
            {
                Move(Vector3.right);
            }

            //move left
            else if (Input.GetKey(KeyCode.A))
            {
                Move(Vector3.left);
            }

            //move up
            else if (Input.GetKey(KeyCode.W))
            {
                Move(Vector3.up);
            }

            //move down
            else if (Input.GetKey(KeyCode.S))
            {
                Move(Vector3.down);
            }

            //rotate cw
            else if (Input.GetKey(KeyCode.E))
            {
                Rotate(Vector3.back);
            }

            //rotate ccw
            else if (Input.GetKey(KeyCode.Q))
            {
                Rotate(Vector3.forward);
            }

            else if(Input.GetKeyDown("enter") || Input.GetKeyDown("return"))
            {
                CreateJointAndCollider();
            }

        }

        //print("focusJoint connect with " + focusJoint.connectedBody.name);
	}

    // check if it's still sticking
    bool AllowTransform()
    {
        //print(stickingObject.GetComponent<Collider2D>().bounds+" "+ GetComponent<Collider2D>().bounds);
        //return isTouching;

        return stickingObject.GetComponent<Collider2D>().bounds.Intersects(GetComponent<Collider2D>().bounds);
    }

    void Move(Vector3 trans)
    {
        Vector3 storePrevPos = stickingObject.transform.position;
        stickingObject.transform.position += trans * moveSpeed;

        if(!AllowTransform())
        {
            stickingObject.transform.position = storePrevPos;
        }
    }

    void Rotate(Vector3 dir)
    {
        Quaternion storeRotation = transform.rotation;
        stickingObject.transform.Rotate(dir * rotateSpeed);

        if(!AllowTransform())
        {
            stickingObject.transform.rotation = storeRotation;
        }
    }

    public void Stick(string toStickType)
    {

        GameObject toStick;

        if (toStickType.Equals("Box"))
            toStick = boxModel;

        else if (toStickType.Equals("Stick"))
            toStick = stickModel;

        else toStick = null;
        
        GameObject newStuff = Instantiate(toStick, transform.position, transform.rotation);
        //newStuff.layer = LayerMask.NameToLayer("Sticking");
        newStuff.GetComponent<Collider2D>().isTrigger = true;
        newStuff.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        

        stickingObject = newStuff;
        
        isSticking = true;
    }

    //void OnTriggerEnter(Collider coll)
    //{
    //    print("touching " + coll.gameObject.name);
    //    if(coll.gameObject.Equals(stickingObject))
    //    {
    //        print("touching");
    //        isTouching = true;
    //    }
    //}

    //void OnTriggerExit(Collider coll)
    //{
    //    if(coll.gameObject.Equals(stickingObject))
    //    {
    //        print("no touching");
    //        isTouching = false;
    //    }
    //}

    void CreateJointAndCollider()
    {
        /*
         * TODO TODO TODO create joint & bring collider back
         * */


        isSticking = false;
        stickingObject = null;
    }
}
