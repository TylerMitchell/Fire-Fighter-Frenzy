using UnityEngine;
using System.Collections;

public class RagingSoundPlayer : MonoBehaviour {
	
	public AudioClip RagingFire;
	
	public bool FirePresent = true;
	
	public int FullFire = 15;
	
	public int FireCount;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		GameObject[] FireCounter = GameObject.FindGameObjectsWithTag("Fire");
		int temp = 0;
		foreach(GameObject Fire in FireCounter){
			temp++;
		}
		
		FireCount = temp;
		
		
		audio.clip = RagingFire;
		
		audio.volume = (float)FireCount/(float)FullFire;
		
		if(FirePresent){
			if(!audio.isPlaying){
				audio.Play();
			}
		}
		else{
			if(!audio.isPlaying){
				audio.Stop();
			}
		}
	}
}
