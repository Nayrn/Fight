using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{

    public bool bStart;
    public bool bQuit;

    public void OnMouseDown()
    {
        if (bStart == true)
        {
            Application.LoadLevel("MAIN SCENCE 2");

            //when it tells you that you should use scenemanager instead, it LIES
            //SceneManager.LoadScene("main build", LoadSceneMode.Single);
            
            Debug.Log("i SHOULD work");
        }

        if (bQuit == true)
        {
            Application.Quit();

            Debug.Log("i SHOULD work");
        }
    }
}