using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MapGenerator))]
public class Spawner : MonoBehaviour
{
	[SerializeField] private GameObject CrystalPrefab;
	[SerializeField] private int maxAmountPerLevel;

	private Transform GemsHolder;

	private void Awake()
	{
		MapGenerator mg = GetComponent<MapGenerator>();
		mg.SignalGeneratingIsOver += () => SpawnCrystalsAndNotes(mg.meshGen.Corners());
		mg.SignalGeneratingIsOver += () => SpawnPlayerAndMonsterAndSphereGoal(mg.PositionsInRooms());
	}

	private void SpawnCrystalsAndNotes( List<Vector3> GemsLocations )
	{
		//this.GemsLocations = GemsLocations;
		if ( CrystalPrefab == null )
		{
			Debug.LogError("Gem Prefab not assigned" , gameObject);
			return;
		}
		GemsHolder = new GameObject("GemsHolder").transform;
		int i = 0;
		foreach ( var pos in GemsLocations.OrderBy(a => Random.value) )
		{
			i++;
			//if ( i >= maxAmountPerLevel ) return;
			if ( i < maxAmountPerLevel )
				Instantiate(CrystalPrefab , pos + Vector3.up , Random.rotation , GemsHolder);
			//TODO: spawning objectives inside CrystalSpawner?!?!!?
			else if ( i - maxAmountPerLevel < Objective.objectives.Count )
			{
				Objective.objectives[i - maxAmountPerLevel].transform.position = pos + Vector3.up;
			}
			else
			{
				return;
			}
		}

	}


	private void SpawnPlayerAndMonsterAndSphereGoal( List<Vector3> positionInRooms )
	{
		//this.positionInRooms = positionInRooms;
		Player.Self.StartPosition(positionInRooms[Random.Range(0 , positionInRooms.Count)]);
		Monster.Self.StartPosition(positionInRooms[Random.Range(0 , positionInRooms.Count)]);
		SphereGoal.Self.StartPosition(positionInRooms[Random.Range(0 , positionInRooms.Count)]);
	}


	//#region Testing
	//public List<Vector3> GemsLocations;
	//public List<Vector3> positionInRooms;
	//private void OnDrawGizmos()
	//{
	//if ( positionInRooms == null ) return;
	//foreach ( var p in positionInRooms )
	//Gizmos.DrawCube(p , Vector3.one);
	//if ( GemsLocations == null ) return;
	//foreach ( var gl in GemsLocations )
	//Gizmos.DrawCube(gl + Vector3.up , Vector3.one);
	//}
	//#endregion

}
