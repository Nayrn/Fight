using UnityEngine;
using System.Collections;

public class CameraThing : MonoBehaviour {

	public const float Y_CLAMP_MIN = 5.0f;
	public const float Y_CLAMP_MAX = 50.0f;

	public Transform m_PlayerOne;
	public Transform m_PlayerTwo;
	//public Transform m_PlayerThree;
	//public Transform m_PlayerFour;

	public JoystickNum Joystick = JoystickNum.Keyboard;

	public Transform lookAt;
	public Transform camTransform;

	private Camera cam;

	private float distance = 10.0f;
	private float xPos = 0.0f;
	private float yPos = 0.0f;
	public float xSensitivity = 4.0f;
	public float ySensitivity = 1.0f;

	private CursorLockMode lockMode;

	public float horizontalSpeed = 2.0f;


	// Use this for initialization
	void Start()
	{
		camTransform = transform;
		cam = Camera.main;

		lockMode = CursorLockMode.Locked;
	}

	void FixedUpdate()
	{
		SinglePlayer();

		if (Input.GetKeyDown(KeyCode.Z))
		{
			SwitchCursorMode();
		}

		Cursor.lockState = lockMode;

	}

	void LateUpdate()
	{
		Vector3 dir = new Vector3(0, 0, -distance);
		Quaternion rotation = Quaternion.Euler(yPos, xPos, 0);
		camTransform.position = lookAt.position + rotation * dir;

		camTransform.LookAt(lookAt.position);
	}

    void SinglePlayer()
    {
		if (Joystick == JoystickNum.Keyboard)
		{
			xPos += Input.GetAxis("Mouse X");
			yPos += Input.GetAxis("Mouse Y");
		}
		else
		{
			xPos += Input.GetAxis(Joystick + "CameraHorizontal") * xSensitivity;
			yPos += Input.GetAxis(Joystick + "CameraVertical") * ySensitivity;
		}

		yPos = Mathf.Clamp(yPos, Y_CLAMP_MIN, Y_CLAMP_MAX);
    }

    void SwitchCursorMode()
    {
        switch (Cursor.lockState)
        {
            case CursorLockMode.Locked:
                lockMode = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case CursorLockMode.None:
                lockMode = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
        }
    }

	void Multiplayer()
	{

	}
}
