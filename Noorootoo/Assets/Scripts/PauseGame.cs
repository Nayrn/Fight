using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public bool paused;

    public GameObject pausePanel;
    public Text pauseText;
    public Image pauseTitle;

    // Use this for initialization
    void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && paused == false)
        {
            paused = true;
            Debug.Log("Now you pause it");

        }
        else if (Input.GetKeyDown(KeyCode.Space) && paused == true)
        {
            paused = false;
            Debug.Log("and now you don't");
        }

        if (paused == true)
        {
            pausePanel.SetActive(true);
            pauseText.gameObject.SetActive(true);
            pauseTitle.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pausePanel.SetActive(false);
            pauseText.gameObject.SetActive(false);
            pauseTitle.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}