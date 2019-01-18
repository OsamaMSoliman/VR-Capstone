using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{

	[Obsolete("use Integer as a parameter" , true)]
	public static IEnumerator Begin( string name )
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);
		asyncLoad.allowSceneActivation = false;
		yield return Wait.ForSeconds(2f);
		while ( !asyncLoad.isDone ) yield return null;
		asyncLoad.allowSceneActivation = true;
	}

	public static IEnumerator Begin( int sceneBuildIndex )
	{
		yield return Wait.ForSeconds(2f);
		SceneManager.LoadSceneAsync(sceneBuildIndex);
	}
}
