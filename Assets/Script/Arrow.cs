using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        print("trigger");
        switch (other.gameObject.tag)
        {
            case "Target":
                print("target");
                Destroy(other.gameObject);
                break;
        }
    }
}
