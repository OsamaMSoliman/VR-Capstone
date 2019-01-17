using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class LightTrailGuide : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private NavMeshPath minPath;

    private void Awake() => navMeshAgent = GetComponent<NavMeshAgent>();

    private void OnEnable() => StartCoroutine(CalcAllPathes());

    private static WaitForSeconds delay5s = new WaitForSeconds(5f);
    private IEnumerator CalcAllPathes()
    {
        NavMeshPath minPath = null;
        float minDistance = float.MaxValue;
        foreach (var target in Objective.objectives)
        {
            if (target.gameObject.activeInHierarchy)
            {
                NavMeshPath path = new NavMeshPath();
                if (NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, path))//&& path.status == NavMeshPathStatus.PathComplete )
                {
                    float temp = PathLength(path.corners);
                    if (temp < minDistance)
                    {
                        minDistance = temp;
                        minPath = path;
                    }
                }
                yield return null;
            }
        }
        if (minPath != null)
        {
            navMeshAgent.SetPath(minPath);
            while (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance + 1)
                yield return delay5s;
            //TODO: make it dramatic (polishing)
            gameObject.SetActive(false);
            transform.SetParent(Compass.Self.Holder);
        }
    }

    private float PathLength(Vector3[] corners)
    {
        if (corners.Length < 2) return 0;
        float sum = 0;
        for (int i = 0; i < corners.Length - 1; i++)
            sum += Vector3.Distance(corners[i], corners[i + 1]);
        return sum;
    }
}
