using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCapsule : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;

    public float jumpVelocity = 15f;

    public float distanceToGround = 0.01f;
    public LayerMask groundLayer;
    public GameObject bullet;
    public float bulletSpeed = 50f;
  
    private float vInput;
    private float hInput;
    private Rigidbody _rb;
    bool jump = false;
    private CapsuleCollider _col;

    private GameBehavior _gameManager;

    public delegate void JumpingEvent();
    public event JumpingEvent playerJump;
    void Start()
    {
         _rb = GetComponent<Rigidbody>();

        _col = GetComponent<CapsuleCollider>();

        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
     }

    void Update()
    {
         vInput = Input.GetAxis("Vertical") * moveSpeed;
         hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        /* 
        transform.Translate(Vector3.forward * vInput *
        Time.deltaTime);
        transform.Rotate(Vector3.up * hInput * Time.deltaTime);
        */
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            {
               GetComponent<AudioSource>().Play();
               jump = true;
               
            }
        Debug.Log("IsGrounded: " + IsGrounded());
        Debug.Log("Jumped:" + (IsGrounded() && Input.GetKeyDown(KeyCode.Space)));
        if (jump)
        {
            _rb.AddForce(Vector3.up * jumpVelocity,
                ForceMode.Impulse);

                playerJump();
            jump = false;
        }
            if (Input.GetMouseButtonDown(0))
            {
            GameObject newBullet = Instantiate(bullet,
            this.transform.position + new Vector3(0,0,1),
            this.transform.rotation) as GameObject;
            Rigidbody bulletRB =
                newBullet.GetComponent<Rigidbody>();
            
            bulletRB.velocity = this.transform.forward * bulletSpeed;
            }
            
           
     }
    void FixedUpdate() 
    {
         
        Vector3 rotation = Vector3.up * hInput;

        Quaternion angleRot = Quaternion.Euler(rotation *
            Time.fixedDeltaTime);

        _rb.MovePosition(this.transform.position +
        this.transform.forward * vInput * Time.fixedDeltaTime);

        _rb.MoveRotation(_rb.rotation * angleRot);
    }
    
    private bool IsGrounded()
    {
    // 7
    Vector3 capsuleBottom = new Vector3(_col.bounds.center.x,
        _col.bounds.min.y, _col.bounds.center.z);

    // 8
    bool grounded = Physics.CheckCapsule(_col.bounds.center,
       capsuleBottom, distanceToGround, groundLayer,
          QueryTriggerInteraction.Ignore);

    // 9
    return grounded;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy") 
        {
            _gameManager.HP -= 1;
        }
    }
}