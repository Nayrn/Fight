using UnityEngine;
using System.Collections;

public enum GameMode
{
	SinglePlayer,
	MultiPlayer
};

public class CameraThing : MonoBehaviour {

	public JoystickNum Joystick = JoystickNum.Keyboard;
	private float AspectRatio;
	public GameObject Player;

	public GameMode Mode;

	//-----SinglePlayer Camera Variables-----//
	public const float Y_CLAMP_MIN = 5.0f;
	public const float Y_CLAMP_MAX = 50.0f;

	private float xPos = 0.0f;
	private float yPos = 0.0f;
	public float xSensitivity = 4.0f;
	public float ySensitivity = 1.0f;

	//-----SinglePlayer Target variables-----//

	public const float MIN_CAMERA_DISTANCE = 3.0f;
	public const float MAX_CAMERA_DISTANCE = 10.0f;

	public Transform TargetObject;

	private Vector3 TargetPosition;

	//---------------------------------------//
	//-----MultiPlayer Camera Variables------//

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

	private Transform lookAt;
	private Transform camTransform;

	public Camera cam;

	private float distance = 10.0f;

	// Use this for initialization
	void Start()
	{
		Mode = GameMode.SinglePlayer;
		camTransform = transform;

		lookAt = Player.transform;

		AspectRatio = Screen.width / Screen.height;
		TanFOV = Mathf.Tan(Mathf.Deg2Rad * cam.fieldOfView / 2.0f);
	}

	void FixedUpdate()
	{
		switch(Mode)
		{
			case GameMode.SinglePlayer:
				SinglePlayer();
				break;
			case GameMode.MultiPlayer:
				MultiPlayer();
				break;
			default:
				MultiPlayer();
				break;
		}

	}

	void LateUpdate()
	{
		if (Mode == GameMode.SinglePlayer)
		{
			float tempFloat = cam.transform.position.y;

			Vector3 dir = new Vector3(0, 0, -distance);
			Quaternion rotation = Quaternion.Euler(yPos, xPos, 0);
			camTransform.position = lookAt.position + rotation * dir;

			if (Player.GetComponent<PlayerValues>().isGrounded == true)
			{
				tempFloat = cam.transform.position.y;
			}

			//camTransform.position = new Vector3(camTransform.position.x, tempFloat, camTransform.position.z);\
			TargetPosition = (Player.transform.position + TargetObject.transform.position) / 2;
			//camTransform.position = Vector3.Lerp(camTransform.position, Player.transform.position + TargetPosition.normalized * distance, Time.deltaTime);
			camTransform.LookAt(Player.transform.position);
		}
	}

    void SinglePlayer()
    {
		if (Joystick == JoystickNum.Keyboard)
		{
			xPos += Input.GetAxis("Mouse X") * xSensitivity;
			yPos += Input.GetAxis("Mouse Y") * ySensitivity;
		}
		else
		{
			xPos += Input.GetAxis(Joystick + "CameraHorizontal") * xSensitivity;
			yPos += Input.GetAxis(Joystick + "CameraVertical") * ySensitivity;
		}

		xPos = Mathf.Clamp(xPos, 45, 45);
		yPos = Mathf.Clamp(yPos, Y_CLAMP_MIN, Y_CLAMP_MAX);
    }
	void MultiPlayer()
	{

	}
}
