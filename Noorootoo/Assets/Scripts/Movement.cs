﻿using UnityEngine;
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

    private bool isMoving;
    public Animator anim;

    public PlayerValues Player;
	private Quaternion desiredDirection;

	float idleSwitch = 0;
	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		Player.isGrounded = true;
        isMoving = false;
    }

	// Update is called once per frame
	void Update()
	{
		// use input manager
		IdleSwitch();
	}

	void FixedUpdate()
	{
        //-----Animation Code-----//
        if (Input.GetAxis(Joystick + "Vertical") < 0 || Input.GetAxis(Joystick + "Vertical") > 0 || Input.GetAxis(Joystick + "Horizontal") < 0 || Input.GetAxis(Joystick + "Horizontal") > 0)
            isMoving = true;
        else
            isMoving = false;

		anim.SetFloat("MovingX", Input.GetAxis(Joystick + "Horizontal"));
		anim.SetFloat("MovingZ", -Input.GetAxis(Joystick + "Vertical"));

		anim.SetBool("isMoving", isMoving);

		//-----Movement Code-----//

		Vector3 cameraForward = cam.transform.forward;
		cameraForward.y = 0.0f; cameraForward.Normalize();
		Vector3 cameraRight = Vector3.Cross(cameraForward, Vector3.up);

		Vector3 inputDirection = new Vector3(Input.GetAxis(Joystick + "Horizontal"), 0, -Input.GetAxis(Joystick + "Vertical"));

		if(inputDirection.magnitude > 0.0f)
		{
			inputDirection.Normalize();
			desiredDirection = Quaternion.LookRotation(inputDirection, Vector3.up);

			Quaternion CameraDirection = Quaternion.LookRotation(cameraForward, Vector3.up);
			desiredDirection = CameraDirection * desiredDirection;
		}


		if(Quaternion.Dot(desiredDirection, rb.transform.rotation) > 0)
		{
			Vector3 forwardOffset = cameraForward  * -Input.GetAxis(Joystick + "Vertical") * Player.m_Speed * Time.deltaTime;
			Vector3 rightOffset = cameraRight * -Input.GetAxis(Joystick + "Horizontal") * Player.m_Speed * Time.deltaTime;
			rb.MovePosition(rb.transform.position + forwardOffset + rightOffset);
		
			cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, rb.transform.rotation, Time.deltaTime);
		}

		rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, desiredDirection, Player.TurnSpeed * Time.deltaTime);

        //-----Jump Code -----//

		float secondsLeft = 0.3f;
		while (secondsLeft > 0)
		{
			if (Player.fallSpeed == 0 || (Player.isGrounded == true && Input.GetButton(Joystick + "Jump")))
			{
				Player.isGrounded = false;
				anim.SetBool("isGrounded", Player.isGrounded);
				anim.SetTrigger("JumpPressed");
				if (Player.isGrounded == false && transform.position.y < 4f)
					rb.AddForce(0, 6, 0, ForceMode.Impulse);
				else
					Player.fallSpeed = -4;
			}
			secondsLeft -= Time.deltaTime;

		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Ground")
		{
			Player.isGrounded = true;
			anim.SetBool("isGrounded", Player.isGrounded);
		}
	}
	
	void IdleSwitch()
	{
		if(isMoving == false)
		{
			if (idleSwitch > 0)
				idleSwitch -= Time.deltaTime;
			else if (idleSwitch <= 0)
			{
				idleSwitch = Random.Range(3.0f, 8.0f);
				anim.SetTrigger("IdleSwitch");

			}
		}
	}
};
