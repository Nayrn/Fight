using UnityEngine;
using System.Collections;
// walk, run && jump
public enum JoystickNum
{
		Joystick1,
		Joystick2,
		Joystick3,
		Joystick4,
		Joystick0
};

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
	public JoystickNum Joystick;
	private Camera cam;

    public bool isGrounded;
    public float speed;
    
   
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();

		cam = Camera.main;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
        // use input manager
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(Input.GetAxis(Joystick + "Horizontal") * speed, 0.0f, Input.GetAxis(Joystick + "Vertical") * speed);
	}
}
