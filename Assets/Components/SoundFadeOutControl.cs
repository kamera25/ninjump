using UnityEngine;
using System.Collections;

public class SoundFadeOutControl : MonoBehaviour 
{
	bool fadeOut = false;
	float dump = 1.5F;

	AudioSource audioSource;

	void Start()
	{
		audioSource = this.GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () 
	{
		if( fadeOut)
		{
			audioSource.volume -= Time.deltaTime * dump;
		}
	}

	public void EnableFadeOut()
	{
		fadeOut = true;
	}
}
