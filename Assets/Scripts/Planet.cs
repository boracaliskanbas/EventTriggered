using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.EventSystems;

public class Planet : MonoBehaviour {

	// Use this for initialization
	public float forceMagnitude = 10;
	//events



	void Start () {




	}

	void OnTriggerEnter(Collider collider)
	{
		//Debug.Log (collider.gameObject.name + " entered the collider");
		//EventObjectEntered.Invoke();
	
		ControllerScript.Instance.objectEntered (collider.gameObject);



	

	}

	void OnTriggerStay(Collider collider)
	{
		ControllerScript.Instance.objectInside (collider.gameObject, gameObject);
	}

	void OnTriggerExit(Collider collider)
	{
		ControllerScript.Instance.objectExit (collider.gameObject);
	}

	// Update is called once per frame
	void Update () 
	{
		
	
	}



}
