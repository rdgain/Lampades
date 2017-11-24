using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointManager : MonoBehaviour {

    public GameObject boxModel;
    public GameObject stickModel;

    public bool insideLight = false;

    public float moveSpeed = 0.5f;
    public float rotateSpeed = 0.2f;

    float keptMoveForce;

    public CircleCollider2D centerCollider;

    public bool isSticking = false;
    bool isTouching = true;
    GameObject stickingObject;
    Vector3 storePrevPos;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        
        if (!isSticking)
        {
            if (Input.GetKeyDown(KeyCode.B) && insideLight)
            {
                //Need to decrease stuff num in collection down
                //Create sticking option menu
                Stick("Stick");
            }
        }

        else
        {
            PreSticking();
        }

        //print("focusJoint connect with " + focusJoint.connectedBody.name);
	}

    void PreSticking()
    {
        if (Input.GetKeyDown("enter") || Input.GetKeyDown("return"))
        {
            if (gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                CreateJointAndCollider();
                ProjectToShadow();
            }
            GetComponent<PlayerControl>().canMove = true;
            GetComponent<PlayerControl>().moveForce = keptMoveForce;
        }
        if (gameObject.layer != LayerMask.NameToLayer("Player"))
            return;

        //move right
        if (Input.GetKey(KeyCode.D))
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

        else if (Input.GetKey(KeyCode.R))
        {
            stickingObject.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(3, 0));
        }

        
    }

    void ProjectToShadow()
    {

    }

    // check if it's still sticking
    bool AllowTransform()
    {
        //print(stickingObject.GetComponent<Collider2D>().bounds+" "+ GetComponent<Collider2D>().bounds);
        //return isTouching;

        bool touching = stickingObject.GetComponent<Collider2D>().IsTouching(centerCollider);

        return touching;
    }

    void Move(Vector3 trans)
    {
        
        stickingObject.transform.position += trans * moveSpeed;
        

        //print(canDo +" "+storePrevPos+" "+stickingObject.transform.position);

        if(!AllowTransform())
        {
            //print("restored");

            Vector3 MoveBackDir = transform.position - stickingObject.transform.position;

            stickingObject.transform.position += MoveBackDir.normalized * moveSpeed;
            //print(stickingObject.transform.position + " " + storePrevPos);
            
        }
    }

    void Rotate(Vector3 dir)
    {
        Quaternion storeRotation = transform.rotation;
        stickingObject.transform.Rotate(dir * rotateSpeed);

        if(!AllowTransform())
        {
            Vector3 MoveBackDir = transform.position - stickingObject.transform.position;

            stickingObject.transform.position += MoveBackDir.normalized * moveSpeed;
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

        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {

        GameObject newStuff = Instantiate(toStick, transform.position, transform.rotation);
        //newStuff.layer = LayerMask.NameToLayer("Sticking");
        newStuff.GetComponent<Collider2D>().isTrigger = true;
        newStuff.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        newStuff.GetComponent<CollectiblesManager>().rotating = false;
        newStuff.GetComponent<SpriteRenderer>().color = new Color(fromRGB(247), fromRGB(151), fromRGB(231), 1);

        stickingObject = newStuff;
    }
        isSticking = true;
        GetComponent<PlayerControl>().canMove = false;
        keptMoveForce = GetComponent<PlayerControl>().moveForce;
        GetComponent<PlayerControl>().moveForce = 0;
    }

    float fromRGB(int rgb)
    {
        return (float)rgb / 255f;
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
         * create joint & bring collider back
         * */

        stickingObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        stickingObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
        joint.connectedBody = stickingObject.GetComponent<Rigidbody2D>();

        stickingObject.transform.parent = transform;

        isSticking = false;
        stickingObject = null;
    }
}
