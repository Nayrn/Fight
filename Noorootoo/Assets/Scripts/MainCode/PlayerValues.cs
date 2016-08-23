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
    SOUL,
    UNASPECTED
}

public class PlayerValues : MonoBehaviour {

    //-----CONST VARIABLES-----//
	private const float MAX_HEALTH = 100;

	public Rigidbody rb;

    //-----Status Variables, Damage to be moved to upcoming "Move Struct" in "Attack.cs"
	public float m_Health;

    public ElemTrait Attribute;

	public bool isStunned = false;
    public bool isStasis = false;
    public bool isBlocking = false;

	//Opponent Variable
	public GameObject Opponent;

    //-----SOUL VARIABLES-----//
    public float m_soulAmount;

	//-----CONTROLLER VARIABLES-----//

	public JoystickNum Joystick = JoystickNum.Keyboard;
	public bool Targeted = false;

    //----------MOVEMENT VARIABLES-----//

	//-----Variable used in jump, is turned off if above 3u above the ground, or if Jump is pressed
    public bool isGrounded;
    //-----DoubleJump variable used in determining how many jumps have been used up
    //-----True if player HAS NOT used up their double jump
    public bool DoubleJump = true;

    //-----Speed variable, 5 roughly matches the speed ad which the animations play
    //-----Characters skate if variable is higher
    [HideInInspector]
    public float m_Speed = 5;

    //-----Access to the animator, used for all animations
	public Animator PlayerAnimation = new Animator();


    //----------ATTACK VARIABLES-----//


    //-----Attack variables used in Attack.cs, stored here for cleanliness
    public bool isAttacking = false;
    public bool PrimaryAttack = false;
    public bool SecondaryAttack = false;

	//----------EXTRA VARIABLES-----//

	//-----Gravity Variables
	public float gravityEdit = 1;

    //-----Turn Speed. Increase to hasten the rate at which a character turns on the spot. default is 780
    public float TurnSpeed = 780.0f;

    //-----UI pieces
  
    public Image KOText;
    public Slider p1Slider;
    public Slider SoulSlider;
    //public ParticleSystem particle;
    //public ParticleSystem dust;
    //-----Stun Timer
    private float staticTime = 0.0f;

    // Use this for initialization
    void Start ()
    {
		m_Health = MAX_HEALTH;
        m_soulAmount = 0;
        SoulSlider.maxValue = 100;
        Attribute = ElemTrait.UNASPECTED;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        rb.AddForce(Physics.gravity * gravityEdit * rb.mass);

		p1Slider.value = m_Health;
		SoulSlider.value = m_soulAmount;

        if (m_Health <= 0)
        {
            KOText.gameObject.SetActive(true);
            Time.timeScale = 0;
            // death or KO
            //play.gameObject.SetActive(true);
            m_Health = 0;
        }

        //------TESTING, DOES WORK ------
       
        if(m_soulAmount > 100)
        {
            m_soulAmount = 100;
        }
		//-----INPUT FOR ELEMENTS-------

		ChangeElement();

		if (isStunned)
        {
            staticTime -= Time.deltaTime;
            if (staticTime <= 0.0f)
                isStunned = false;
        }

        if(isGrounded)
        {
            dust.Play();
        }
	}

	void OnTriggerEnter(Collider col)
	{
		if (!isBlocking)
		{
			if (col.gameObject.tag == "PrimaryAttack")
			{
				m_Health -=  5;
                isAttacking = true;
				col.enabled = false;
				PlayerAnimation.SetTrigger("TempHit");
				Debug.Log("Colliders Off from hit");

			}
			if (col.gameObject.tag == "SecondaryAttack")
			{
				m_Health -= 10;
                isAttacking = true;
				col.enabled = false;
				PlayerAnimation.SetTrigger("TempHit");

				Debug.Log("Colliders Off from hit");
			}
		}
	}

   ElemTrait Strength(ElemTrait elem)
    {
        return elem;
    }

    public void MakePlayerStunned(float time)
    {
        isStunned = true;
        staticTime = time;
    }

	private void ChangeElement()
	{ 
		if (Input.GetAxis(Joystick + "DpadVertical") > 0) // FIRE
		{
			Debug.Log("Element Change; Fire");
			Attribute = ElemTrait.FIRE;
		}
		if (Input.GetAxis(Joystick + "DpadHorizontal") > 0) // EARTH
		{
			Debug.Log("Element Change; Earth");
			Attribute = ElemTrait.EARTH;
		}
		if (Input.GetAxis(Joystick + "DpadVertical") < 0) //AIR
		{
			Debug.Log("Element Change; Air");
			Attribute = ElemTrait.AIR;
		}
		if (Input.GetAxis(Joystick + "DpadHorizontal") < 0) // WATER
		{
			Debug.Log("Element Change; Water");
			Attribute = ElemTrait.WATER;
		}
	}
}
