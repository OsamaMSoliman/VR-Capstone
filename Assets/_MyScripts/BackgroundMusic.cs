using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

	[SerializeField] private AudioClip start;
	private void Start()
	{
		if ( start != null ) GetComponent<AudioSource>().PlayOneShot(start);
	}

}
