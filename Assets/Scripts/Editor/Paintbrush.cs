using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Paintbrush : EditorWindow
{

    Color color;

    [MenuItem("Window / Paint Objects")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(Paintbrush));
    }

    private void OnGUI()
    {
        GUILayout.Label("Select the color your want to use here:", EditorStyles.boldLabel);
        color = EditorGUILayout.ColorField("Desired color", color);

        GUILayout.Label("Now select all Objects you wish to recolor and select 'Change Color'", EditorStyles.label);
        if ( GUILayout.Button("Change Color"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if(renderer != null)
                {
                    renderer.sharedMaterial.color = color;
                }
            }
        }
    }
}
