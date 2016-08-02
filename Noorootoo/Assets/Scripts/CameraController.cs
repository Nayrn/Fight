﻿using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour {

	private float xOffsetMax = 50;
	private float xOffsetMin = -50;
	private float yOffsetMax = 18;
	private float yOffsetMin = -0.5f;

	public GameObject FollowedObject;
	public GameObject TrackedObject;
	public GameManager Game;

	public bool Targeted = true;

	public JoystickNum Joystick = JoystickNum.Keyboard;
	private Transform CameraPivot;

	public Vector3 CameraOffset = new Vector3();
	private Vector3 TargetOffset = new Vector3();
	private Vector3 FreeOffset = new Vector3();

	float xPos = 0;
	float yPos = 0;

	public Vector3 Sensitivity = new Vector3(4.0f, 3.0f, 1.0f);
	// Use this for initialization
	void Start () {
		CameraPivot = transform.parent;
		UpdatePivotRotation();
	}



	// Update is called once per frame
	void LateUpdate()
	{
		//rotation + position lerp
		CameraPivot.transform.position = FollowedObject.transform.position;// Vector3.Lerp(CameraPivot.transform.position, FollowedObject.transform.position, Time.deltaTime * 5);

		if (Input.GetKeyDown(KeyCode.O))
			Targeted = !Targeted;

		if (Targeted == true)
			TargetView();
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
			transform.RotateAround(CameraPivot.position, Vector3.up, Input.GetAxis("Mouse X") * Sensitivity.x);
			transform.RotateAround(CameraPivot.position, Vector3.right, -Input.GetAxis("Mouse Y") * Sensitivity.y);
		}
		else
		{
			transform.RotateAround(CameraPivot.position, Vector3.up, Input.GetAxis(Joystick + "CameraHorizontal") * Sensitivity.x);
			transform.RotateAround(CameraPivot.position, Vector3.right, -Input.GetAxis(Joystick + "CameraVertical") * Sensitivity.y);
		}
		transform.LookAt(FollowedObject.transform.position);
	}
}
