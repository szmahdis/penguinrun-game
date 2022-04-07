using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemyTool
{
    public class EnemyCreator : MonoBehaviour
    {
        
       
        private GameObject enemy;
        //public Pattern wayPointsPattern;

      
        public float radius;
        public int pathLength;
        public GameObject wayPointPrefab;
        
        private List<WayPoint> wayPoints;
        private Transform centre;
        private GameObject wayPointsParent;
        
        public enum Pattern
        {
            rectangle,
            Line
        }

        public void Awake()
        {
           
        }

        public void ChangeModel(Enemy enemy)
        {
            if (this.enemy != null) DestroyImmediate(this.enemy);
            this.enemy = Instantiate(enemy.enemyModel, this.transform);
            centre = this.enemy.transform;

            if (wayPoints != null)
            {
                SetWaypoints();
            }
        }

        private void SetWaypoints()
        {
            enemy.GetComponent<Ghost>().wayPoints = wayPoints;
        }
        
        public void ChangePattern()
        {
            if (this.wayPointsParent != null)
            {
                Debug.Log("destroy");
                DestroyImmediate(this.wayPointsParent);
            }
            wayPointsParent = Instantiate(new GameObject(), this.transform) as GameObject;
            wayPoints = GenerateWaypoints();
            
            if(enemy!=null)
                SetWaypoints();
        }
        
        
        public List<WayPoint> GenerateWaypoints()
        {
            wayPoints = new List<WayPoint>();
            
            int num = pathLength;
            for (int i = 0; i < num; i++)
            {
                /* Distance around the circle */  
                var radians = 2 * Mathf.PI /  num* i;
         
                /* Get the vector direction */ 
                var vertical = Mathf.Sin(radians);
                var horizontal = Mathf.Cos(radians); 
         
                var spawnDir = new Vector3 (horizontal, 0, vertical);
         
                /* Get the spawn position */ 
                var spawnPos = centre.position + spawnDir * radius; // Radius is just the distance away from the point
         
                /* Now spawn */
                GameObject waypointGO = Instantiate(wayPointPrefab, spawnPos, Quaternion.identity);
                waypointGO.transform.SetParent(this.wayPointsParent.transform);
                WayPoint waypoint = waypointGO.GetComponent<WayPoint>();
                
                /* Rotate the enemy to face towards player */
                waypoint.transform.LookAt(centre.position);
         
                /* Adjust height */
                waypoint.transform.Translate (new Vector3 (0, waypoint.transform.localScale.y / 2, 0));
                
                wayPoints.Add(waypoint);
            }

            return wayPoints;
        }
   
    }
}

