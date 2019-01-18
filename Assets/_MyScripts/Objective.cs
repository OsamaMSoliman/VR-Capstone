using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
	public static List<Objective> objectives = new List<Objective>();
	private void Awake() => objectives.Add(this);

	[SerializeField] private float dissolveTime = 1f;
	private new Renderer renderer;
	private OVRGrabbable grabbable;

	private void Start()
	{
		renderer = GetComponentInChildren<Renderer>();
		if ( renderer == null ) Debug.LogError("no renderer in the children" , gameObject);
		grabbable = GetComponent<OVRGrabbable>();
		grabbable.GrabStartSignal += Grabbed;
	}

	private IEnumerator ObjectiveGrabbed()
	{
		GetComponent<AudioSource>().Play();
		//NoisePower
		renderer.material.SetFloat("Vector1_CE39B22F" , Random.value * 10);
		//dissolve Color
		renderer.material.SetColor("Color_7E6BB6F2" , Random.ColorHSV());
		// start dissolving
		for ( float dissolveFactor = 0 ; dissolveFactor < dissolveTime ; dissolveFactor += 0.1f )
		{
			renderer.material.SetFloat("Vector1_414CC3D2" , dissolveFactor / dissolveTime);
			yield return Wait.ForSeconds(0.1f);
		}

		gameObject.SetActive(false);
		objectives.Remove(this);
		if ( objectives.Count == 0 )
			SphereGoal.Self.Descend();

	}

	private void OnTriggerEnter( Collider other )
	{
		if ( other.CompareTag("Player") && !Player.Self.isVR )
		{
			StartCoroutine(ObjectiveGrabbed());
		}
	}

	private void Grabbed()
	{
		grabbable.grabbedBy.ForceRelease(grabbable);
		StartCoroutine(ObjectiveGrabbed());
	}
	private void OnDestroy()
	{
		objectives.Remove(this);
	}
}
