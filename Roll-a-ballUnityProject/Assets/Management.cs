using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Management : MonoBehaviour 
{
	public List<GameObject> allDoorsThisRoom;
	bool doorsClosed = true;

	public void ChangeSceneTO(string sceneName)
	{
		Application.LoadLevel (sceneName);
	}

	public void OpenDoors(GameObject thisRoom)
	{
		if (doorsClosed)
		{
			Debug.Log("Doors are Closed");
			// Get doors
			Debug.Log (thisRoom.name);
			allDoorsThisRoom.Add(thisRoom.transform.Find("Walls&Floor/WallT/Door/DoorLeft").gameObject);
			allDoorsThisRoom.Add(thisRoom.transform.Find("Walls&Floor/WallB/Door/DoorLeft").gameObject);
			allDoorsThisRoom.Add(thisRoom.transform.Find("Walls&Floor/WallL/Door/DoorLeft").gameObject);
			allDoorsThisRoom.Add(thisRoom.transform.Find("Walls&Floor/WallR/Door/DoorLeft").gameObject);
			
			allDoorsThisRoom.Add(thisRoom.transform.Find("Walls&Floor/WallT/Door/DoorRight").gameObject);
			allDoorsThisRoom.Add(thisRoom.transform.Find("Walls&Floor/WallB/Door/DoorRight").gameObject);
			allDoorsThisRoom.Add(thisRoom.transform.Find("Walls&Floor/WallL/Door/DoorRight").gameObject);
			allDoorsThisRoom.Add(thisRoom.transform.Find("Walls&Floor/WallR/Door/DoorRight").gameObject);

			// open doors
			foreach (GameObject door in allDoorsThisRoom)
			{
				Vector3 open = door.transform.position;
				open.y += 3;
				door.transform.position = open;
			}
		}
	}
}
