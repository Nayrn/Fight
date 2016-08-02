using UnityEngine;
using System.Collections;


public class Attack : MonoBehaviour
{
    public JoystickNum Joystick = JoystickNum.Keyboard;
    public GameObject[] colliders;

	public string[] PrimaryCombos;
	public string[] SecondaryCombos;
	public int PrimaryCount = 0;
	public int SecondaryCount = 0;

	private float leewayTime = 4.5f;

	private AnimatorStateInfo CurrentState;

    public PlayerValues Player;

    public float colliderTime;

    public Animator anim;
    // Use this for initialization
    void Start()
    {
        colliderTime = 0.5f;
        Player.SecondaryAttack = false;
        Player.PrimaryAttack = false;
		for (int i = 0; i < colliders.Length; i++ )
			colliders[i].GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown(Joystick + "Punch") && Player.SecondaryAttack == false)// punch
		{
			if (Player.PrimaryAttack == false)
			{
				Player.PrimaryAttack = true;
				colliderTime = 1.0f;

				PrimaryCount++;
				Debug.Log("Punching");
				// set colliders to active
				CollidersOn();
				// punch animation  
				anim.SetBool("PrimaryAttack", Player.PrimaryAttack);
			}
			else if (Player.PrimaryAttack == true)
			{
				if (PrimaryCount < PrimaryCombos.Length - 1)// && CurrentState.IsName(PrimaryCombos[PrimaryCount]))
					PrimaryCount++;
			}

			if (PrimaryCount <= PrimaryCombos.Length)
			{
				anim.SetInteger("PrimaryCombo", PrimaryCount);

				CurrentState = anim.GetCurrentAnimatorStateInfo(0);
				if (CurrentState.IsName(PrimaryCombos[PrimaryCount]))
					colliderTime = CurrentState.length + leewayTime;
			}

			if (PrimaryCount == PrimaryCombos.Length && CurrentState.IsName(PrimaryCombos[PrimaryCombos.Length - 1]))// && CurrentState.IsName(SecondaryCombos[SecondaryCount]))
				PrimaryCount = 0;
		}

        if (Input.GetButtonDown(Joystick + "Kick") && Player.PrimaryAttack == false)   // Kick
		{
			if (Player.SecondaryAttack == false)
			{
				Player.SecondaryAttack = true;
				colliderTime = 1.0f;

				SecondaryCount++;
				Debug.Log("Kicking");
				// set colliders to active
				CollidersOn();
				// punch animation  
				anim.SetBool("SecondaryAttack", Player.SecondaryAttack);

				CurrentState = anim.GetCurrentAnimatorStateInfo(0);
				if (CurrentState.IsName(SecondaryCombos[SecondaryCount]))
					colliderTime = CurrentState.length + leewayTime;
			}
			else if (Player.SecondaryAttack == true)
			{
				if (SecondaryCount < SecondaryCombos.Length - 1)// && CurrentState.IsName(PrimaryCombos[PrimaryCount]))
					SecondaryCount++;
			}

			if (SecondaryCount <= SecondaryCombos.Length)
			{
				anim.SetInteger("PrimaryCombo", SecondaryCount);

				CurrentState = anim.GetCurrentAnimatorStateInfo(0);
				if (CurrentState.IsName(SecondaryCombos[SecondaryCount]))
					colliderTime = CurrentState.length + leewayTime;
			}

			if (SecondaryCount == SecondaryCombos.Length && CurrentState.IsName(SecondaryCombos[SecondaryCombos.Length - 1]))// && CurrentState.IsName(SecondaryCombos[SecondaryCount]))
				SecondaryCount = 0;
		}
    }

    //void OnCollisionEnter(Collision col)
    //{
        //if (col.gameObject.tag == "RightHand")
        //{
            //Player.m_Health = Player.m_Health - 10;
            //// hit animation
            //Debug.Log("col with hands");
			//anim.SetBool("isHit", true);
        //}

        //if (col.gameObject.tag == "SecondaryAttack")
        //{
            //Player.m_Health = Player.m_Health - 20;
            //// hit animation
            //Debug.Log("col with feet");
            //anim.SetBool("isHit", true);
        //}

    //}

    public void CollidersOn()
    {
		if (Player.PrimaryAttack)
		{
			colliders[0].GetComponent<BoxCollider>().enabled = true;
			colliders[0].tag = "PrimaryAttack";
		}
		else if (Player.SecondaryAttack)
		{
			colliders[0].GetComponent<BoxCollider>().enabled = true;
			colliders[0].tag = "SecondaryAttack";
		}
		// switch colliders on for this amount of time
	}

    void FixedUpdate()
    {
		if (Player.SecondaryAttack || Player.PrimaryAttack)
		{
			if (colliderTime > 0 && (Player.PrimaryAttack == true || Player.SecondaryAttack == true))
				colliderTime -= Time.deltaTime;
			else if (colliderTime <= 0.0f)
			{
				for (int i = 0; i < colliders.Length; i++)
					colliders[i].GetComponent<BoxCollider>().enabled = false;

				colliderTime = 0;

				Player.SecondaryAttack = false;
				Player.PrimaryAttack = false;
				PrimaryCount = 0;
				SecondaryCount = 0;

				//turn animation off
				anim.SetBool("SecondaryAttack", Player.SecondaryAttack);
				anim.SetBool("PrimaryAttack", Player.PrimaryAttack);
			}
		}
    }
}
