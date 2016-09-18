using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public struct playerData
{
	GameObject characterSelected;
	int skinSelected;
}
public class MenuData : MonoBehaviour {

	public playerData[] Players = new playerData[1];
	private GameObject[] CharacterList;

	public string levelSelected = "Main Build 2"; // farting goose

	// Use this for initialization
	void Start ()
	{
		DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

    public void OnClick()
    {
        SceneManager.LoadScene(levelSelected, LoadSceneMode.Single);
    }

}
