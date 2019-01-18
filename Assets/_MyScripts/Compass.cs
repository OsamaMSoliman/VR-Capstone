using System.Collections.Generic;
using UnityEngine;
//TODO: crystal light fades
//TODO: crystal light attract the monster
public class Compass : MonoBehaviour
{
	[Header("Update can't work while disabled")]
	[Header("Note: This script must be on the parent")]

	[SerializeField] private LightTrailGuide lightTrailPrefab;
	[SerializeField] private CrystalAmmoHolder gemAmmoHolder;
	private List<LightTrailGuide> LTGs;

	private int _ammoLimit;
	public int AmmoLimit {
		get { return _ammoLimit; }
		set {
			gemAmmoHolder?.Show(value);
			_ammoLimit = value;
		}
	}
	public Transform Holder { get { return transform.GetChild(0); } }

	#region simple Singleton
	public static Compass Self { get; private set; }
	private void Awake() => Self = this;
	#endregion


	private void Start()
	{
		AmmoLimit = 1;
		LTGs = new List<LightTrailGuide>();
		for ( int i = 0 ; i < AmmoLimit ; i++ )
			LTGs.Add(Instantiate(lightTrailPrefab , transform));
	}

	void Update()
	{
		if ( Player.Self.isVR )
		{
			if ( Mathf.Abs(Vector3.Angle(transform.right , Vector3.up)) < 25 )
			{
				Holder.gameObject.SetActive(true);
				if ( OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger , OVRInput.Controller.LTouch) )
					SpawnLightTrailGuide();
				Monster.Self.GoAfterPlayer();
			}
			else
			{
				Holder.gameObject.SetActive(false);
				Monster.Self.StopChasing();
			}
		}
		else
		{
			if ( Input.GetMouseButtonDown(0) ) SpawnLightTrailGuide();
			if ( Input.GetMouseButtonDown(1) )
			{
				bool b = Holder.gameObject.activeInHierarchy;
				Holder.gameObject.SetActive(!b);
				if ( b ) Monster.Self.StopChasing();
				else Monster.Self.GoAfterPlayer();
			}
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