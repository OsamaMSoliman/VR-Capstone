using System.Collections;
using UnityEngine;

public class SphereGoal : MonoBehaviour
{
	public static SphereGoal Self { get; internal set; }
	private void Awake() => Self = this;

	public void Descend() => StartCoroutine(Descending());

	private IEnumerator Descending()
	{
		while ( transform.position.y > 0 )
		{
			transform.position -= Vector3.up * Time.deltaTime;
			yield return null;
		}
		GetComponent<Animator>().enabled = false;
		transform.localScale = Vector3.one * 10;
	}

	private void OnTriggerEnter( Collider other )
	{
		if ( other.CompareTag("Player") )
			Player.Self.GameOver();
	}

	public void StartPosition( Vector3 vector3 ) => transform.position = vector3 + Vector3.up * 30;
}
