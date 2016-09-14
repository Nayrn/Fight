using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public struct playerData
{
	int characterSelected;
	int skinSelected;
}
public class MenuData : MonoBehaviour {

	public playerData[] Players = new playerData[1];

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
        SceneManager.LoadScene("Main Build 2", LoadSceneMode.Single);
    }

}
