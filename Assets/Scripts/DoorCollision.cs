using UnityEngine;
using System.Collections;

public class DoorCollision : MonoBehaviour {
	private LevelGenerator levelMap;
	public bool ignoreLeftDoor = false;
	public bool ignoreRightDoor = false;
	public bool ignoreTopDoor = false;
	public bool ignoreBottomDoor = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter (Collider whichDoor){
		levelMap = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<LevelGenerator>();
		
		levelMap.currentRoom = levelMap.currentRoom.getNextRoom(this.tag);
		
		
		//Josh Added
		GameObject[] LevelRef = GameObject.FindGameObjectsWithTag("LevelGenerator");
		
		LevelGenerator Rooming = LevelRef[0].gameObject.GetComponent<LevelGenerator>();
		
		
		Rooming.RoomChange = true;
		
		StartCoroutine("RoomChangeCounter");
		//Debug.Log("Room Change " + Rooming.RoomChange);
	}
	
	IEnumerator RoomChangeCounter(){
		yield return new WaitForSeconds(1.0f);
		GameObject[] LevelRef = GameObject.FindGameObjectsWithTag("LevelGenerator");
		
		LevelGenerator Rooming = LevelRef[0].gameObject.GetComponent<LevelGenerator>();
		
		
		Rooming.RoomChange = false;
	}
}
