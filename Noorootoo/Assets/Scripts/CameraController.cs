using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour {

	public GameObject FollowedObject;
	public GameObject TrackedObject;

	private Transform CameraPivot;

	public Vector2 CameraOffset = new Vector2();
	// Use this for initialization
	void Start () {
		CameraPivot = transform.parent;
		UpdatePivotRotation();
	}



	// Update is called once per frame
	void Update () {
		//rotation + position lerp
		CameraPivot.transform.position = FollowedObject.transform.position;
		transform.LookAt(TrackedObject.transform.position);

		UpdatePivotRotation();
	}

	private void UpdatePivotRotation()
	{
		Vector3 dir = TrackedObject.transform.position - FollowedObject.transform.position;
		dir.y = 0.0f;
		dir.Normalize();

		Quaternion rot = Quaternion.LookRotation(dir);

		Quaternion offsetRot = Quaternion.AngleAxis(CameraOffset.x, new Vector3(0, 1.0f, 0));
		offsetRot *= Quaternion.AngleAxis(CameraOffset.y, new Vector3(1.0f, 0.0f, 0));



		CameraPivot.rotation = rot * offsetRot;


	}
}
