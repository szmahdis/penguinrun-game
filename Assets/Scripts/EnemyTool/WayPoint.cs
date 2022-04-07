using UnityEngine;

namespace EnemyTool
{
    public class WayPoint : MonoBehaviour
    {
        public float radius;
        public bool drawGizmos = true;
    
        public void OnDrawGizmos() {

            if (drawGizmos)
            {
                if (radius == 0.0f) radius = 0.2f;
                Gizmos.DrawSphere(gameObject.transform.position,radius);
            }
        
        }
    }
}
