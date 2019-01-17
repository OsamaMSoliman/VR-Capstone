using System.Collections;
using UnityEngine;

public class Gem : MonoBehaviour
{
	[SerializeField] private float consumptionDuration = 2f;

	private new Renderer renderer;
	private bool isConsumed;

	private void Start()
	{
		renderer = GetComponentInChildren<Renderer>();
		if ( renderer == null ) Debug.LogError("no renderer in the children" , gameObject);
	}

	private static WaitForSeconds delay10th = new WaitForSeconds(0.1f);
	private IEnumerator GemGrabbed()
	{
		if ( !isConsumed )
		{
			isConsumed = true;

			// dissolve = true
			renderer.material.SetFloat("Boolean_D9AB7FF2" , 1);
			// start dissolving
			for ( float dissolveFactor = consumptionDuration ; dissolveFactor > 0 ; dissolveFactor -= 0.1f )
			{
				renderer.material.SetFloat("Vector1_FE8C72C3" , dissolveFactor / consumptionDuration);
				//TODO: show the gem power appear on the right hand
				yield return delay10th;
			}

			Compass.Self.AmmoLimit++;
			//don't SetActive to false, leave a trace as a help for the player
		}
	}

	private void OnTriggerEnter( Collider other )
	{
		print(other.name);
		if ( other.CompareTag("Player") && !PlayerType.Self.isVR )
		{
			StartCoroutine(GemGrabbed());
		}
	}

}
