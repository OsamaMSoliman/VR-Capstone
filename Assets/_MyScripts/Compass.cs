using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
	[Header("Update can't work while disabled")]
	[Header("Note: This script must be on the parent")]

	[SerializeField] private LightTrailGuide lightTrailPrefab;
	private List<LightTrailGuide> LTGs;

	public int AmmoLimit { get; set; }
	public Transform Holder { get { return transform.GetChild(0); } }

	#region simple Singleton
	public static Compass Self;
	private void Awake() => Self = this;
	#endregion


	private void Start()
	{
		AmmoLimit = 3;
		LTGs = new List<LightTrailGuide>();
		for ( int i = 0 ; i < AmmoLimit ; i++ )
			LTGs.Add(Instantiate(lightTrailPrefab , transform));
	}

	void Update()
	{
		if ( PlayerType.Self.isVR )
		{
			if ( Mathf.Abs(Vector3.Angle(transform.right , Vector3.up)) < 25 )
			{
				//TODO: enable light
				Holder.gameObject.SetActive(true);
				if ( OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger , OVRInput.Controller.LTouch) )
					SpawnLightTrailGuide();
			}
			else
			{
				Holder.gameObject.SetActive(false);
			}
		}
		else
		{
			if ( Input.GetMouseButtonDown(0) ) SpawnLightTrailGuide();
			if ( Input.GetMouseButtonDown(1) ) Holder.gameObject.SetActive(!Holder.gameObject.activeInHierarchy);
		}

	}

	private void SpawnLightTrailGuide()
	{
		if ( AmmoLimit <= 0 ) return;

		AmmoLimit--;

		bool didNotSpawn = true;
		for ( int i = 0 ; i < LTGs.Count ; i++ )
			if ( !LTGs[i].gameObject.activeInHierarchy )
			{
				LTGs[i].transform.position = Holder.position;
				LTGs[i].transform.SetParent(null);
				LTGs[i].gameObject.SetActive(true);
				didNotSpawn = false;
				break;
			}
		if ( didNotSpawn )
		{
			var ltg = Instantiate(lightTrailPrefab , transform.position , Quaternion.identity);
			ltg.gameObject.SetActive(true);
			LTGs.Add(ltg);
		}
	}
}