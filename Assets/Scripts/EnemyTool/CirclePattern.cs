using System.Collections.Generic;
using UnityEngine;

namespace EnemyTool
{
    public class CirclePattern : WayPointPattern
    {

        public float radius;
        public int pathLength;
    
        public override List<WayPoint> GenerateWaypoints()
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
                WayPoint waypoint = Instantiate(wayPointPrefab, spawnPos, Quaternion.identity).GetComponent<WayPoint>();
         
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
