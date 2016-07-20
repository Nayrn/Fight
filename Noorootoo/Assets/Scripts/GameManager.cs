using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private CursorLockMode lockMode;
	// Use this for initialization
	void Start () {
		lockMode = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown(KeyCode.Z))
		{
			SwitchCursorMode();
		}

		Cursor.lockState = lockMode;
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
