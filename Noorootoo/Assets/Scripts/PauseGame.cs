using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public bool paused;

    public GameObject pausePanel;
    public Text pauseText;

    // Use this for initialization
    void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            paused = true;
            Debug.Log("I should work here");
        }
        if (Input.GetKeyDown(KeyCode.Escape) && paused == true)
        {
            paused = false;
            Debug.Log("I should work here");
        }

        if (paused == true)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            pauseText.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            pauseText.gameObject.SetActive(false);
        }
    }
}