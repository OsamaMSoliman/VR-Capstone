using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class LightTrailGuide : MonoBehaviour
{
	private NavMeshAgent navMeshAgent;

	private void Awake()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	private void OnEnable()
	{
		StartCoroutine(CalcAllPathes());
	}

	private IEnumerator CalcAllPathes()
	{
		NavMeshPath path = new NavMeshPath();
		foreach ( var target in GemSpawner.DistanceToGem.Keys )
		{
			path.ClearCorners();
			if ( NavMesh.CalculatePath(transform.position , target.position , NavMesh.AllAreas , path) )//&& path.status == NavMeshPathStatus.PathComplete )
				GemSpawner.DistanceToGem[target] = PathLength(path.corners);
			yield return null;
		}
		// signal ready to fire
	}

	private float PathLength( Vector3[] corners )
	{
		float sum = 0;
		for ( int i = 0 ; i < corners.Length - 1 ; i++ )
			sum += Vector3.Distance(corners[i] , corners[i + 1]);
		return sum;
	}


	#region Testing
	RaycastHit hit;
	void Update()
	{
		if ( Input.GetMouseButtonDown(0) )
		{
			if ( Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition) , out hit , 200f) )
				navMeshAgent.SetDestination(hit.point);
		}
		else if ( Input.GetMouseButtonDown(1) )
		{

		}
	}
	#endregion

}
