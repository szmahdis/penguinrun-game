using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Health))]
public class HealthCheat : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.BeginHorizontal();
        Health health = (Health)target;
        if(GUILayout.Button("Replenish Health"))
        {
            health.AddHealth(5);
        }
        if(GUILayout.Button("Decrease Health"))
        {
            health.TakeDamage(1);
        }
        GUILayout.EndHorizontal();
    }
}
