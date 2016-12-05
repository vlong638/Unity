using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonoBehaviourEventsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnMouseEnter()
    {
        PrintWithCount("OnMouseEnter",3);
    }
    private void OnMouseOver()
    {
        PrintWithCount("OnMouseOver", 3);
    }
    private void OnMouseExit()
    {
        PrintWithCount("OnMouseExit", 3);
    }
    private void OnMouseDown()
    {
        PrintWithCount("OnMouseDown", 3);
    }
    private void OnMouseUp()
    {
        PrintWithCount("OnMouseUp", 3);
    }
    private void OnTriggerEnter(Collider other)
    {
        PrintWithCount("OnTriggerEnter", 3);
    }
    private void OnTriggerExit(Collider other)
    {
        PrintWithCount("OnTriggerExit", 3);
    }
    private void OnTriggerStay(Collider other)
    {
        PrintWithCount("OnTriggerStay", 3);
    }
    private void OnCollisionEnter(Collision collision)
    {
        PrintWithCount("OnCollisionEnter", 3);
    }
    private void OnCollisionExit(Collision collision)
    {
        PrintWithCount("OnCollisionExit", 3);
    }
    private void OnCollisionStay(Collision collision)
    {
        PrintWithCount("OnCollisionStay", 3);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        PrintWithCount("OnControllerColliderHit", 3);
    }
    private void OnBecameVisible()
    {
        PrintWithCount("OnBecameVisible", 3);
    }
    private void OnBecameInvisible()
    {
        PrintWithCount("OnBecameInvisible", 3);
    }
    private void OnEnable()
    {
        PrintWithCount("OnEnable", 3);
    }
    private void OnDisable()
    {
        PrintWithCount("OnDisable", 3);
    }
    private void OnDestroy()
    {
        PrintWithCount("OnDestroy", 3);
    }
    private void OnGUI()
    {
        PrintWithCount("OnGUI", 3);
    }

    Dictionary<string, int> EventsCount = new Dictionary<string, int>();
    private void PrintWithCount(string functionName, int limit)
    {
        var value = 0;
        EventsCount.TryGetValue(functionName, out value);
        if (value < limit)
        {
            print(string.Format("{0} functioned {1} times", functionName, value));
            value += 1;
            EventsCount[functionName] = value;
        }
    }
}
