using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class LevelGenerator : MonoBehaviour {
	private List<GameObject> rooms;
	private List<float> roomCameraHeights;
	private static int gridMagnitude = 100;
	private System.Random rndGen;
	public static int roomCount = 4;
	public Room[][] roomGrid = new Room[gridMagnitude][];
	public Room initialRoom;
	public Room currentRoom;
	public Transform firePrefab;
	public Transform citizenPrefab;
	public bool changedrooms = false;
	
	
	//JOSH ADDED
	public int[][] BlackOutGrid = new int[roomCount][];
	
	//JOSH ADDED
	public bool RoomChange = false;
	
	// Use this for initialization
	void Start () {
		//JOSH ADDED			       TOP DOOR	     LEFT DOOR		     RIGHT DOOR		     BOTTOM DOOR
		BlackOutGrid[0] = new int[16] {7, 8, 23, 24, 112, 113, 128, 129, 126, 127, 142, 143, 231, 232, 247, 248};
		BlackOutGrid[1] = new int[16] {7, 8, 23, 24, 112, 113, 128, 129, 126, 127, 142, 143, 231, 232, 247, 248};
		
		rooms = new List<GameObject>();
		roomCameraHeights = new List<float>();
		for( int i = 0; i < roomCount; i++ ){
			rooms.Add(GameObject.Find("room" + (i+1).ToString() ));
			roomCameraHeights.Add(6 + (i*20) );
		}
		rndGen = new System.Random();
		for(int i = 0; i < gridMagnitude; i++){
			roomGrid[i] = new Room[gridMagnitude];
		}
		for(int i = 0; i < gridMagnitude; i++){
			for(int j = 0; j < gridMagnitude; j++){
				int index = rndGen.Next(0, roomCount);
				roomGrid[i][j] = new Room( (i*j)+j, index, roomCameraHeights[index], firePrefab, citizenPrefab );
				
			}
		}
		for(int i = 0; i < gridMagnitude; i++){
			for(int j = 0; j < gridMagnitude; j++){
				Room leftRoom = null;
				Room rightRoom = null;
				Room topRoom = null;
				Room bottomRoom = null;
				Boolean hasLeft = true;
				Boolean hasRight = true;
				Boolean hasTop = true;
				Boolean hasBottom = true;
				if( i == 0 ){ hasLeft = false; }
				else if( i == (gridMagnitude-1) ){ hasRight = false; }
				if( j == 0 ){ hasTop = false; }
				else if( j == (gridMagnitude-1) ){ hasBottom = false;; }
				if( hasLeft ){ leftRoom = roomGrid[i-1][j]; }
				if( hasRight ){ rightRoom = roomGrid[i+1][j]; }
				if( hasTop ){ topRoom = roomGrid[i][j-1]; }
				if( hasBottom ){ bottomRoom = roomGrid[i][j+1];}
				roomGrid[i][j].setupAdjacentRooms(leftRoom, rightRoom, bottomRoom, topRoom);
			}
		}
		double middleSquare = gridMagnitude/2;
		currentRoom = roomGrid[Convert.ToInt32(middleSquare)][Convert.ToInt32(middleSquare)];
		initialRoom = currentRoom;
		//spawn points for fire
		//remember where you dropped your corpse
		
	}
	
	// Update is called once per frame
	void Update () {
		//JOSH ADDED
		/*
		if(RoomChange){
			RoomChange = false;
		} */
	}
	
	public bool getChangeRoom(){
		return changedrooms;
	}
	
	public void setChangeRoom(bool input){
		changedrooms = input;
	}
	
	public class Room{
	public int number;
	public int roomType;
	public float cameraHeight;
	private Room leftRoom;
	private Room rightRoom;
	private Room topRoom;
	private Room bottomRoom;
	public bool ignoreLeftDoor = false;
	public bool ignoreRightDoor = false;
	public bool ignoreTopDoor = false;
	public bool ignoreBottomDoor = false;
	public int[][] spawnData;// 2-d array of ints: 0=Nothing, 1=Fire, 2=Citizen, 3=Corpse
	public int gridDivisions = 16;
	public float unitSize = 0.5f;
	public float somethingProbability = 0.2f;
	public float fireProbability = 0.40f;
	public float citizenProbability = 0.05f;
	public float corpseProbability = 0.02f;
	public Transform firePrefab;
	public Transform citizenPrefab;
	private List<UnityEngine.GameObject> roomObjects = new List<UnityEngine.GameObject>();
	public List<GameObject> corpses = new List<GameObject>();
		
	public Room(int num, int roomType, float camH, Transform fire, Transform citizen){
		number = num;
		roomType = roomType;
		cameraHeight = camH;
		firePrefab = fire;
		citizenPrefab = citizen;
		spawnData = new int[gridDivisions][];
		for( int i = 0; i < gridDivisions; i++){
			spawnData[i] = new int[gridDivisions];
		}
		generateSpawnData();
	}
	
	public void generateSpawnData(){
			//JOSH ADDED
			//JOSH ADDED			   TOP DOOR	     LEFT DOOR		             RIGHT DOOR		     BOTTOM DOOR
			int[] bgrid = new int[20] {7, 8, 23, 24, 80, 96, 112, 113, 128, 129, 126, 127, 142, 143, 230, 231, 232, 247, 248, 249};
			bool skip = false;
			for(int i=0; i < gridDivisions; i++){
			for(int j=0; j < gridDivisions; j++){
				spawnData[i][j] = 0;
				//JOSH ADDED
				for(int lcv = 0; lcv < bgrid.Length; lcv++){
					if(bgrid[lcv] == (j*16+i)){
						skip = true;
						lcv = bgrid.Length;
					}
				}
				//JOSH ADDED
				if(!skip){
					if( UnityEngine.Random.value < somethingProbability ){ // there is something in this space
						if(UnityEngine.Random.value < fireProbability){//1
							spawnData[i][j] = 1;
						}
						else if(UnityEngine.Random.value < citizenProbability){//2
							spawnData[i][j] = 2;
							switch( Convert.ToInt16(UnityEngine.Random.value * 4) ){
								case 0: 
									if(j < (gridDivisions-4) ){ for(int k=0; k < 3; k++){ spawnData[i][j+k] = 2; } }
									else{spawnData[i][j] = 0;}
									break;
								case 1: if( j > 3 ){ for(int k=0; k < 3; k++){ spawnData[i][j-k] = 2; } }
									else{spawnData[i][j] = 0;}
									break;
								case 2: if( i < (gridDivisions-4) ){ for(int k=0; k < 3; k++){ spawnData[i+k][j] = 2;} }
									else{spawnData[i][j] = 0;}
									break;
								case 3: if( i > 3 ){ for(int k=0; k < 3; k++){ spawnData[i-k][j] = 2; } }
									else{spawnData[i][j] = 0;}
									break;
								default: 
									break;
							}
						}
					}
				}
				//JOSH ADDED
				skip = false;
			}
		}
	}
	
	public void setupAdjacentRooms( Room left, Room right, Room bottom, Room top){
		leftRoom = left;
		rightRoom = right;
		topRoom = top;
		bottomRoom = bottom;
	}
	
	public void spawnStuff(){
			GameObject temp;
		Quaternion rot = new Quaternion(0,0,0, 0);
		for(int i = 0; i < gridDivisions; i++){
			for(int j = 0; j < gridDivisions; j++){
				
				if(spawnData[i][j] == 1){
					
					Vector3 position = new Vector3(-4.0f + (unitSize*i), cameraHeight-5.9f, -4.0f + (unitSize*j) );
					//JOSH ADDED
						/*
						temp = Instantiate(firePrefab, position, Quaternion.identity) as GameObject;
						if(temp == null)
							Debug.Log("NULL Instance");
						else
							Debug.Log(temp.ToString());
						temp.GetComponent<Fire_Properties>().Fire_ID_i = i;
						temp.GetComponent<Fire_Properties>().Fire_ID_j = j;
					*/
						roomObjects.Add( Instantiate(firePrefab, position, Quaternion.identity) as GameObject );
				}
				if(spawnData[i][j] == 2){
					switch( Convert.ToInt32(UnityEngine.Random.value * 4) ){
							case 0: rot = Quaternion.Euler(0,90,0);
								break;
							case 1: rot = Quaternion.Euler(0,180,0);
								break;
							case 2: rot = Quaternion.Euler(0,270,0);
								break;
							case 3: rot = Quaternion.Euler(0,360,0);
								break;
							default: 
								break;
						}
					Vector3 position = new Vector3(-4.0f + (unitSize*i), cameraHeight-5.9f, -4.0f + (unitSize*j) );
					/*
						Instantiate(
					citizenPrefab, 
					position,
					rot );
					*/
						roomObjects.Add( Instantiate(citizenPrefab, position, rot ) as GameObject);
				}
			}
		}
	}
	
	public void moveCameraAndPlayer(String enterDoor){
		GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(-5, cameraHeight, -5);
		if(enterDoor == "Top"){ 
			GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, (cameraHeight-5.9f), 3); 
		}
		else if(enterDoor == "Bottom"){ 
			GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, (cameraHeight-5.9f), -3); 
		}
		else if(enterDoor == "Left"){ 
			GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(-3, (cameraHeight-5.9f), 0); 
		}
		else if(enterDoor == "Right"){ 
			GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(3, (cameraHeight-5.9f), 0); 
		}
		spawnStuff();
	}
	
	public int getRoomNumber(){ return number; }
	
		public void KillSpawns(){
			GameObject[] FireCounter = GameObject.FindGameObjectsWithTag("Fire");
		int temp = 0;
		foreach(GameObject Fire in FireCounter){
			Destroy(FireCounter[temp]);
				        temp++;
		}
			
			GameObject[] PersonCounter = GameObject.FindGameObjectsWithTag("Person");
		int temp2 = 0;
		foreach(GameObject Female in PersonCounter){
			Destroy(PersonCounter[temp2]);
				        temp2++;
		}
			
			/*
		for(int i = 0; i < roomObjects.Count; i++){
			Destroy( roomObjects[i] );
		}
		roomObjects.Clear();*/
	}
		
	public Room getNextRoom( String hitDoor ){
		Room temp = this;
		String enter = "";
		if( hitDoor == "TopDoor" && topRoom != null && !ignoreTopDoor){ enter = "Bottom"; temp = topRoom;}// temp.ignoreBottomDoor = true; }
		else if( hitDoor == "BottomDoor" && bottomRoom != null && !ignoreBottomDoor){ enter = "Top"; temp = bottomRoom;}// temp.ignoreTopDoor = true; }
		else if( hitDoor == "LeftDoor" && leftRoom != null && !ignoreLeftDoor){ enter = "Right"; temp = leftRoom;}// temp.ignoreRightDoor = true; }
		else if( hitDoor == "RightDoor" && rightRoom != null && !ignoreRightDoor){ enter = "Left"; temp = rightRoom;}// temp.ignoreLeftDoor = true; }
		KillSpawns();
			temp.moveCameraAndPlayer(enter);
		
			//LevelGenerator
			
		return temp;
	}
}
}
