using UnityEngine;

public class PlayerType : MonoBehaviour
{
	public static PlayerType Self { get; private set; }
	public bool isVR;

	[SerializeField] private ParticleSystem Fog;

	private void Awake()
	{
		if ( Self == null )
			Self = this;
		else
		{
			Destroy(this);
			return;
		}
		transform.GetChild(isVR ? 0 : 1).gameObject.SetActive(true);
		Fog.transform.SetParent(transform.GetChild(isVR ? 0 : 1));
	}
}
