using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshGenerator))]
public class GemSpawner : MonoBehaviour
{
	[SerializeField] private GameObject GemPrefab;

	private Transform GemsHolder;
	private List<Vector3> GemsLocations;


	private void Start()
	{
		GetComponent<MapGenerator>().signalGeneratingIsOver += () => SpawnGems(GetComponent<MeshGenerator>().Corners());

	}

	private void SpawnGems( List<Vector3> GemsLocations )
	{
		this.GemsLocations = GemsLocations;
		if ( GemPrefab == null )
		{
			Debug.LogError("Gem Prefab not assigned" , gameObject);
			return;
		}
		GemsHolder = new GameObject("GemsHolder").transform;
		foreach ( var pos in GemsLocations )
		{
			Instantiate(GemPrefab , pos , Random.rotation , GemsHolder);
		}
	}


	private void OnDrawGizmos()
	{
		if ( GemsLocations == null ) return;
		foreach ( var gl in GemsLocations )
		{
			Gizmos.DrawCube(gl + Vector3.up , Vector3.one);
		}

	}
}
