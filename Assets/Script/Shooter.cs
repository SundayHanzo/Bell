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
    float power;

    //touch position
    public Vector3 downPosition;
    public Vector3 upPosition;
    public Vector3 nowPosition;
    Touch touch;
    bool touched;

    //gage
    float gage;

    // Use this for initialization
    void Start () {
        //create arrow object
        CreateArrow();

        //init power 0
        downPosition.y = 0;
        upPosition.y = 0;
        power = 0;
        gage = 0;
        //is touched
        touched = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1")) GetTouchPos();
        if (Input.GetButtonUp("Fire1")) GetUpPos();
        if (touched == true) {
            //get moved size
            float y = baseWidth * (Input.mousePosition.y / Screen.width) - (baseWidth / 2);
            nowPosition = transform.position + new Vector3(0, y, 0);
            gage = downPosition.y - nowPosition.y;
            
            //scale gage size
            gage = gage/100;
            print("gage : " + gage);
            if (arrow.transform.position.z > 8)
            {
                arrow.transform.Translate(Vector3.up * gage);
            }
                


            print("arrow pos z : " + arrow.transform.position.z);
        }

        if (Input.touchCount == 1) {
            touch = Input.GetTouch(0);
            //touch start
            if (touch.phase == TouchPhase.Began) {
                GetTouchPos();
            }//touch drag
            if (touch.phase == TouchPhase.Moved) {

            }//touch end
            if (touch.phase == TouchPhase.Ended) {
                GetUpPos();
            }
        }

    }

    public float GetGage() {
        return gage;
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
        //is touched
        touched = true;
        //get down position
        float y = baseWidth * (Input.mousePosition.y / Screen.width) - (baseWidth / 2);
        downPosition = transform.position + new Vector3(0, y, 0);
        //downPosition = touch.position - touch.deltaPosition;
        print("GETTOUCHPOS down pos : " + downPosition.x + " " + downPosition.y + " " + downPosition.z);

    }

    void GetUpPos() {
        //is touched
        touched = false;
        //get up position
        float y = baseWidth * (Input.mousePosition.y / Screen.width) - (baseWidth / 2);
        upPosition = transform.position + new Vector3(0, y, 0);
        //upPosition = touch.position - touch.deltaPosition;
        print("up pos : " + upPosition.x + " " + upPosition.y + " " + upPosition.z);

        Shot();
    }

    void GetPower() {
        //power
        power = downPosition.y - upPosition.y;
        power = Math.Abs(power);
    }

    void Shot() {
        GetPower();
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
