using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GameStarter))]
public class ObjectBuilderEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		GameStarter myScript = (GameStarter)target;
		if (GUILayout.Button("Start Game"))
		{
			myScript.StartGame();
		}
	}
}