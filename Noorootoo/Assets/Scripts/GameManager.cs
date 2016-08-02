using UnityEngine;
using System.Collections;

public enum GameMode
{
	SinglePlayer,
	MultiPlayer,
	MultiPlayerConfined
};
public class GameManager : MonoBehaviour {

	public GameMode Mode = GameMode.SinglePlayer;

	public Camera MainCamera;
	public Camera Player2Camera;

	private CursorLockMode lockMode;
	// Use this for initialization
	void Start () {
		lockMode = CursorLockMode.None;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown(KeyCode.Z))
		{
			SwitchCursorMode();
		}

		Cursor.lockState = lockMode;

		if(Input.GetKeyDown(KeyCode.P))
		{
			if (Mode == GameMode.SinglePlayer)
				Mode = GameMode.MultiPlayer;
			else
				Mode = GameMode.SinglePlayer;
		}

		SplitScreen();
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

	private void SplitScreen()
	{
		if(Mode == GameMode.SinglePlayer || Mode == GameMode.MultiPlayerConfined)
		{
			MainCamera.rect = new Rect(0, 0, 1, 1);
		}
		else if(Mode == GameMode.MultiPlayer)
		{
			MainCamera.rect = new Rect(0, 0, 0.5f, 1);
			Player2Camera.rect = new Rect(0.5f, 0, 0.5f, 1);
		}
	}
}
