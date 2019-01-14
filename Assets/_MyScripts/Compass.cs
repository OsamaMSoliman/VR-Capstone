using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField] private LightTrailGuide lightTrailPrefab;
    private List<LightTrailGuide> lights;
    private int maxLimit = 3;

    private Light handLight;

    private void Start()
    {
        handLight = GetComponent<Light>();

        //lights = new List<LightTrailGuide>();
        //for (int i = 0; i < maxLimit; i++)
        //  lights.Add(Instantiate(lightTrailPrefab, transform));
    }

    void Update()
    {
        if (Mathf.Abs(Vector3.Angle(transform.right, Vector3.up)) < 25)
        {
            //TODO: enable light
            handLight.enabled = true;
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
            {
                //TODO: Spawn LightTrail
                Instantiate(lightTrailPrefab, transform.position, Quaternion.identity).gameObject.SetActive(true);
            }
        }
        else
        {
            handLight.enabled = false;

        }
    }

    private void SpawnFromPool()
    {
        //TODO: 
    }
}
