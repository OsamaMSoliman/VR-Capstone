using System.Collections.Generic;
using UnityEngine;

public class Wait : MonoBehaviour
{
	private static Dictionary<float , WaitForSeconds> _wait = new Dictionary<float , WaitForSeconds>();

	public static WaitForSeconds ForSeconds( float sec )
	{
		if ( !_wait.ContainsKey(sec) )
			_wait[sec] = new WaitForSeconds(sec);
		return _wait[sec];
	}
}
