using UnityEngine;
using System.Collections;

public class CollisionSoul : MonoBehaviour
{
    public PlayerValues player1;
    public PlayerValues player2;
    private bool soulUp;
    private bool soulUp2;
    private float soulTime = 0.5f;
    private float soulTime2 = 0.5f;
    // Use this for initialization
    void Start ()
    {
        soulUp = false;
        soulUp2 = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
     

        check(player1.Attribute, player2.Attribute);

         if (soulUp == true)
         {
             soulTime -= Time.deltaTime;
             player1.m_soulAmount = player1.m_soulAmount + 20.0f * Time.deltaTime;     
         }
       
       
        if (soulTime < 0)
        {
            soulUp = false;
            soulTime = 0.5f;
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
            player2.MakePlayerStunned(0.5f);
        }
     
        

        if (col.gameObject.tag == "SecondaryAttack")
        {
            soulUp = true;
            player2.MakePlayerStunned(0.5f);
        }




        //fill up soul for p2 and stop p1 movements
        if (col.gameObject.tag == "PrimaryP2")
        {
            soulUp2 = true;
            player1.MakePlayerStunned(0.5f);
            player1.m_Health -= player1.m_damage;
            player2.m_soulAmount += 10.7f;
        }
        else
            soulUp2 = false;
      
        if (col.gameObject.tag == "SecondaryP2")
        {
            soulUp2 = true;
            player1.MakePlayerStunned(0.5f);
            
            player1.m_Health -= player1.m_damage;
        }

    }

    ElemTrait check(ElemTrait elem1, ElemTrait elem2)
    {
        elem1 = player1.Attribute;   elem2 = player2.Attribute;
        // water > fire > Earth > > air > water

        // checking player1
        if (elem1 == ElemTrait.FIRE) 
        {
            if (elem2 == ElemTrait.WATER)
                player1.m_damage = 15.0f;
            
        }

        if(elem1 == ElemTrait.WATER)
        {
            if (elem2 == ElemTrait.AIR)
                player1.m_damage = 15.0f;
        }

        if(elem1 == ElemTrait.EARTH)
        {
            if (elem2 == ElemTrait.FIRE)
                player1.m_damage = 15.0f;
        }
        if(elem1 == ElemTrait.AIR)
        {
            if (elem2 == ElemTrait.EARTH)
                player1.m_damage = 15.0f;
        }

        if (elem1 == ElemTrait.UNASPECTED)
        {
            if (elem2 != ElemTrait.UNASPECTED)
                player1.m_damage = 15.0f;
        }


        // checking player 2
        if (elem2 == ElemTrait.FIRE)
        {
            if (elem1 == ElemTrait.WATER)
                player2.m_damage = 15.0f;
        }

        if (elem2 == ElemTrait.WATER)
        {
            if (elem1 == ElemTrait.AIR)
                player2.m_damage = 15.0f;
        }

        if (elem2 == ElemTrait.EARTH)
        {
            if (elem1 == ElemTrait.FIRE)
                player2.m_damage = 15.0f;
        }
        if (elem2 == ElemTrait.AIR)
        {
            if (elem1 == ElemTrait.EARTH)
                player2.m_damage = 15.0f;
        }

        if(elem2 == ElemTrait.UNASPECTED)
        {
            if (elem1 != ElemTrait.UNASPECTED)
                player2.m_damage = 15.0f;
        }
            
        return elem1;
    }
}
