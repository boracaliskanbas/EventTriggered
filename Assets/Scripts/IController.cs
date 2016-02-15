using System;
using UnityEngine;
using UnityEngine.EventSystems;


public interface IController : IEventSystemHandler
{

	void EventObjectEntered (GameObject gameobject);
	void EventObjectInside (GameObject gameobject);
	void EventObjectExit (GameObject gameobject);

}


