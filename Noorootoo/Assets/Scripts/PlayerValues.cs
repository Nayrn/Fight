using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public enum ElemTrait
{
    FIRE,
    WATER,
    EARTH,
    AIR,
    SOUL
}

public class PlayerValues : MonoBehaviour {

    //-----CONST VARIABLES-----//
	private const float MAX_HEALTH = 100;

    //-----HEALTH VARIABLES-----//

    //-----Status Variables, Damage to be moved to upcoming "Move Struct" in "Attack.cs"
	public float m_Health;

    public ElemTrait Attribute;

    public bool isStasis = false;
    public bool isBlocking = false;
    //----------MOVEMENT VARIABLES-----//

    //-----Variable used in jump, is turned off if above 3u above the ground, or if Jump is pressed
    [HideInInspector]
    public bool isGrounded;
    //-----DoubleJump variable used in determining how many jumps have been used up
    //-----True if player HAS NOT used up their double jump
    public bool DoubleJump = true;

    //-----Speed variable, 5 roughly matches the speed ad which the animations play
    //-----Characters skate if variable is higher
    [HideInInspector]
    public float m_Speed = 5;

    //-----Access to the animator, allows for Hit animations and Death animations
	public Animator anim = new Animator();


    //----------ATTACK VARIABLES-----//


    //-----Attack variables used in Attack.cs, stored here for cleanliness
    public bool isAttacking = false;
    public bool PrimaryAttack = false;
    public bool SecondaryAttack = false;


    //----------EXTRA VARIABLES-----//


    //-----Turn Speed. Increase to hasten the rate at which a character turns on the spot. default is 780
    public float TurnSpeed = 780.0f;

    //-----UI pieces
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
		if (!isBlocking)
		{
			if (col.gameObject.tag == "PrimaryAttack")
			{
				m_Health -=  10;

				col.enabled = false;
				anim.SetTrigger("TempHit");
				//anim.SetBool("isHit", true);
			}
			if (col.gameObject.tag == "SecondaryAttack")
			{
				m_Health -= 20;

				col.enabled = false;
				anim.SetTrigger("TempHit");
				//anim.SetBool("isHit", true);
			}
		}
	}
}
