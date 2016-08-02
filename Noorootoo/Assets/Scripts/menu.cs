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

    void OnMouseDown()
    {
        if (bStart == true)
        {
            //when it says you can use scenemanager, it LIES
            Application.LoadLevel("MAIN SCENCE 2");
            string scene = "MAIN SCENCE 2";
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
            
            Debug.Log("i SHOULD work");
        }

        if (bQuit == true)
        {
            Application.Quit();

            Debug.Log("i SHOULD work");
        }
    }
}