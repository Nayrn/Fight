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
	public Camera cam;

	public bool isGrounded;
	public float speed;
	public float jumpSpeed;

	private float fallSpeed = -4;
	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		isGrounded = true;
	}

	// Update is called once per frame
	void Update()
	{
		// use input manager
	}

	void FixedUpdate()
	{
		rb.transform.position += cam.transform.forward * -Input.GetAxis(Joystick + "Vertical") * speed * Time.deltaTime;
		rb.transform.position += cam.transform.right * Input.GetAxis(Joystick + "Horizontal") * speed * Time.deltaTime;

		float secondsLeft = 0.3f;
		while (secondsLeft > 0)
		{
			if (fallSpeed == 0 || (isGrounded == true && Input.GetButtonDown(Joystick + "Jump")))
			{
				isGrounded = false;
				if (isGrounded == false && transform.position.y < 4f)
				{
					rb.transform.position += Vector3.up * Time.deltaTime;
					fallSpeed = 0;
				}
				else
					fallSpeed = -4;
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
