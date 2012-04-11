using UnityEngine;
using System.Collections;

public class Fire_Properties : MonoBehaviour {
	
	public float Health;
	
	public float MaxHealth;
	
	public float RateofDamage = 0.2f;
	
	public float Damage = 3.0f;
	
	private bool HealthLock = false;
	
	private ParticleEmitter FireParticles;
	
	private Light FireLight;
	
	public int Fire_ID_i;
	
	public int Fire_ID_j;
	
	//public GameObject GUIRef;
	
	void OnParticleCollision(GameObject other){
		if(other.tag == "Stream"){
			if(!HealthLock){
				//Debug.Log("Hit Fi");
				StartCoroutine("HealthUpdate");				
			}
		}
	} 
	
	IEnumerator HealthUpdate(){
		HealthLock = true;		
		Health = Health - Damage;
		yield return new WaitForSeconds(RateofDamage);		
		HealthLock = false;
	}
	
	 void OnTriggerEnter(Collider other) {
		//Debug.Log("Trigger Enter");
		
        Destroy(other.gameObject);
    }
	
	// Use this for initialization
	void Start () {
		Health = 50.0f;
		MaxHealth = Health;
		
		//FireParticles = this.transform.Find("Fire_Particles").gameObject.particleEmitter;
		
		//FireParticles.Emit();
		
		//FireLight = this.transform.Find("Fire_Light").gameObject.light;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		FireParticles = this.transform.Find("Fire_Particles").gameObject.particleEmitter;
		
		FireLight = this.transform.Find("Fire_Light").gameObject.light;
		
		FireParticles.maxEnergy = 3.0f * (Health/MaxHealth);
		
		FireLight.range = 1.5f * Mathf.Sqrt((Health/MaxHealth));
		
		//Debug.Log("Health " + Health);
		
		if(Health <= 0){
			Destroy(this.gameObject);
			
			//gameObject GUIRef = gameObject.GetComponent
			
			GameObject[] GUIRef = GameObject.FindGameObjectsWithTag("MainCamera");
			
			Gameplay_GUI ScoreRef = GUIRef[0].gameObject.GetComponent<Gameplay_GUI>();
			
			ScoreRef.g_score += 10;
			
			RemoveFireFromRoomTable();
			
		}
	}
	
	void RemoveFireFromRoomTable(){
		//NEED TO DO
		
	}
	/*
	private float GetFireHealth( )
	{
		return Fire.Health;
	}
	
	private void SetFireHealth(float Updated)
	{
		Fire.Health = Updated;
	} */
}
