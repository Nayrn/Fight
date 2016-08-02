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

	public Animator anim;

    public bool isAttacking = false;
    public bool PrimaryAttack = false;
    public bool SecondaryAttack = false;

    public float TurnSpeed;
    public Text healthText;
    public Image KOText;
    public Slider p1Slider;
   // public Button play;
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
            //play.gameObject.SetActive(true);
            m_Health = 0;
        }
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "PrimaryAttack")
		{
			m_Health = m_Health - 10;

			col.enabled = false;
			anim.SetTrigger("TempHit");
			//anim.SetBool("isHit", true);
		}
		if (col.gameObject.tag == "SecondaryAttack")
		{
			m_Health = m_Health - 20;

			col.enabled = false;
			anim.SetTrigger("TempHit");
			//anim.SetBool("isHit", true);
		}
	}

    public void PlayClick()
    {
       // SceneManager.LoadScene("MAIN SCENCE 2"); // CAUSES YELLING
    }
}
