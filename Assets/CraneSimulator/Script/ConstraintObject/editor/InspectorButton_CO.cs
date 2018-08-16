using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Pillar))]
public class InspectorButton_CO : Editor {

	public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        Pillar myScript = (Pillar)target;
        if(GUILayout.Button("Build Object"))
        {
            myScript.BuildObject();
        }
    }
}
