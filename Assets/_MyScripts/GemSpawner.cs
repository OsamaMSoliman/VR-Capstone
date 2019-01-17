using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MapGenerator))]
public class GemSpawner : MonoBehaviour
{
	[SerializeField] private GameObject GemPrefab;
	[SerializeField] private int maxAmountPerLevel;

	private Transform GemsHolder;


	private void Awake()
	{
		MapGenerator mg = GetComponent<MapGenerator>();
		mg.SignalGeneratingIsOver += () => SpawnGems(mg.meshGen.Corners());
	}

	private void SpawnGems( List<Vector3> GemsLocations )
	{
		//this.GemsLocations = GemsLocations;
		if ( GemPrefab == null )
		{
			Debug.LogError("Gem Prefab not assigned" , gameObject);
			return;
		}
		GemsHolder = new GameObject("GemsHolder").transform;
		int i = 0;
		foreach ( var pos in GemsLocations.OrderBy(a => Random.value) )
		{
			i++;
			if ( i >= maxAmountPerLevel ) return;
			Instantiate(GemPrefab , pos + Vector3.up , Random.rotation , GemsHolder);
		}

	}


	//#region Testing
	//private List<Vector3> GemsLocations;
	//private void OnDrawGizmos()
	//{
	//if ( GemsLocations == null ) return;
	//foreach ( var gl in GemsLocations )
	//{
	//Gizmos.DrawCube(gl + Vector3.up , Vector3.one);
	//}
	//}
	//#endregion

}
