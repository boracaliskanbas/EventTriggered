using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Player : MonoBehaviour {

	public Vector3 initialForce = new Vector3(0, 0, 10);
	//events
	public delegate void ProcessPlayerDelegate(Player player);

	//player rotation when on orbit
	public bool clockwise = false;

	//Is player orbiting around the planet or not
	public bool canOrbit = false;




	// Use this for initialization
	void Start () {

		GetComponent<Rigidbody> ().AddForce (initialForce, ForceMode.Acceleration);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
