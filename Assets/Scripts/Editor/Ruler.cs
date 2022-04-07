using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Ruler : EditorWindow
{

    GameObject ruler;
    GameObject object2;

    [MenuItem("Window / Measurement tools")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(Ruler));
    }
    
    private void OnGUI()
    {
        GUILayout.Label("Select the ruler your want to use here:", EditorStyles.label);
        ruler = (GameObject)EditorGUILayout.ObjectField("Current Ruler", ruler, typeof(GameObject), true);
        // object2 = (GameObject)EditorGUILayout.ObjectField("Object 2", object2, typeof(GameObject), true);

        GUILayout.Label("Places the Ruler between both currently selected objects", EditorStyles.label);
        if (GUILayout.Button("Show Distance"))
        {
            if (Selection.gameObjects.Length == 2 && ruler != null) {
                //Sets ruler object inbetween both selected objects
                ruler.transform.position = new Vector3((Selection.gameObjects[0].transform.position.x + Selection.gameObjects[1].transform.position.x) / 2,
               (Selection.gameObjects[0].transform.position.y + Selection.gameObjects[1].transform.position.y) / 2 +5,
               (Selection.gameObjects[0].transform.position.z + Selection.gameObjects[1].transform.position.z) / 2);
            } 
        }
        GUILayout.Label("Rotates the Ruler by 90 Degrees", EditorStyles.label);
        if (GUILayout.Button("Rotate Ruler"))
        {
            ruler.transform.Rotate(0, 90, 0);
        }
        GUILayout.Label("Hides the Ruler from plain sight", EditorStyles.label);
        if (GUILayout.Button("Hide Ruler"))
        {
            ruler.transform.position = new Vector3(1000, 0, 0);
        }

    }
}
