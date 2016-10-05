using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour {

	private PlayerValues Player;

	private float xOffsetMax = 50;
	private float xOffsetMin = -50;
	private float yOffsetMax = 18;
	private float yOffsetMin = -0.5f;

	private float yOffset = 9;
	private float xOffset = -7;

    float freeViewX = 0.0f;
    float freeViewY = 0.0f;
    Vector3 direction = new Vector3(0, 0, -5);

    public GameObject FollowedObject;
	public GameObject TrackedObject;
	public GameManager Game;
	private Transform CameraPivot;

	public Vector3 CameraOffset = new Vector3();
	private Vector3 TargetOffset = new Vector3();
	private Vector3 FreeOffset = new Vector3();

	public Vector3 Sensitivity = new Vector3(4.0f, 3.0f, 1.0f);
	// Use this for initialization
	void Start ()
    {
		CameraPivot = transform.parent;
		UpdatePivotRotation();

		transform.position = FollowedObject.transform.forward.normalized + direction;

		Player = FollowedObject.GetComponent<PlayerValues>();
	}

	void Update()
	{
     
   
    }

	// Update is called once per frame
	void LateUpdate()
	{
		//rotation + position lerp
		CameraPivot.transform.position = FollowedObject.transform.position;// Vector3.Lerp(CameraPivot.transform.position, FollowedObject.transform.position, Time.deltaTime * 5);
		CameraPivot.transform.LookAt(TrackedObject.transform.position);
        //LOCK ON CAMERA BYE BYE
		if (Input.GetButtonDown(Player.Joystick + "LockOn"))
		{
			Player.Targeted = !Player.Targeted;
			Player.PlayerAnimation.SetBool("isTargeted", Player.Targeted);
		}
		//-----Swaps views if targeted is changed-----//
		if (Player.Targeted == true)
		{
			TargetView();
		}
		else
			FreeView();
	}

	private void UpdatePivotRotation()
	{
        Vector3 newPos = (FollowedObject.transform.position - TrackedObject.transform.position).normalized * 5;

        newPos = new Vector3(newPos.x, newPos.y + 2, newPos.z);
        transform.position = CameraPivot.transform.position + newPos;

		Vector3 dir = TrackedObject.transform.position - FollowedObject.transform.position;
		dir.y = 0.0f;
		//dir.Normalize();

		Quaternion rot = Quaternion.LookRotation(dir);

		Quaternion offsetRot = Quaternion.AngleAxis(CameraOffset.x, Vector3.up);
		offsetRot *= Quaternion.AngleAxis(CameraOffset.y, Vector3.right);

		transform.rotation = Quaternion.Lerp(transform.rotation, rot * offsetRot, Time.deltaTime * 1.0f);
	}

	void TargetView()
	{
		if (Player.Joystick == JoystickNum.Keyboard)
		{
			CameraOffset.x += Input.GetAxis("Mouse X") * Sensitivity.x;
			CameraOffset.y += Input.GetAxis("Mouse Y") * Sensitivity.y;
		}
		else
		{
			CameraOffset.x += Input.GetAxis(Player.Joystick + "CameraHorizontal") * Sensitivity.x;
			CameraOffset.y += Input.GetAxis(Player.Joystick + "CameraVertical") * Sensitivity.y;
		}

		CameraOffset.x = Mathf.Clamp(CameraOffset.x, xOffsetMin, xOffsetMax);
		CameraOffset.y = Mathf.Clamp(CameraOffset.y, yOffsetMin, yOffsetMax);


		UpdatePivotRotation();

		//Camera lock on based on distance
		Vector3 myPosition = Player.transform.position;
		Vector3 theirPosition = TrackedObject.transform.position;

		Vector3 Midpoint = new Vector3();
		Vector3 MidpointDirection = new Vector3();

		float distance = Vector3.Distance(myPosition, theirPosition);
		Vector3 PlayerDir = theirPosition - myPosition;
        // MIDPOINT CAMERA BYE BYE
		
			Midpoint = myPosition + (PlayerDir * (distance * 0.5f));
			MidpointDirection = ((FollowedObject.transform.position + Midpoint) - transform.position).normalized;
		

		//Quaternion lookAt = Quaternion.LookRotation(MidpointDirection);
		//transform.rotation = Quaternion.Slerp(transform.rotation, lookAt, Time.deltaTime * 5.0f);
		transform.LookAt(Midpoint, Vector3.up);
	}
	void FreeView()
	{
        
		if (Player.Joystick == JoystickNum.Keyboard)
		{
            freeViewX += Input.GetAxis("Mouse X") * Sensitivity.x;
            freeViewY += Input.GetAxis("Mouse Y") * Sensitivity.y;
		}
		else
		{
            freeViewX += Input.GetAxis(Player.Joystick + "CameraHorizontal") * Sensitivity.x;
            freeViewY += Input.GetAxis(Player.Joystick + "CameraVertical") * Sensitivity.y;
        }

        freeViewY = Mathf.Clamp(freeViewY, 4, 55);

        Quaternion rotation = Quaternion.Euler(freeViewY, freeViewX, 0);
        transform.position = FollowedObject.transform.position + rotation * direction;
		
		//-----Lookat Player position
		transform.LookAt(FollowedObject.transform.position);
	}
}
