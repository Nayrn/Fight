using UnityEngine;
using System.Collections;
// walk, run && jump
public enum JoystickNum
{
		Joystick1,
		Joystick2,
		Joystick3,
		Joystick4,
		Keyboard, 
        AI
};

public class Movement : MonoBehaviour
{
	public PlayerValues Player;

	public Camera cam;

    private bool isMoving;
	private Quaternion desiredDirection;
    private int jumpAmount;
	float idleSwitch = 0;
	// Use this for initialization
	void Start()
	{
		Player.isGrounded = true;
        isMoving = false;
        jumpAmount = 0;
    }

	// Update is called once per frame
	void Update()
	{
		// use input manager
		IdleSwitch();
	}

	void FixedUpdate()
	{
		if (!Player.isStasis)
		{
			MoveCode();

			if (Input.GetButtonDown(Player.Joystick + "Jump"))
			{
				if (!Player.isGrounded && Player.DoubleJump && !Player.isAttacking)
				{
					//Update to only activate on Normal Jump loop

					Player.DoubleJump = false;
					Player.PlayerAnimation.SetTrigger("DoubleJump");

					Player.rb.velocity = new Vector3(Player.rb.velocity.x, 0, Player.rb.velocity.z);
					Player.rb.AddForce(0, 8, 0, ForceMode.Impulse);
				}
				else if (Player.isGrounded == true)
				{
					Player.rb.AddForce(0, 8, 0, ForceMode.Impulse);

					Player.isGrounded = false;
					Player.PlayerAnimation.SetBool("isGrounded", Player.isGrounded);
					Player.PlayerAnimation.SetTrigger("JumpPressed");
				}
			}
		}
		else
		{
			transform.position = transform.position;
		}

        if (Player.isGrounded)
            jumpAmount = 0;
        


	}

	void MoveCode()
	{
		//-----Animation Code-----//
		if ((Input.GetAxis(Player.Joystick + "Vertical") < 0 || Input.GetAxis(Player.Joystick + "Vertical") > 0 || Input.GetAxis(Player.Joystick + "Horizontal") < 0 || Input.GetAxis(Player.Joystick + "Horizontal") > 0))
			isMoving = true;
		else
			isMoving = false;


		if (Player.Targeted)
		{
			Player.PlayerAnimation.SetFloat("MovingX", Input.GetAxis(Player.Joystick + "Horizontal"));
			Player.PlayerAnimation.SetFloat("MovingZ", -Input.GetAxis(Player.Joystick + "Vertical"));
		}
		else
		{
			Player.PlayerAnimation.SetFloat("MovingX", Mathf.Abs(Input.GetAxis(Player.Joystick + "Horizontal")));
			Player.PlayerAnimation.SetFloat("MovingZ", Mathf.Abs(Input.GetAxis(Player.Joystick + "Vertical")));
		}

		Player.PlayerAnimation.SetBool("isMoving", isMoving);

		//-----FREE MOVEMENT CODE-----//

		Vector3 cameraForward = cam.transform.forward;
		cameraForward.y = 0.0f; cameraForward.Normalize();
		Vector3 cameraRight = Vector3.Cross(cameraForward, Vector3.up);

		Vector3 inputDirection = new Vector3(Input.GetAxis(Player.Joystick + "Horizontal"), 0, -Input.GetAxis(Player.Joystick + "Vertical"));

		if (inputDirection.magnitude > 0.1f)
		{
			inputDirection.Normalize();
			desiredDirection = Quaternion.LookRotation(inputDirection, Vector3.up);

			Quaternion CameraDirection = Quaternion.LookRotation(cameraForward, Vector3.up);
			desiredDirection = CameraDirection * desiredDirection;


			Vector3 forwardOffset = cameraForward * -Input.GetAxis(Player.Joystick + "Vertical") * Player.m_Speed * Time.deltaTime;
			Vector3 rightOffset = cameraRight * -Input.GetAxis(Player.Joystick + "Horizontal") * Player.m_Speed * Time.deltaTime;

			//-----Move Player in the direction of the joystick input
			Player.rb.MovePosition(Player.rb.transform.position + forwardOffset + rightOffset);

			//-----Rotate Player towards desired Joystick direction
			Player.rb.transform.rotation = Quaternion.RotateTowards(Player.rb.transform.rotation, desiredDirection, Player.TurnSpeed * Time.deltaTime);
		}

		////-----Force Player to look at opponent if Targeted
		//if (Player.Targeted)
		//	Player.rb.transform.LookAt(new Vector3(Player.Opponent.transform.position.x, transform.position.y, Player.Opponent.transform.position.z));

		if (Physics.Raycast(transform.position, Vector3.down, 3) == false && Player.isGrounded == true)
		{
			Player.isGrounded = false;
			Player.PlayerAnimation.SetBool("isGrounded", false);

			Player.PlayerAnimation.SetTrigger("FallTrigger");
		}

        if(Player.Joystick == JoystickNum.AI)
        {
            isMoving = true;
        }
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Ground")
			Player.PlayerAnimation.SetBool("isGrounded", true);
	}
	//-----Idle ear twitch. Fully completed.
	void IdleSwitch()
	{
		if(isMoving == false)
		{
			if (idleSwitch > 0)
				idleSwitch -= Time.deltaTime;
			else if (idleSwitch <= 0)
			{
				idleSwitch = Random.Range(3.0f, 8.0f);
				Player.PlayerAnimation.SetTrigger("IdleSwitch");

			}
		}
	}
};
