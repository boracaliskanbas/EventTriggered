using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;




public class ControllerScript : MonoBehaviour{

	//Entrance Velocity of the player to a sphere
	public Vector3 BaseVel = new Vector3(0,0,0);

	//magnitude of velocity of the player
	public float speed = 0.0f;

	//Is Input taken from the console
	public bool bInput = false;

	//main camera for the screen
	public static Camera mainCamera;

	public Vector3 VelIn = new Vector3(0,0,0);

	private static ControllerScript itsStaticThis;

	private  static ControllerScript instance;  	

	// Construct 	
	private ControllerScript() {}  	

	//  Instance 	
	public static ControllerScript Instance { 		
		get { 			
			if (instance == null)
				instance = GameObject.FindObjectOfType (typeof(ControllerScript)) as  ControllerScript; 			
			return instance; 		
		}  	

	}

	// Use this for initialization
	void Start () {

		mainCamera = Camera.main;




	
	}
	
	// Update is called once per frame
	void Update () {



	}

	// Physics related stuff here !
	void FixedUpdate () {


	}





	public void objectEntered (GameObject gameobject)
	{
		Debug.Log ("EventObjectEntered");

		BaseVel = gameobject.GetComponent<Rigidbody> ().velocity;

		var posVector = (transform.position - gameobject.transform.position);  //collider.transform.position);
		var angle = Vector3.Angle (posVector, gameobject.GetComponent<Rigidbody>().velocity);  //collider.GetComponent<Rigidbody> ().velocity);

		Debug.Log ("angle val " + angle);

		Debug.Log (Vector3.Cross (posVector, gameobject.GetComponent<Rigidbody> ().velocity));  //collider.GetComponent<Rigidbody> ().velocity));

		if (Vector3.Cross (posVector, gameobject.GetComponent<Rigidbody> ().velocity).y > 0) 
			gameobject.GetComponent<Player> ().clockwise = true;
		else
			gameobject.GetComponent<Player> ().clockwise = false;

		//Debug.Log ("Player entered with an angle of " + angle + " " + " " +canOrbit);
	}

	public void objectInside (GameObject gameobject, GameObject planetobject)
	{
		Debug.Log ("EventObjectInside");	

		if (!gameobject.GetComponent<Player> ().canOrbit) {

			float multiplier = planetobject.GetComponent<SphereCollider> ().radius * planetobject.transform.localScale.x / (Vector3.Distance(gameobject.GetComponent<Rigidbody>().position, planetobject.transform.position));
			VelIn = BaseVel + (BaseVel * (multiplier - 1)*  10.0f); 
			float speedx = (VelIn.x) * (VelIn.x);
			float speedz = (VelIn.z) * (VelIn.z);

			speed = Mathf.Sqrt (speedx + speedz);
		}


		//Debug.Log (rotateAngle * Time.fixedDeltaTime);
		var posVector = (planetobject.transform.position - gameobject.transform.position);
		var angle = Vector3.Angle (posVector, gameobject.GetComponent<Rigidbody> ().velocity);

		if (Mathf.Abs (angle) >= 70.0f && !bInput) {

			gameobject.GetComponent<Player> ().canOrbit = true;



			gameobject.GetComponent<Rigidbody> ().velocity = Vector3.zero;

		}

		if((Input.touchCount > 0) || (Input.GetKey (KeyCode.DownArrow)) ){

			var normAngle = (planetobject.transform.position - gameobject.transform.position).normalized;

			var tempAngle = normAngle.x;

			if(gameobject.GetComponent<Player> ().clockwise)
				normAngle.x = -normAngle.z;
			else
				normAngle.x = normAngle.z;



			if(gameobject.GetComponent<Player> ().clockwise)
				normAngle.z = tempAngle;
			else
				normAngle.z = -tempAngle;

			gameobject.GetComponent<Rigidbody> ().velocity = normAngle * speed;
			bInput = true;
			return;

		}



		if (!bInput) {
			if (gameobject.GetComponent<Player> ().canOrbit) {



				Debug.Log ("scale = " + this.transform.localScale.x);

				if (gameobject.GetComponent<Player> ().clockwise) {
					gameobject.transform.RotateAround (planetobject.transform.position, Vector3.up, Time.fixedDeltaTime * 360 * speed / (2 * Mathf.PI * (Vector3.Distance(gameobject.GetComponent<Rigidbody>().position,planetobject.transform.position))));//GetComponent<SphereCollider> ().radius * this.transform.localScale.x));
				}  else
					gameobject.transform.RotateAround (planetobject.transform.position, Vector3.down, Time.fixedDeltaTime * 360 * speed / (2 * Mathf.PI * (Vector3.Distance(gameobject.GetComponent<Rigidbody>().position,planetobject.transform.position))));//GetComponent<SphereCollider> ().radius * this.transform.localScale.x));



			}  else
				gameobject.GetComponent<Rigidbody> ().velocity = VelIn;

			//vel = gameobject.GetComponent<Rigidbody> ().velocity;

		}



	}

	public void objectExit (GameObject gameobject)
	{
		Debug.Log ("EventObjectExit");	

		//Debug.Log ("Player is out of sphere collider");

		gameobject.GetComponent<Player> ().canOrbit = false;
		gameobject.GetComponent<Player> ().clockwise = false;

		bInput = false;


	}


}
