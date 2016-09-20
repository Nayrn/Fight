using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameMode
{
	SinglePlayer,
	MultiPlayer,
	MultiPlayerConfined
};


public class GameManager : MonoBehaviour {

	private GameMode Mode = GameMode.MultiPlayer;

	//-----Camerastate and Splitscreen Variables-----//
	public Camera MainCamera;
	public Camera Player2Camera;

	private PlayerValues Player1;
	private PlayerValues Player2;

	//-----Timer Variables-----//
	private float Timer;
	public Text TimeText;
	public float maxTime;

	private CursorLockMode lockMode;
	// Use this for initialization
	void Start ()
	{
		lockMode = CursorLockMode.None;

		MainCamera = GameObject.Find("Camera Pivot").GetComponentInChildren<Camera>();
		Player2Camera = GameObject.Find("Camera 2 Pivot").GetComponentInChildren<Camera>();

		Player1 = GameObject.Find("Player1").GetComponent<PlayerValues>();
		Player2 = GameObject.Find("Player2").GetComponent<PlayerValues>();

		Timer = maxTime;
	}
	
	void Update()
	{
		//-----Cast to int to get rounded timer
		//-----Cast to string to show on Canvas
		int timer = (int)Timer;
		TimeText.text = timer.ToString();

		//-----Reduce timer by deltaTime
		Timer -= Time.deltaTime;

		//-----Reset to 0 if it falls below
		if (Timer < 0)
			Timer = 0;


	}
	// Update is called once per frame
	void FixedUpdate ()
	{
		//-----Swaps Cursor free on Keyboard
		if (Input.GetKeyDown(KeyCode.Z))
		{
			SwitchCursorMode();

			Cursor.lockState = lockMode;
		}

		//----Swaps Player mode -- Also changes screen view if necessary-----//
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
			//Resets Player screens for single Player
			MainCamera.rect = new Rect(0, 0, 1, 1);
		}
		else if(Mode == GameMode.MultiPlayer)
		{
			//Updates Player screens to show 2 Players
			MainCamera.rect = new Rect(0, 0, 0.5f, 1);
			Player2Camera.rect = new Rect(0.5f, 0, 0.5f, 1);
		}
	}
}
