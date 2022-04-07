using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyTool
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class Ghost : MonoBehaviour
    {
        [SerializeField]
        private float damage;
        [SerializeField]
        private float ChaseSpeed;
        [SerializeField]
        private float NormalSpeed;
        [SerializeField]
        private GameObject Prey;
    
        private Rigidbody enemyRigidbody;
        private Animator _animator;

        private Health _health;
        private ThirdPersonController _thirdPersonController;
        public bool freeze;
        public enum Behaviour
        {
            LineOfSight,
            Intercept, 
            PatternMovement,
            ChasePatternMovement,
            Hide,
            PatternMovementNavMesh,
            ChasePatternMovementNavMesh
        }

        public Behaviour behaviour;
    
        private int currentWayPoint = 0;
        [SerializeField]
        private float distanceThreshold;

        [SerializeField]
        private float ChaseEvadeDistance;

        [SerializeField]
        private GameObject wayPointPrefab;
        [SerializeField] 
        public List<WayPoint> wayPoints;
        
        private NavMeshAgent agent;
        void Awake()
        {
            if (damage == 0.0f) damage = 0.5f;
            if (ChaseSpeed == 0.0f) ChaseSpeed = 6.0f;
            if (NormalSpeed == 0.0f) NormalSpeed = 3.0f;
            if(Prey == null) Prey =  GameObject.FindWithTag("Player");
            if (_thirdPersonController == null) _thirdPersonController = Prey.GetComponent<ThirdPersonController>();
            behaviour = Behaviour.ChasePatternMovementNavMesh;
            if (ChaseEvadeDistance == 0.0f) ChaseEvadeDistance = 10f;
            if (distanceThreshold == 0.0f) distanceThreshold = 0.1f;

            _health = FindObjectOfType<Health>();
            _animator = GetComponent<Animator>(); 
            enemyRigidbody = GetComponent<Rigidbody>();	
            agent = GetComponent<NavMeshAgent>();
            agent.autoBraking = false;
            agent.destination = wayPoints[currentWayPoint].transform.position;
            agent.speed = NormalSpeed;
    
        }
    
        void FixedUpdate()
        {

        
        
            switch (behaviour)
            {
                case Behaviour.LineOfSight:
                    ChaseLineOfSight(Prey.transform.position, ChaseSpeed);
                    break;
                case Behaviour.Intercept:
                    Intercept(Prey.transform.position);
                    break;
                case Behaviour.PatternMovement:
                    PatternMovement();
                    break;
                case Behaviour.ChasePatternMovement:
                    if (Vector3.Distance(gameObject.transform.position, Prey.transform.position) < ChaseEvadeDistance)
                    {
                        ChaseLineOfSight(Prey.transform.position, ChaseSpeed);
                    }
                    else
                    {
                        PatternMovement();
                    }

                    break;
                case Behaviour.Hide:
                    if (PlayerVisible(Prey.transform.position))
                    {
                        ChaseLineOfSight(Prey.transform.position, ChaseSpeed);
                    }
                    else
                    {
                        PatternMovement();
                    }

                    break;
                case Behaviour.PatternMovementNavMesh:
                    if (!agent.pathPending && agent.remainingDistance < 0.5f)
                        NavigateToNextPoint();
                    break;

                case Behaviour.ChasePatternMovementNavMesh:
                    if (!freeze)
                    {
                        if (PlayerVisible(Prey.transform.position) && (Vector3.Distance(gameObject.transform.position, Prey.transform.position) < ChaseEvadeDistance))
                        {
                            agent.speed = ChaseSpeed;
                            agent.destination = Prey.transform.position;
                        }
                        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
                        {
                            agent.speed = NormalSpeed;
                            NavigateToNextPoint();
                        }  
                    }
                    else
                    {
                        ChaseLineOfSight(Prey.transform.position, -NormalSpeed);
                    }
                
                    break;
                default:
                    break;
            }
        }

        private void ChaseLineOfSight(Vector3 targetPosition, float Speed){
            Vector3 direction = targetPosition - transform.position;
            direction.Normalize();

            enemyRigidbody.velocity = new Vector3(direction.x * Speed, enemyRigidbody.velocity.y, direction.z * Speed);
        }

        private void Intercept(Vector3 targetPosition){
            Vector3 enemyPosition = gameObject.transform.position;
        
            Vector3 VelocityRelative, Distance, PredictedInterceptionPoint;
            float timeToClose;

            VelocityRelative = Prey.GetComponent<Rigidbody>().velocity - enemyRigidbody.velocity;
            Distance = targetPosition - enemyPosition;

            timeToClose = Distance.magnitude / VelocityRelative.magnitude;

            PredictedInterceptionPoint = targetPosition + (timeToClose * Prey.GetComponent<Rigidbody>().velocity);

            Vector3 direction = PredictedInterceptionPoint - enemyPosition;
            direction.Normalize();
            enemyRigidbody.velocity = new Vector3(direction.x * ChaseSpeed, enemyRigidbody.velocity.y, direction.z * ChaseSpeed);


        }

        private void PatternMovement(){
            //Move towards the current waypoint. 
            ChaseLineOfSight(wayPoints[currentWayPoint].transform.position, NormalSpeed);

            //Check if we are close to the next waypoint and incerement to the next waypoint. 
            if(Vector3.Distance(gameObject.transform.position, wayPoints[currentWayPoint].transform.position) 
               < distanceThreshold){
                currentWayPoint = (currentWayPoint + 1) % wayPoints.Count; //modulo to restart at the beginning. 
            }


        }

        private bool PlayerVisible(Vector3 targetPosition){
            Vector3 directionToTarget = targetPosition - gameObject.transform.position;
            directionToTarget.Normalize();

            RaycastHit hit;
            Physics.Raycast(gameObject.transform.position, directionToTarget, out hit);

        
            return hit.collider.gameObject.tag.Equals("Player");

        
        }
     
        private void NavigateToNextPoint(){
        
            currentWayPoint = (currentWayPoint + 1) % wayPoints.Count;
            //Move towards the current waypoint. 
            if (wayPoints[currentWayPoint] == null) wayPoints.Remove(wayPoints[currentWayPoint]);
            else
            {
                agent.destination = wayPoints[currentWayPoint].transform.position;
            }
       

        
        }
    

        [ContextMenu("Enable Waypoint Gizmos")]
        public void EnableDrawGizmosWaypoints()
        {
            foreach(WayPoint waypoint in wayPoints)
            {
                waypoint.drawGizmos = true;
            }
        }
    
        [ContextMenu("Disable Waypoint Gizmos")]
        public void DisableDrawGizmosWaypoints()
        {
            foreach(WayPoint waypoint in wayPoints)
            {
                waypoint.drawGizmos = false;
            }
        }
    
        public void AddNewWayPoint(Vector3 distance)
        {
            GameObject waypointGameObject = Instantiate(wayPointPrefab, distance, Quaternion.identity);
            WayPoint waypoint = waypointGameObject.GetComponent<WayPoint>();
            wayPoints.Add(waypoint);
        }

        public void AddNewWayPoint(WayPoint wayPoint1, WayPoint wayPoint2)
        {
            GameObject waypointGameObject = Instantiate(wayPointPrefab, Vector3.Lerp(wayPoint1.transform.position, wayPoint2.transform.position, 0.5f), Quaternion.identity);
            WayPoint waypoint = waypointGameObject.GetComponent<WayPoint>();
            wayPoints.Insert(wayPoints.IndexOf(wayPoint1), waypoint);
        
        }

        public void RemoveEmptyWayPoints()
        {
            wayPoints.RemoveAll(item => item == null);

        }
    
    
        private void OnTriggerEnter(Collider other)
        {
            if (!freeze)
            {
                if (other.gameObject.tag == "PenguinBody")
                {

                    _thirdPersonController.enemy = this;
                    if (_thirdPersonController.Grounded)
                    {
                        _animator.SetTrigger("die");
                        _health.TakeDamage(damage);
                        _thirdPersonController.Stun();   
                    }
                    else
                    {
                        _animator.SetTrigger("die");
                        _thirdPersonController.JumpAfterEnemy();
                        Destroy(this.gameObject);
                    }
           
                }
            }
        
        
        }
    }
}
    
    