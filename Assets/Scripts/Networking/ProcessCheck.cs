using System;
using UnityEngine;

// This is being used

using UnityEngine.SceneManagement; // This is being used

namespace Assets.Scripts.Networking
{
	public class ProcessCheck : MonoBehaviour
	{
		private void Start()
		{
		    Environment.GetCommandLineArgs();
#if !UNITY_EDITOR
		var arguments = Environment.GetCommandLineArgs();
		if (arguments.Length <= 1) return;

		SceneManager.LoadScene(4);
#endif
		}
	}
}