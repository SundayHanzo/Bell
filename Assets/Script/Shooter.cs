using UnityEngine;
using System.Collections;
using System;

public class Shooter : MonoBehaviour {
    //arrow prefab
    GameObject arrow;
    public GameObject arrowPrefab;
    Vector3 arrowPosition;
 
    //shot value
    public float shotSpeed;
    public float shotTorque;
    public float baseWidth;

    //touch position
    public Vector3 downPosition;
    public Vector3 upPosition;

    
	// Use this for initialization
	void Start () {
        //create arrow object
        CreateArrow();
    }
	
	// Update is called once per frame
	void Update () {
       if (Input.GetButtonDown("Fire1")) GetTouchPos();
       if (Input.GetButtonUp("Fire1")) GetUpPos();
	}


    void CreateArrow() {
        //position
        arrowPosition.x = -0.03f;
        arrowPosition.y = -3.38f;
        arrowPosition.z = 11.0f;
        //create
        arrow = (GameObject)Instantiate(arrowPrefab, arrowPosition, Quaternion.identity);
        arrow.transform.Rotate(-90.0f, 0.0f, 0.0f);
    }

    void GetTouchPos() {
        //get down position
        float y = baseWidth * (Input.mousePosition.y / Screen.width) - (baseWidth / 2);
        downPosition = transform.position + new Vector3(0, y, 0);
        print("down pos : " + downPosition.x + " " + downPosition.y + " " + downPosition.z);

    }

    void GetUpPos() {
        //get up position
        float y = baseWidth * (Input.mousePosition.y / Screen.width) - (baseWidth / 2);
        upPosition = transform.position + new Vector3(0, y, 0);
        print("up pos : " + upPosition.x + " " + upPosition.y + " " + upPosition.z);

        Shot();
    }

    void Shot() {
        //power
        float power = downPosition.y - upPosition.y;
        power = Math.Abs(power);
        //get Rigidbody
        Rigidbody arrowRigidBody = arrow.GetComponent<Rigidbody>();
        //add force
        arrowRigidBody.AddForce(transform.forward * shotSpeed * power);
        arrowRigidBody.AddTorque(new Vector3(0, shotTorque, 0));
        //delete current arrow and create new arrow after 3second
        Invoke("ArrowDelete", 3);
    }

    void ArrowDelete() {
        //destroy current arrow
        Destroy(arrow);
        //create new arrow
        CreateArrow();
    }

    
}
