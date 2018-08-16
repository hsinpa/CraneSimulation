using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NetWall))]
public class InspectorButton_NetWallCO : Editor {

	public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        NetWall myScript = (NetWall)target;
        if(GUILayout.Button("Build Object"))
        {
            myScript.BuildObject();
        }
    }
}
