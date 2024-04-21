using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickUp : MonoBehaviour
{   public GameBehavior gameManager;
    //1
    public GameObject player;
    void OnCollisionEnter(Collision collision)
    {
        //2
        if (collision.gameObject.name == "Player")
        {
            //3
            Destroy(this.transform.parent.gameObject);

            //4
            Debug.Log("Hammer Get!");
           gameManager.PrintLootReport();
            
        }
        
 // 4
    
    }
   
}
