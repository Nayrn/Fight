using UnityEngine;
using System.Collections;
// walk, run && jump
public enum JoystickNum
{
		Joystick1,
		Joystick2,
		Joystick3,
		Joystick4,
		Keyboard
};

public class Movement : MonoBehaviour
{
	public Rigidbody rb;
	public JoystickNum Joystick;
	private Camera cam;

	public bool isGrounded;
	public float speed;
	public float jumpSpeed;


	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		isGrounded = true;

		cam = Camera.main;
	}

	// Update is called once per frame
	void Update()
	{
		// use input manager
	}

	void FixedUpdate()
	{
		rb.velocity = new Vector3(Input.GetAxis(Joystick + "Horizontal") * speed, Physics.gravity.y, -Input.GetAxis(Joystick + "Vertical") * speed);

		float secondsLeft = 0.3f;
		while (secondsLeft > 0)
		{
			if (isGrounded == true)
			{
				if (Input.GetButton(Joystick + "Jump"))
				{
					rb.AddForce(new Vector3(0, 50, 0), ForceMode.Acceleration);

					if (transform.position.y > 2.0f)
					{
						isGrounded = false;

					}
				}
			}
			secondsLeft -= Time.deltaTime;
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Ground")
		{
			isGrounded = true;
		}
	}
};
