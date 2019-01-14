using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class LightTrailGuide : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private NavMeshPath minPath;

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
        NavMeshPath minPath = null;
        float minDistance = float.MaxValue;
        foreach (var target in Objective.objectives)
        {
            //TODO: doesn't go to the nearest one
            path.ClearCorners();
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
        yield return null;
        if (minPath != null) navMeshAgent.SetPath(minPath);
    }

    private float PathLength(Vector3[] corners)
    {
        float sum = 0;
        for (int i = 0; i < corners.Length - 1; i++)
            sum += Vector3.Distance(corners[i], corners[i + 1]);
        return sum;
    }


    #region Testing
    RaycastHit hit;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 200f))
                navMeshAgent.SetDestination(hit.point);
        }
        else if (Input.GetMouseButtonDown(1))
        {

        }
    }
    #endregion

}
