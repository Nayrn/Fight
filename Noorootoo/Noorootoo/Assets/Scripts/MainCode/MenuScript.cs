using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
	MenuData md;
	// Use this for initialization
	void Start()
    {
		GameObject MenuDataObject = new GameObject();
		MenuDataObject.transform.position = Vector3.zero;
		MenuDataObject.name = "MenuData";
		MenuDataObject.AddComponent<MenuData>();
		md = MenuDataObject.GetComponent<MenuData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void LoadLevel(string Level)
	{
        Application.LoadLevel("Main Build 2");
		//SceneManager.LoadScene(Level, LoadSceneMode.Single);
	}

	public void SinglePlayer(string Level)
	{
		md.Players = new PlayerData[1];

		Debug.Log("No SinglePlayer for you.");

		VSMatch(Level);
	}
	public void VSMatch(string Level)
	{
		md.Players = new PlayerData[2];

		LoadLevel(Level);
	}
	public void GameExit()
	{
		Application.Quit();
	}
}