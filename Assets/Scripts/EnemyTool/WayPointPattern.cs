using System.Collections.Generic;
using UnityEngine;

namespace EnemyTool
{
    public abstract class WayPointPattern : MonoBehaviour
    {

        public List<WayPoint> wayPoints;
        public Transform centre;
        public GameObject wayPointPrefab;
        public abstract List<WayPoint> GenerateWaypoints();

    }
}
