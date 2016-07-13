using UnityEngine;
using System.Collections;
// walk, run && jump
public class Movement : MonoBehaviour
{
    public Rigidbody rb;

    public bool isGrounded;
    public float speed;
    
   
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
     
    }
	
	// Update is called once per frame
	void Update ()
    {
    
        // use input manager
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, 0.0f, Input.GetAxis("Vertical") * speed);

    }
}
