using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[SelectionBase]

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public Transform patrolRoute;
    
    

    public List<Transform> locations;
    private int locationIndex = 0;
    private NavMeshAgent agent;

     private int _lives = 3;
     public int EnemyLives
     {
        get {return _lives;}
        private set
        {
            _lives = value;

            if (_lives <= 0)
            {
                
                Destroy(this.gameObject);
                Debug.Log("Enemy down.");
            }
        }
     }
     void Start()
     {
         agent = GetComponent<NavMeshAgent>();
         
        player = GameObject.Find("Player").transform;
         
        InitializePatrolRoute();

         MoveToNextPatrolLocation();
     }

void Update()
     {
        if(agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
     }
 // 4
 void MoveToNextPatrolLocation()
     {
        if (locations.Count == 0)
        
            return;
        
            agent.destination = locations[locationIndex].position;
            
            locationIndex = (locationIndex + 1) % locations.Count;
        
     }
     // 3
    
 void InitializePatrolRoute()
     {
         // 5
         foreach(Transform child in patrolRoute)
         {
             // 6
             locations.Add(child);
        }
    }
     // 1
     void OnTriggerEnter(Collider other)
     {
         //2
         if(other.name == "Player")
         {
            agent.destination = player.position;
             
            Debug.Log("Player detected - attack!");
         }
     }
void OnTriggerExit(Collider other)
     {
        
         if(other.name == "Player")
         {
             Debug.Log("Player out of range, resume patrol");
         }
     }

void OnCollisionEnter(Collision collision)
     {
        if(collision.gameObject.name == "Bullet(Clone)")
        {
            GetComponent<AudioSource>().Play();
            EnemyLives -= 1;
            Debug.Log("Critical hit!");
             
             
        }
     }
     
}