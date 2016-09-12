using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollisionSoul : MonoBehaviour
{
	public PlayerValues Player;
    public PlayerValues P2;
	public PlayerValues Opponent;
    private bool soulUp;
    private float soulTime = 0.5f;
    private float distCheck;
    public Text p1Win;
    public Text p2Win;
    // Use this for initialization
    void Start ()
    {
        soulUp = false;
        p1Win.gameObject.SetActive(false);
        p2Win.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Check(Opponent.Attribute, Player.Attribute);

        if (soulUp == true)
        {
            soulTime -= Time.deltaTime;
            Opponent.m_soulAmount = Opponent.m_soulAmount + 20.0f * Time.deltaTime;
        }

        if (soulTime < 0)
        {
            soulUp = false;
            soulTime = 0.5f;
        }

        if (Player.isKO == true)
        {
            p1Win.text = Player.tag.ToString();
            p1Win.gameObject.SetActive(true);
        }
        else if (P2.isKO)
        {
            p2Win.text = P2.tag.ToString();
            p2Win.gameObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        // fill up soul for p1 and stop p2 movement
        if (col.gameObject.tag == "PrimaryAttack")
        {
            soulUp = true;          
            Player.MakePlayerStunned(0.5f);
 
        }
        if (col.gameObject.tag == "SecondaryAttack")
        {
            soulUp = true;
            Player.MakePlayerStunned(0.5f);

        }
	}



    // ELEMENT STUFF
    ElemTrait Check(ElemTrait elem2, ElemTrait elem1)
    {
        //-----Water > Fire > Earth > Air > Water-----//
        // checking Opponent
        if (elem1 == ElemTrait.FIRE) 
        {
            if (elem2 == ElemTrait.WATER)
                Opponent.m_damage = 15.0f;
            
        }

        if(elem1 == ElemTrait.WATER)
        {
            if (elem2 == ElemTrait.AIR)
                Opponent.m_damage = 15.0f;
        }

        if(elem1 == ElemTrait.EARTH)
        {
            if (elem2 == ElemTrait.FIRE)
                Opponent.m_damage = 15.0f;
        }
        if(elem1 == ElemTrait.AIR)
        {
            if (elem2 == ElemTrait.EARTH)
                Opponent.m_damage = 15.0f;
        }

        if (elem1 == ElemTrait.UNASPECTED)
        {
            if (elem2 != ElemTrait.UNASPECTED)
                Opponent.m_damage = 15.0f;
        }


        // checking player 2
        if (elem2 == ElemTrait.FIRE)
        {
            if (elem1 == ElemTrait.WATER)
                Player.m_damage = 15.0f;
        }

        if (elem2 == ElemTrait.WATER)
        {
            if (elem1 == ElemTrait.AIR)
                Player.m_damage = 15.0f;
        }

        if (elem2 == ElemTrait.EARTH)
        {
            if (elem1 == ElemTrait.FIRE)
                Player.m_damage = 15.0f;
        }
        if (elem2 == ElemTrait.AIR)
        {
            if (elem1 == ElemTrait.EARTH)
                Player.m_damage = 15.0f;
        }

        if(elem2 == ElemTrait.UNASPECTED)
        {
            if (elem1 != ElemTrait.UNASPECTED)
                Player.m_damage = 15.0f;
        }
            
        return elem1;
    }
}
