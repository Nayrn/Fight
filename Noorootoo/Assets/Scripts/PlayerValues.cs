using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class PlayerValues : MonoBehaviour {

	private const float MAX_HEALTH = 100;

	public float m_Health;
	public float m_Damage;

	public bool isGrounded;
	public float m_Speed;
	public float m_JumpSpeed;

    public bool isAttacking = false;
    public bool PrimaryAttack = false;
    public bool SecondaryAttack = false;

    public float TurnSpeed;
    public Text healthText;
    public Text KOText;
    public Slider p1Slider;
    public Button play;
	[HideInInspector]
	public float fallSpeed = -4;

	// Use this for initialization
	void Start ()
    {
		m_Health = MAX_HEALTH;
        
	}
	
	// Update is called once per frame
	void Update ()
    {
      //m_Health--; for testing purposes, does work
      int health = (int)m_Health;
      healthText.text = health.ToString();
      p1Slider.value = m_Health;

       
        if(m_Health <= 0)
        {
            KOText.gameObject.SetActive(true);
            Time.timeScale = 0;
            // death or KO
            play.gameObject.SetActive(true);
            m_Health = 0;
        }
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "LeftHand" || col.gameObject.tag == "RightHand")
		{
			m_Health = m_Health - 10;
          
		}

		if (col.gameObject.tag == "LeftFoot" || col.gameObject.tag == "RightFoot")
		{
			m_Health = m_Health - 10;
		}

	}

    public void PlayClick()
    {
       // SceneManager.LoadScene("MAIN SCENCE 2"); // CAUSES YELLING
    }
}
