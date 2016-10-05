using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour {

	public JoystickNum Joystick = JoystickNum.Keyboard;

	public bool isSelected = false;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		ControlCheck();
	}

	private void RaycastCheck()
	{

	}
	 
	private void ControlCheck()
	{
		if (Input.GetButtonDown(Joystick + "Jump") && !isSelected)
			isSelected = true;
		if (Input.GetButtonDown(Joystick + "Primary") && isSelected)
			isSelected = false;

		if (!isSelected)
			transform.position = new Vector3(transform.position.x + Input.GetAxis(Joystick + "Horizontal") * 0.2f,	transform.position.y - Input.GetAxis(Joystick + "Vertical") * 0.2f, transform.position.z);
	}
}
