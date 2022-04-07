using System;
using UnityEditor;
using UnityEngine;

namespace EnemyTool
{
    [CustomEditor(typeof(Ghost))]
    public class GhostEditor : Editor
    {
    
        Vector3 distance;
        int listPosition;

        public void OnSceneGUI()
        {
            Ghost ghost = (Ghost)target;

            // get the chosen game object
            Ghost t = target as Ghost;

            if( t == null || t.gameObject == null )
                return;

            GameObject selected = Selection.activeGameObject;
            if (selected != null && selected.Equals(t.gameObject))
            {
                // iterate over game objects added to the array...
                for( int i = 0; i < t.wayPoints.Count; i++ )
                {
                    // ... and draw a line between them
                    if (t.wayPoints[i] != null)
                    {
                        if(i<t.wayPoints.Count-1 && t.wayPoints[i+1] != null)
                            Handles.DrawLine( t.wayPoints[i].transform.position, t.wayPoints[i+1].transform.position);
                        
                        
                        Handles.Label(t.wayPoints[i].transform.position, i.ToString());
                        /*
                        Handles.BeginGUI();
                        GUILayout.BeginArea(new Rect(20, 20, 150, 60));
                        var rect = EditorGUILayout.BeginVertical();
                        GUI.color = Color.yellow;
                        GUI.Box(rect, GUIContent.none);
                        GUI.color = Color.white;
                        GUILayout.FlexibleSpace();
                        GUILayout.Label(i.ToString());
                        GUILayout.FlexibleSpace();
                        GUILayout.EndArea();
                        Handles.EndGUI();*/
                    }
                      
                    
                }
            }
           
            
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Ghost myScript = (Ghost)target;

            if (GUILayout.Button("delete waypoints not in the scene"))
            {
                myScript.RemoveEmptyWayPoints();
            }
        
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("enable gizmos"))
            {
                myScript.EnableDrawGizmosWaypoints();
            }
            if (GUILayout.Button("disable gizmos"))
            {
                myScript.DisableDrawGizmosWaypoints();
            }
            GUILayout.EndHorizontal();
        
        
            GUILayout.Space(20f); 
            GUILayout.Label("Generate and add new waypoint to List according to it's distance to the selected gameObject", EditorStyles.boldLabel);

            GUILayout.Space(10f);
            distance = EditorGUILayout.Vector3Field("Distance from object: ", distance);
        
            if (GUILayout.Button("Generate"))
            {
                GameObject selected = Selection.activeGameObject;

                if (selected != null)
                {
                    myScript.AddNewWayPoint(selected.transform.position+ distance);
                }
                else
                {
                    Debug.Log("Please make sure you are selecting a game object");
                }
           
            }
        
            GUILayout.Space(20f); 
            GUILayout.Label("Generate waypoint(s) between two waypoints and add them to list", EditorStyles.boldLabel);
        
            if (GUILayout.Button("Generate"))
            {
                GameObject[] selected = Selection.gameObjects;
                if (selected.Length != 2)
                {
                    Debug.Log("please select two Waypoints");
                }
                else
                {
                    WayPoint wayPoint1 = selected[0].GetComponent<WayPoint>();
                    WayPoint wayPoint2 = selected[1].GetComponent<WayPoint>();

                    if (wayPoint1 != null && wayPoint2 != null)
                    {
                        myScript.AddNewWayPoint(wayPoint1, wayPoint2);
                    }
                    else
                    {
                        Debug.Log("please make sure the selected objects are waypoints");
                    }
                
                }
           
            }
        
            /*

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Save")) //8
        {
            PlayerPrefs.SetString("PlayerName", playerName); //9
            PlayerPrefs.SetString("PlayerLevel", playerLevel);
            PlayerPrefs.SetString("PlayerElo", playerElo);
            PlayerPrefs.SetString("PlayerScore", playerScore);

            Debug.Log("PlayerPrefs Saved");
        }

        if (GUILayout.Button("Reset")) //10
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs Reset");
        }

        GUILayout.EndHorizontal();
        */
        
        }
    

    }
}

