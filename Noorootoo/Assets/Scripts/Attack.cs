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
			if(Player.PrimaryAttack == true)
			{
				if (PrimaryCount <= 2 && CurrentState.IsName(PrimaryCombos[PrimaryCount]))
					PrimaryCount++;

				if(PrimaryCount != 3)
				{
					anim.SetInteger("PrimaryCombo", PrimaryCount);

					CurrentState = anim.GetCurrentAnimatorStateInfo(0);
					if(CurrentState.IsName(PrimaryCombos[PrimaryCount]) && colliderTime == 0);
						colliderTime = CurrentState.length;
				}
			}
			else if(Player.PrimaryAttack == false)
			{
				Player.PrimaryAttack = true;

				Debug.Log("Punching");
				// set colliders to active
				CollidersOn();
				// punch animation  
				anim.SetBool("PrimaryAttack", Player.PrimaryAttack);
				
				CurrentState = anim.GetCurrentAnimatorStateInfo(0);
				if(CurrentState.IsName(PrimaryCombos[PrimaryCount]) && colliderTime == 0);
					colliderTime = CurrentState.length;
			}
        }

        if (Input.GetButtonDown(Joystick + "Kick") && Player.PrimaryAttack == false)   // Kick
        {
            // set colliders to active
            Player.SecondaryAttack = true;
            CollidersOn();
            // kick animation
            anim.SetBool("SecondaryAttack", true);

			AnimatorStateInfo Anim = anim.GetCurrentAnimatorStateInfo(0);
			if (Anim.IsName(SecondaryCombos[SecondaryCount]) && colliderTime == 0) ;
				colliderTime = Anim.length;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "PrimaryAttack")
        {
            Player.m_Health = Player.m_Health - 10;
            // hit animation
            Debug.Log("col with hands");
			anim.SetBool("isHit", true);
        }

        if (col.gameObject.tag == "SecondaryAttack")
        {
            Player.m_Health = Player.m_Health - 20;
            // hit animation
            Debug.Log("col with feet");
            anim.SetBool("isHit", true);
        }

    }

    public void CollidersOn()
    {
		if (Player.PrimaryAttack == true)
		{
			colliders[0].GetComponent<BoxCollider>().enabled = true;
		}
        if (Player.SecondaryAttack == true)
        {
            colliders[1].GetComponent<BoxCollider>().enabled = true;
            colliders[2].GetComponent<BoxCollider>().enabled = true;
        }
        // switch colliders on for this amount of time
    }

    void FixedUpdate()
    {
		if (Player.SecondaryAttack || Player.PrimaryAttack)
		{
			if (colliderTime > 0 && (Player.PrimaryAttack == true || Player.SecondaryAttack == true))
				colliderTime -= Time.deltaTime;
			else if (colliderTime < 0.0f)
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
