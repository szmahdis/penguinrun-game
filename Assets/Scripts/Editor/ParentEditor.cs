using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Parent))]

public class ParentEditor : Editor
{
   
    private string type;
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

 		Parent myScript = (Parent)target;

        GUILayout.BeginHorizontal();

        type = EditorGUILayout.TextField("Tag", type);
        
        if (GUILayout.Button("Make every object with this Tag a child"))
        {
            myScript.BecomeParent(type);
        }
        
        GUILayout.EndHorizontal();
    }
}

