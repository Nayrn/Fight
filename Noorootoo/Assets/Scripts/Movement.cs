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

    private bool isMoving;
    public Animator anim;
    public GameObject child;

    public PlayerValues Player;
	private float xRot = 0;

	private Quaternion desiredDirection;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		Player.isGrounded = true;
        anim = child.GetComponent<Animator>();
        isMoving = false;
    }

	// Update is called once per frame
	void Update()
	{
		// use input manager
	}

	void FixedUpdate()
	{
        //-----Animation Code-----//
        if (Input.GetAxis(Joystick + "Vertical") < 0 || Input.GetAxis(Joystick + "Vertical") > 0 || Input.GetAxis(Joystick + "Horizontal") < 0 || Input.GetAxis(Joystick + "Horizontal") > 0)
        {

            isMoving = true;
            anim.SetBool("isMoving", true);

        }
        else
        {
            isMoving = false;
            anim.SetBool("isMoving", false);
        }

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


		if(Quaternion.Dot(desiredDirection, rb.transform.rotation) > 0.5f)
		{
			Vector3 forwardOffset = cameraForward * -Input.GetAxis(Joystick + "Vertical") * Player.m_Speed * Time.deltaTime;
			Vector3 rightOffset = cameraRight * -Input.GetAxis(Joystick + "Horizontal") * Player.m_Speed * Time.deltaTime;
			rb.MovePosition(rb.transform.position + forwardOffset + rightOffset);
		}

		rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, desiredDirection, Player.TurnSpeed * Time.deltaTime);

        //-----Jump Code -----//

		float secondsLeft = 0.3f;
		while (secondsLeft > 0)
		{
			if (Player.fallSpeed == 0 || (Player.isGrounded == true && Input.GetButton(Joystick + "Jump")))
			{
				Player.isGrounded = false;
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
		}
	}
};
