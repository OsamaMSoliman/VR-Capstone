using System.Collections;
using UnityEngine;

[RequireComponent(typeof(OVRGrabbable))]
public class Crystal : MonoBehaviour
{
	[SerializeField] private float dissolveTime = 2f;

	private new Renderer renderer;
	private bool isConsumed;
	private OVRGrabbable grabbable;
	private bool NotRotating;

	private void Start()
	{
		renderer = GetComponentInChildren<Renderer>();
		if ( renderer == null ) Debug.LogError("no renderer in the children" , gameObject);
		grabbable = GetComponent<OVRGrabbable>();
		grabbable.GrabStartSignal += Grabbed;
		NotRotating = true;
	}

	private IEnumerator Consume()
	{
		if ( !isConsumed )
		{
			isConsumed = true;

			// dissolve = true
			renderer.material.SetFloat("Boolean_D9AB7FF2" , 1);
			// start dissolving
			for ( float dissolveFactor = dissolveTime ; dissolveFactor > 0 ; dissolveFactor -= 0.1f )
			{
				renderer.material.SetFloat("Vector1_FE8C72C3" , dissolveFactor / dissolveTime);
				yield return Wait.ForSeconds(0.1f);
			}

			Compass.Self.AmmoLimit++;
			//don't SetActive to false, leave a trace as a help for the player
		}
	}


	public IEnumerator ProduceShaderAnimation()
	{
		if ( renderer != null )
		{
			// dissolve = true
			yield return Wait.ForSeconds(0.1f);

			renderer.material.SetFloat("Boolean_D9AB7FF2" , 1);
			// start dissolving
			for ( float dissolveFactor = 0 ; dissolveFactor < dissolveTime ; dissolveFactor += 0.1f )
			{
				renderer.material.SetFloat("Vector1_FE8C72C3" , dissolveFactor / dissolveTime);
				yield return Wait.ForSeconds(0.1f);
			}
			// dissolve = true
			renderer.material.SetFloat("Boolean_D9AB7FF2" , 0);
			if ( NotRotating )
			{
				NotRotating = false;
				Vector3 rotationDirection = transform.position + Random.onUnitSphere;
				while ( true )
				{
					transform.Rotate(rotationDirection);
					yield return Wait.ForSeconds(0.1f);
				}
			}
		}
	}


	private void OnTriggerEnter( Collider other )
	{
		if ( other.CompareTag("Player") && !Player.Self.isVR )
		{
			StartCoroutine(Consume());
		}
	}

	private void Grabbed()
	{
		grabbable.grabbedBy.ForceRelease(grabbable);
		StartCoroutine(Consume());
	}

}
