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
	public PlayerValues Player;

	public Rigidbody rb;
	public Camera cam;

    private bool isMoving;
	private Quaternion desiredDirection;

	float idleSwitch = 0;
	// Use this for initialization
	void Start()
	{
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
		if (!Player.isStasis)
		{
			//-----Animation Code-----//
			if ((Input.GetAxis(Player.Joystick + "Vertical") < 0 || Input.GetAxis(Player.Joystick + "Vertical") > 0 || Input.GetAxis(Player.Joystick + "Horizontal") < 0 || Input.GetAxis(Player.Joystick + "Horizontal") > 0) && Player.isGrounded)
			{
				isMoving = true;

				Player.PlayerAnimation.SetLayerWeight(1, 1);
			}
			else
			{
				isMoving = false;
				Player.PlayerAnimation.SetLayerWeight(1, 0);
			}

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

			//-----Movement Code-----//

			Vector3 cameraForward = cam.transform.forward;
			cameraForward.y = 0.0f; cameraForward.Normalize();
			Vector3 cameraRight = Vector3.Cross(cameraForward, Vector3.up);

			Vector3 inputDirection = new Vector3(Input.GetAxis(Player.Joystick + "Horizontal"), 0, -Input.GetAxis(Player.Joystick + "Vertical"));

			if (inputDirection.magnitude > 0.0f)
			{
				inputDirection.Normalize();
				desiredDirection = Quaternion.LookRotation(inputDirection, Vector3.up);

				Quaternion CameraDirection = Quaternion.LookRotation(cameraForward, Vector3.up);
				desiredDirection = CameraDirection * desiredDirection;

				Vector3 forwardOffset = cameraForward * -Input.GetAxis(Player.Joystick + "Vertical") * Player.m_Speed * Time.deltaTime;
				Vector3 rightOffset = cameraRight * -Input.GetAxis(Player.Joystick + "Horizontal") * Player.m_Speed * Time.deltaTime;


				rb.MovePosition(rb.transform.position + forwardOffset + rightOffset);

				rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, desiredDirection, Player.TurnSpeed * Time.deltaTime);

				if(Player.Targeted)
				{
					rb.transform.LookAt(new Vector3(Player.Opponent.transform.position.x, transform.position.y, Player.Opponent.transform.position.z));
				}
			}

			//-----Jump Code -----//

			//float secondsLeft = 0.3f;
			//while (secondsLeft > 0)
			//{
			if ((Player.isGrounded == true && Input.GetButton(Player.Joystick + "Jump")))
			{
				Player.PlayerAnimation.SetLayerWeight(1, 0);
				Player.PlayerAnimation.SetLayerWeight(2, 0);
				Player.isGrounded = false;
				Player.PlayerAnimation.SetBool("isGrounded", Player.isGrounded);
				Player.PlayerAnimation.SetTrigger("JumpPressed");
				if (Player.isGrounded == false)
					rb.AddForce(0, 9, 0, ForceMode.Impulse);
			}
			//secondsLeft -= Time.deltaTime;

			//}

			if (Physics.Raycast(transform.position, Vector3.down, 3) == false && Player.isGrounded == true)
			{
				Player.isGrounded = false;
				Player.PlayerAnimation.SetTrigger("FallTrigger");
			}
			else if(Physics.Raycast(transform.position, Vector3.down, 0.5f) == true)
				Player.PlayerAnimation.SetBool("isGrounded", true);
		}
		else
		{
			transform.position = transform.position;
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Ground")
			Player.PlayerAnimation.SetBool("isGrounded", true);
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
				Player.PlayerAnimation.SetTrigger("IdleSwitch");

			}
		}
	}
};
