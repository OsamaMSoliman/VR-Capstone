using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player Self { get; private set; }
	public bool isVR;

	[SerializeField] private Canvas canvas;
	[SerializeField] private ParticleSystem Fog;
	private HeadBehaviour head;

	private void Awake()
	{
		Self = this;

		transform.GetChild(isVR ? 0 : 1).gameObject.SetActive(true);
		canvas.gameObject.SetActive(!isVR);
		Fog.transform.SetParent(transform.GetChild(isVR ? 0 : 1));
		head = GetComponentInChildren<HeadBehaviour>();
	}

	public Transform Transfrom { get { return isVR ? head.transform : transform.GetChild(1); } }


	public void GameOver()
	{
		head?.FadeOut();
		StartCoroutine(LoadLevel.Begin(0));
	}


	public void StartPosition( Vector3 vector3 )
	{
		if ( Player.Self.isVR ) Transfrom.position = vector3;
		else
		{
			CharacterController cc = Transfrom.GetComponent<CharacterController>();
			cc.enabled = false;
			Transfrom.position = vector3 + Vector3.up;
			cc.enabled = true;
		}
	}

}
