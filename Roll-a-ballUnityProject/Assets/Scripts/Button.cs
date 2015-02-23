using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Button : MonoBehaviour 
{
	GameObject manager;

	public List<GameObject> allDoorsThisRoom;
	GameObject thisRoom;
	bool doorsOpen;

	// Use this for initialization
	void Start () 
	{
		manager = GameObject.Find ("EventSystem");
		thisRoom = this.transform.parent.parent.gameObject;
	}

	void OnCollisionEnter(Collision other)
	{
		switch (gameObject.transform.name)
		{
			case "Doors":
				manager.GetComponent<Management>().OpenDoors(thisRoom);
				break;
			default:
				manager.GetComponent<Management>().ChangeSceneTO(gameObject.transform.name);
				break;
		}

//		if(!doorsOpen)
//		{
//			Debug.Log ("button pressed");
//			GetDoors ();
//			OpenDoors ();
//			doorsOpen = true;
//		}
	}

//	void GetDoors()
//	{
//		Debug.Log (thisRoom.name);
//		allDoorsThisRoom.Add(thisRoom.transform.Find("Walls&Floor/WallT/Door/DoorLeft").gameObject);
//		allDoorsThisRoom.Add(thisRoom.transform.Find("Walls&Floor/WallB/Door/DoorLeft").gameObject);
//		allDoorsThisRoom.Add(thisRoom.transform.Find("Walls&Floor/WallL/Door/DoorLeft").gameObject);
//		allDoorsThisRoom.Add(thisRoom.transform.Find("Walls&Floor/WallR/Door/DoorLeft").gameObject);
//
//		allDoorsThisRoom.Add(thisRoom.transform.Find("Walls&Floor/WallT/Door/DoorRight").gameObject);
//		allDoorsThisRoom.Add(thisRoom.transform.Find("Walls&Floor/WallB/Door/DoorRight").gameObject);
//		allDoorsThisRoom.Add(thisRoom.transform.Find("Walls&Floor/WallL/Door/DoorRight").gameObject);
//		allDoorsThisRoom.Add(thisRoom.transform.Find("Walls&Floor/WallR/Door/DoorRight").gameObject);
//	}
//
//	void OpenDoors()
//	{
//		foreach (GameObject door in allDoorsThisRoom)
//		{
//			Vector3 open = door.transform.position;
//			open.y += 3;
//			door.transform.position += open;
//		}
//	}
}
