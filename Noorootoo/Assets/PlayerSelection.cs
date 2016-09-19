using UnityEngine;
using System.Collections;

public class PlayerSelection : MonoBehaviour {

	private GameObject[] CharacterList;

	private int Index = 0;

	private bool AxisInUse = false;

	public JoystickNum Joystick = JoystickNum.Keyboard;
	// Use this for initialization
	void Start ()
	{
		CharacterList = new GameObject[transform.childCount];


		for (int i = 0; i < transform.childCount; i++)
			CharacterList[i] = transform.GetChild(i).gameObject;


		//Set all to off
		foreach (GameObject g in CharacterList)
			g.SetActive(false);

		

		if (CharacterList[Index])
			CharacterList[Index].SetActive(true);
	}
	
	// Update is called once per frame
	void Update ()
	{
		Controls();
	}

	public void Controls()
	{
		CharacterList[Index].SetActive(false);

		if (Input.GetAxis(Joystick + "DpadHorizontal") < 0 && AxisInUse == false)
		{
			AxisInUse = true;

			Index--;

			if (Index < 0)
				Index = CharacterList.Length - 1;
		}
		else if (Input.GetAxis(Joystick + "DpadHorizontal") > 0 && AxisInUse == false)
		{
			AxisInUse = true;

			Index++;

			if (Index == CharacterList.Length)
				Index = 0;
		}
		else if (Input.GetAxis(Joystick + "DpadHorizontal") == 0)
			AxisInUse = false;

		CharacterList[Index].SetActive(true);
	}
}
