using UnityEngine;
using System.Collections;

public class Gameplay_GUI : MonoBehaviour {
	
	public float Oxygen = 1.0f;
	
	public int g_score = 0;
	
	private Vector2 pos = new Vector2(Screen.width/4, 0);
	
	private Vector2 size = new Vector2(Screen.width/4, Screen.height/20);
	
	private Vector2 Lpos = new Vector2(Screen.width/2, 0);
	
	private	Vector2 Lsize = new	Vector2(Screen.width/4, Screen.height/20);
	
	public Texture OxygenBarEmpty;
	
	public Texture OxygenBarFull;
	
	public GUIStyle customGuiStyle;
	
	public AudioClip thud;
	
	public AudioClip Cleared;
	
	public float RateofOxygenLoss = 0.0055f;
	
	public bool BONUS = false;
	
	public int FiresExisting = 1;
	
	public bool CLEARED = false;
	
	public bool NoneExisted = false;
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(pos.x, pos.y, size.x, size.y), OxygenBarEmpty);
		GUI.BeginGroup(new Rect(pos.x, pos.y, size.x * Mathf.Clamp01(Oxygen), size.y));
		GUI.DrawTexture(new Rect(0, 0, size.x, size.y), OxygenBarFull);
		GUI.EndGroup();
		//GUI.skin.label.fontSize = 50;
		GUI.Label(new Rect(Lpos.x, Lpos.y, Lsize.x, Lsize.y), g_score.ToString(), customGuiStyle);
		if(BONUS)
		{
			GUI.Label(new Rect(Screen.width/2, Screen.height/19, Screen.width/4, Screen.height/20), "FIRE CLEAR BONUS!", customGuiStyle);
		}		                   
	}
	
	// Use this for initialization
	void Start () {
		PreCheckforFire();
	}
	
	IEnumerator FlashBonus(){
		BONUS = true;
		audio.clip = Cleared;
		audio.pitch = 0.75f;
		audio.volume = 0.5f;
		audio.Play();
		g_score += 5000;
		yield return new WaitForSeconds(2.0f);
		BONUS = false;
	}
	
	// Update is called once per frame
	void Update () {
		CheckforFire();
		
		GameObject[] LevelRef = GameObject.FindGameObjectsWithTag("LevelGenerator");
		
		LevelGenerator Rooming = LevelRef[0].gameObject.GetComponent<LevelGenerator>();
		
		if(Rooming.RoomChange == true){
			NoneExisted = false;
			PreCheckforFire();
		}
		
		if(!NoneExisted){
			CheckforFire();
		}
		
		if(FiresExisting == 0 && !CLEARED)
		{
			StartCoroutine("FlashBonus");
			CLEARED = true;
		}
		else if(FiresExisting != 0){
			CLEARED = false;
		}
		
		Oxygen -= RateofOxygenLoss * Time.deltaTime;
		
		if(Oxygen <= 0.0f)
		{
			GameObject[] GUIRef = GameObject.FindGameObjectsWithTag("MainCamera");
			
			Kill_Screen GameOverRef = GUIRef[0].gameObject.GetComponent<Kill_Screen>();
			
			if(!GameOverRef.GameOver){				
				audio.clip = thud;
				audio.Play();
			}			
			
			GameOverRef.GameOver = true;
		}
		//g_score++;
	}
	
	void PreCheckforFire(){
		GameObject FireCount = GameObject.FindGameObjectWithTag("Fire");
		if(FireCount == null){
			NoneExisted = true;
		}
		else{
			NoneExisted = false;
		}
	}
	
	void CheckforFire(){
		GameObject FireCount = GameObject.FindGameObjectWithTag("Fire");
		if(FireCount == null){
			FiresExisting = 0;
		}
		else{
			FiresExisting = 1;
		}
	}
}
