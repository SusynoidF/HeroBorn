using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{   public GameBehavior gameManager;
    
     // 1
    
     void Start()
     {
           // 2
           gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
     }
     void OnCollisionEnter(Collision collision)
     { 
        if (collision.gameObject.name == "Player")
         {
             Destroy(this.transform.parent.gameObject);
             Debug.Log("Item collected!");
             
             gameManager.Items += 1;
        }
    }
}
