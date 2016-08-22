using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{

    public bool bStart;
    public bool bQuit;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (bStart == true)
        {
            //when it says you can use scenemanager, it LIES
            Application.LoadLevel("Main Build 2");

            //SceneManager.LoadScene("Main Build 2", LoadSceneMode.Single);
            
            Debug.Log("i SHOULD work");
        }

        if (bQuit == true)
        {
            Application.Quit();

            Debug.Log("i SHOULD work");
        }
    }
}