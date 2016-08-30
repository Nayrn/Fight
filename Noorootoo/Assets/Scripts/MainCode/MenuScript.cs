using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void LoadLevel(string Level)
	{
		Application.LoadLevel(Level);
	}
	public void PlayerLoadLevel(int PlayerCount, string Level)
	{
		//Playercount for GameManager

		//Application.LoadLevel(Level);
	}
	public void SinglePlayer()
	{

	}
	public void VSMatch()
	{

	}
	public void GameExit()
	{
		Application.Quit();
	}
}