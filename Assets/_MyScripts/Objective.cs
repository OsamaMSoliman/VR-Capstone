using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public static List<Objective> objectives = new List<Objective>();

    private void Awake()
    {
        objectives.Add(this);
    }
}
