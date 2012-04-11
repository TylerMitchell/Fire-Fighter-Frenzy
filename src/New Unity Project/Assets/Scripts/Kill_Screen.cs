using UnityEngine;
using System.Collections;

public class Kill_Screen : MonoBehaviour {
	
	public bool GameOver;
	
	public int HighScore;
	
	private bool Select = true;
	
	private bool NewHighScore = false;
	
	private bool GUION = false;
	
	public bool lockinput = false;
	
	public bool lockco = false;
	
	public int PlayerScore;
	
	public Texture ContinueSelected;
	public Texture ContinueUnselected;
	public Texture ExitSelected;
	public Texture ExitUnselected;
	
	public Transform Fireman;
	
	void OnGUI()
	{
		if(GUION){
			if(NewHighScore){
				GUI.Label(new Rect(Screen.width*(27.0f/64.0f), Screen.height/4, Screen.width/4, Screen.height/8), "NEW HIGH SCORE!");
				GUI.Label(new Rect(Screen.width*(15.0f/32.0f), Screen.height/4+Screen.height/8, Screen.width/8, Screen.height/8), HighScore.ToString());
			}
			else{
				GUI.Label(new Rect(Screen.width*(7.0f/16.0f), Screen.height/4, Screen.width/8, Screen.height/8), "High Score " + HighScore.ToString());
				GUI.Label(new Rect(Screen.width*(7.0f/16.0f), Screen.height/4+Screen.height/8, Screen.width/8, Screen.height/8), "Your Score " + PlayerScore.ToString());
			}
			
			if(Select){
				GUI.Label(new Rect(Screen.width*(7.0f/16.0f), Screen.height/2, Screen.width/8, Screen.height/8), ContinueSelected);
				GUI.Label(new Rect(Screen.width*(7.0f/16.0f), Screen.height/2+Screen.height/8, Screen.width/8, Screen.height/8), ExitUnselected);
			}
			else{
				GUI.Label(new Rect(Screen.width*(7.0f/16.0f), Screen.height/2, Screen.width/8, Screen.height/8), ContinueUnselected);
				GUI.Label(new Rect(Screen.width*(7.0f/16.0f), Screen.height/2+Screen.height/8, Screen.width/8, Screen.height/8), ExitSelected);
			}
			
		}
	}
	
	// Use this for initialization
	void Start () {
		HighScore = 0;
		
		GameOver = false;	
	}
	
	// Update is called once per frame
	void Update () {
		if(GameOver)
		{
			
			//NEED TO "PAUSE" THE GAME
			
			GameObject[] GUIRef = GameObject.FindGameObjectsWithTag("MainCamera");
			
			Gameplay_GUI ScoreRef = GUIRef[0].gameObject.GetComponent<Gameplay_GUI>();
		
			PlayerScore = ScoreRef.g_score;
			
			if(!GUION){	
				if(ScoreRef.g_score > HighScore){ 
					NewHighScore = true;
					HighScore = ScoreRef.g_score;
				}
				else{
					NewHighScore = false;
				}
			}
			
			GUION = true;
			
			if(!lockco){
				StartCoroutine("PlayDeath");
				lockco = true;
			}
			
			if(!lockinput){				
				if(Input.GetAxis("Vertical") < 0)
					Select = false;
				else if(Input.GetAxis("Vertical") > 0)
					Select = true;
				
				if(Input.GetButton("Fire1")){
					if(Select){
						GUION = false;
						GameOver = false;
						
						//
						GameObject player = GameObject.FindGameObjectWithTag("Player");
						
						LevelGenerator gen = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<LevelGenerator>();
						GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Gameplay_GUI>().Oxygen = 1.0f;
						gen.currentRoom.KillSpawns();
						//
						
						GameObject corpse = Instantiate(Fireman, (player.transform.position + new Vector3(0.1f, 0.1f, 0.1f)), Quaternion.Euler(0,0,0) ) as GameObject ;
						//corpse.animation.Stop();				
						gen.currentRoom.corpses.Add( corpse );
						
						
						//
						gen.currentRoom = gen.initialRoom;
						GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(-5, 6, -5);
						GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0.0f, 0.1f, 3.0f);
						GameObject.FindGameObjectWithTag("Player").transform.forward = new Vector3(0,0,1);
						ScoreRef.g_score = 0;
					}
					else{
						Application.Quit();
					}
				}
			}
		}
		else
		{
			lockco = false;
		}
	}
	
	IEnumerator PlayDeath(){
		lockinput = true;
		yield return new WaitForSeconds(2.0f);
		lockinput = false;
	}
}
