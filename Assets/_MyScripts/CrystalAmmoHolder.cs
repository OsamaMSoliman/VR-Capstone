using System;
using UnityEngine;

public class CrystalAmmoHolder : MonoBehaviour
{
	#region testing for now
	[Obsolete("pass Integer" , true)]
	public void Show( bool state )
	{
		foreach ( Transform child in transform )
			if ( child.gameObject.activeInHierarchy != state )
			{
				child.gameObject.SetActive(state);
				break;
			}

	}
	#endregion

	[SerializeField] private float radius = 1f;

	private void Awake()
	{
		if ( transform.childCount < 1 ) Debug.LogError("must be at least one child" , gameObject);
		transform.GetChild(0).gameObject.SetActive(false);
	}

	public void Show( int size = 3 )
	{
		float ang = (360f / size) * Mathf.Deg2Rad;
		for ( int i = size - transform.childCount ; i > 0 ; i-- )
			Instantiate(transform.GetChild(0) , transform);
		for ( int i = 0 ; i < transform.childCount ; i++ )
		{
			var child = transform.GetChild(i);
			child.gameObject.SetActive(i < size);
			if ( i < size )
			{
				child.position = pointOnCircle(transform.position , radius , ang * i);
				// TODO: change the size to always fit in the circle
				Crystal crystal = child.GetComponent<Crystal>();
				if ( crystal != null ) StartCoroutine(crystal.ProduceShaderAnimation());
			}
		}
	}

	private Vector3 pointOnCircle( Vector3 center , float radius , float angle ) => new Vector3(
			 center.x + radius * Mathf.Sin(angle) ,
			 center.y + radius * Mathf.Cos(angle) ,
			 center.z);


}
