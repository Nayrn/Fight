using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour {

	private float xOffsetMax = 50;
	private float xOffsetMin = -50;
	private float yOffsetMax = 18;
	private float yOffsetMin = -0.5f;

    float _FVX = 0.0f;
    float _FVY = 0.0f;

    public GameObject FollowedObject;
	public GameObject TrackedObject;
	public GameManager Game;

	public bool Targeted = true;

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

	void Update()
	{
		//rotation + position lerp

		//transform.rotation.x
	}

	// Update is called once per frame
	void LateUpdate()
	{
		//rotation + position lerp
		CameraPivot.transform.position = FollowedObject.transform.position;// Vector3.Lerp(CameraPivot.transform.position, FollowedObject.transform.position, Time.deltaTime * 5);

        if (Input.GetButtonDown(Joystick + "LockOn"))
			Targeted = !Targeted;

		//-----Swaps views if targeted is changed-----//
		if (Targeted == true)
		{
			TargetView();
			_FVX = 0;
			_FVY = 0;
		}
		else
			FreeView();
	}

	private void UpdatePivotRotation()
	{
		Vector3 dir = TrackedObject.transform.position - FollowedObject.transform.position;
		dir.y = 0.0f;
		dir.Normalize();

		Quaternion rot = Quaternion.LookRotation(dir);

		Quaternion offsetRot = Quaternion.AngleAxis(CameraOffset.x, Vector3.up);
		offsetRot *= Quaternion.AngleAxis(CameraOffset.y, Vector3.right);

		CameraPivot.rotation = Quaternion.Lerp(CameraPivot.rotation, rot * offsetRot, Time.deltaTime * 3.0f);
	}

	void TargetView()
	{
		if (Joystick == JoystickNum.Keyboard)
		{
			CameraOffset.x += Input.GetAxis("Mouse X") * Sensitivity.x;
			CameraOffset.y += Input.GetAxis("Mouse Y") * Sensitivity.y;
		}
		else
		{
			CameraOffset.x += Input.GetAxis(Joystick + "CameraHorizontal") * Sensitivity.x;
			CameraOffset.y += Input.GetAxis(Joystick + "CameraVertical") * Sensitivity.y;
		}

		CameraOffset.x = Mathf.Clamp(CameraOffset.x, xOffsetMin, xOffsetMax);
		CameraOffset.y = Mathf.Clamp(CameraOffset.y, yOffsetMin, yOffsetMax);


		Vector3 Midpoint = (TrackedObject.transform.position - FollowedObject.transform.position) / 2;
		Vector3 MidpointDirection = ((FollowedObject.transform.position + Midpoint) - transform.position).normalized;

		//transform.position = Vector3.Lerp(transform.position, CameraPivot.position + CameraOffset, Time.deltaTime);
		Quaternion lookAt = Quaternion.LookRotation(MidpointDirection);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookAt, Time.deltaTime * 5.0f);

		UpdatePivotRotation();
	}
	void FreeView()
	{
        
		if (Joystick == JoystickNum.Keyboard)
		{
            _FVX += Input.GetAxis("Mouse X") * Sensitivity.x;
            _FVY += Input.GetAxis("Mouse Y") * Sensitivity.y;
		}
		else
		{
            _FVX += Input.GetAxis(Joystick + "CameraHorizontal") * Sensitivity.x;
            _FVY += Input.GetAxis(Joystick + "CameraVertical") * Sensitivity.y;
        }

        _FVY = Mathf.Clamp(_FVY, 10, 55);

        Vector3 direction = new Vector3(0, 0, -5);
        Quaternion rotation = Quaternion.Euler(_FVY, _FVX, 0);
        transform.position = FollowedObject.transform.position + rotation * direction;

		Quaternion lookAt = Quaternion.LookRotation(FollowedObject.transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookAt, Time.deltaTime * 5.0f);
		transform.LookAt(FollowedObject.transform.position);
	}
}
