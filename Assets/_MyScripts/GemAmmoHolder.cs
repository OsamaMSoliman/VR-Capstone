using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemAmmoHolder : MonoBehaviour
{
    //TODO: cicle equation, and when ammo increase increase the count and make the smaller so they all fit in the circle


    // testing for now 
    public void Show(bool state)
    {
        foreach (Transform child in transform)
            if (child.gameObject.activeInHierarchy != state)
            {
                child.gameObject.SetActive(state);
                break;
            }
    }
}
