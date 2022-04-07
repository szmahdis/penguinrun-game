using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Light))]
public class LightSlider : Editor
{
    public override void OnInspectorGUI()
    {
        
        base.OnInspectorGUI();
        Light lightobject = (Light)target;
        

        GUILayout.Label("Adjust brightness to [0,1]");
        lightobject.intensity = EditorGUILayout.Slider("Intensity",lightobject.intensity, 0f, 1f);

        

    }
}
