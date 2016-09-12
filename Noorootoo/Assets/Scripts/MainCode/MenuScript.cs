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
		Application.LoadLevel(Level);
	}

	public void SinglePlayer(string Level)
	{
		md.Players = new playerData[1];

		Debug.Log("No Singleplayer for you.");

		VSMatch(Level);
	}
	public void VSMatch(string Level)
	{
		md.Players = new playerData[2];

		LoadLevel(Level);
	}
	public void GameExit()
	{
		Application.Quit();
	}
}