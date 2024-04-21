using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public PlayerCapsule player;
    //1
    void OnCollisionEnter (Collision collision)
    { 
        //2
        if (collision.gameObject.name == "Player")
        {
            //3
            Destroy(this.transform.parent.gameObject);

            speedBoost();
            

           
            
            //4
        }
    }
    void speedBoost()
    {
        player.moveSpeed *= 2;
        Debug.Log("SpeedBoost!");

    }
    void endspeedBoost()
    {
        player.moveSpeed /= 2;
        Debug.Log("SpeedBoostOver...");
    }
}
