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

    //-----Status Variables, Damage to be moved to upcoming "Move Struct" in "Attack.cs"
	public float m_Health;

    public ElemTrait Attribute;

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
	public Animator PlayerAnimation = new Animator();


    //----------ATTACK VARIABLES-----//


    //-----Attack variables used in Attack.cs, stored here for cleanliness
    public bool isAttacking = false;
    public bool PrimaryAttack = false;
    public bool SecondaryAttack = false;


    //----------EXTRA VARIABLES-----//


    //-----Turn Speed. Increase to hasten the rate at which a character turns on the spot. default is 780
    public float TurnSpeed = 780.0f;

    //-----UI pieces
  
    public Image KOText;
    public Slider p1Slider;
    public Slider SoulSlider;
   // public Button play;
	[HideInInspector]
	public float fallSpeed = -4;


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
	void Update ()
    {
   
      int health = (int)m_Health;
      
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
    
            
          //if(Input.GetKey(KeyCode.I)) // FIRE
          //{
              //Attribute = ElemTrait.FIRE;
          //}

          //if(Input.GetKey(KeyCode.L)) // EARTH
          //{
             //Attribute = ElemTrait.EARTH;
          //}

          //if(Input.GetKey(KeyCode.K)) //AIR
          //{
                //Attribute = ElemTrait.AIR;
          //}

          //if(Input.GetKey(KeyCode.J)) // WATER
          //{
            //Attribute = ElemTrait.WATER;
          //}
        //--------------------------------

        if(isStasis)
        {
            staticTime -= Time.deltaTime;
            if (staticTime <= 0.0f)
                isStasis = false;
        }
	}

	void OnTriggerEnter(Collider col)
	{
		if (!isBlocking)
		{
			if (col.gameObject.tag == "PrimaryAttack")
			{
				m_Health -=  1;
                isAttacking = true;
				col.enabled = false;
				PlayerAnimation.SetTrigger("TempHit");
               
            }
			if (col.gameObject.tag == "SecondaryAttack")
			{
				m_Health -= 5;
                isAttacking = true;
				col.enabled = false;
				PlayerAnimation.SetTrigger("TempHit");			
			}
		}
	}

   ElemTrait Strength(ElemTrait elem)
    {
        return elem;
    }

    public void MakePlayerStatic(float time)
    {
        isStasis = true;
        staticTime = time;
    }
}
