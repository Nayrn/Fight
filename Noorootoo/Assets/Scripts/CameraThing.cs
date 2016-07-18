using UnityEngine;
using System.Collections;

public class CameraThing : MonoBehaviour {

	public JoystickNum Joystick = JoystickNum.Keyboard;
	private CursorLockMode lockMode;
	private float AspectRatio;

	//-----SinglePlayer Camera Variables-----//
	public const float Y_CLAMP_MIN = 5.0f;
	public const float Y_CLAMP_MAX = 50.0f;

	private float xPos = 0.0f;
	private float yPos = 0.0f;
	public float xSensitivity = 4.0f;
	public float ySensitivity = 1.0f;
	//---------------------------------------//
	//-----MultiPlayer Camera Variables------//

	public Transform m_PlayerOne;
	public Transform m_PlayerTwo;
	//public Transform m_PlayerThree;
	//public Transform m_PlayerFour;

	public const float SCREEN_MARGIN = 1.0f;

	private Vector3 middlePoint;
	//Distance between the player and the midpoint
	private float m_MPDist;
	//Distance between the 2 players.
	private float m_PlayerDistance;
	//Distance between the camera and the midpoint
	private float m_CameraDist;

	private float FOV;
	private float TanFOV;


	//---------------------------------------//

	public Transform lookAt;
	public Transform camTransform;

	public Camera cam;

	private float distance = 10.0f;

	// Use this for initialization
	void Start()
	{
		camTransform = transform;

		AspectRatio = Screen.width / Screen.height;
		TanFOV = Mathf.Tan(Mathf.Deg2Rad * cam.fieldOfView / 2.0f);

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
		float tempFloat = cam.transform.position.y;
		Vector3 dir = new Vector3(0, 0, -distance);
		Quaternion rotation = Quaternion.Euler(yPos, xPos, 0);
		camTransform.position = lookAt.position + rotation * dir;
		camTransform.position = new Vector3(camTransform.position.x, tempFloat, camTransform.position.z);
		
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
	void Multiplayer()
	{
		Vector3 tempVec = m_PlayerTwo.position - m_PlayerOne.position;
		middlePoint = m_PlayerOne.position + 0.5f * tempVec;

		Vector3 tempPos = cam.transform.position;
		tempPos.x = middlePoint.x;
		cam.transform.position = tempPos;

		m_PlayerDistance = tempVec.magnitude;
		m_CameraDist = (m_PlayerDistance / 2.0f / AspectRatio) / TanFOV;

		Vector3 direction = (cam.transform.position - middlePoint).normalized;
		cam.transform.position = middlePoint + direction * (m_CameraDist + SCREEN_MARGIN);
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
}
