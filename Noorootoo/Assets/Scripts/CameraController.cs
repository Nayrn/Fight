using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour {

	private const float X_OFFSET_MAX = 50;
	private const float X_OFFSET_MIN = -50;
	private const float Y_OFFSET_MAX = 18;
	private const float Y_OFFSET_MIN = -5;
	private const float Z_OFFSET_MAX = 10.0f;
	private const float Z_OFFSET_MIN = 5.0f;

	public GameObject FollowedObject;
	public GameObject TrackedObject;
	public GameManager Game;

	private bool Targeted = true;

	public JoystickNum Joystick = JoystickNum.Keyboard;
	private Transform CameraPivot;

	public Vector3 CameraOffset = new Vector3();
	private Vector3 TargetOffset = new Vector3();
	private Vector3 FreeOffset = new Vector3();

	public Vector3 Sensitivity = new Vector3(4.0f, 3.0f, 1.0f);
	// Use this for initialization
	void Start () {
		CameraPivot = transform.parent;
		UpdatePivotRotation();
	}



	// Update is called once per frame
	void Update () {
		//rotation + position lerp
		CameraPivot.transform.position = Vector3.Lerp(CameraPivot.transform.position, FollowedObject.transform.position, Time.deltaTime * 5);

		if (Targeted == true)
		{
			TargetView();
			CameraOffset = Vector3.Lerp(CameraOffset, TargetOffset, Time.deltaTime * 10);
		}
		else
		{
			FreeView();
			CameraOffset = Vector3.Lerp(CameraOffset, FreeOffset, Time.deltaTime * 10);
		}
	}

	private void UpdatePivotRotation()
	{
		Vector3 dir = TrackedObject.transform.position - FollowedObject.transform.position;
		dir.y = 0.0f;
		dir.Normalize();

		Quaternion rot = Quaternion.LookRotation(dir);

		Quaternion offsetRot = Quaternion.AngleAxis(CameraOffset.x, Vector3.up);
		offsetRot *= Quaternion.AngleAxis(CameraOffset.y, Vector3.right);
		offsetRot *= Quaternion.AngleAxis(CameraOffset.z, Vector3.forward);

		CameraPivot.rotation = Quaternion.Lerp(CameraPivot.rotation, rot * offsetRot, Time.deltaTime * 3.0f);

		//Vector3 PlayerScreenPos = .WorldToViewportPoint(FollowedObject.transform.position);

	}

	void TargetView()
	{
		Vector3 Midpoint = (TrackedObject.transform.position - FollowedObject.transform.position) / 2;

		Quaternion lookAt = Quaternion.LookRotation(TrackedObject.transform.position - transform.position);
		CameraPivot.transform.rotation = Quaternion.Slerp(transform.rotation, lookAt, Time.deltaTime * 5.0f);

		if (Joystick == JoystickNum.Keyboard)
		{
			CameraOffset.x += Input.GetAxis("Mouse X") * Sensitivity.x;
			CameraOffset.y += Input.GetAxis("Mouse Y") * Sensitivity.y;
			CameraOffset.z += Input.GetAxis("Mouse Y") * Sensitivity.z;
		}
		else
		{
			CameraOffset.x += Input.GetAxis(Joystick + "CameraHorizontal") * Sensitivity.x;
			CameraOffset.y += Input.GetAxis(Joystick + "CameraVertical") * Sensitivity.y;
			CameraOffset.z += Input.GetAxis(Joystick + "CameraVertical") * Sensitivity.z;
		}

		CameraOffset.x = Mathf.Clamp(CameraOffset.x, X_OFFSET_MIN, X_OFFSET_MAX);
		CameraOffset.y = Mathf.Clamp(CameraOffset.y, Y_OFFSET_MIN, Y_OFFSET_MAX);
		CameraOffset.z = Mathf.Clamp(CameraOffset.z, Z_OFFSET_MIN, Z_OFFSET_MAX);

		UpdatePivotRotation();
	}
	void FreeView()
	{
		
	}
}
