using UnityEngine;
using System.Collections;

public class AIScript : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public GameObject player;
    public PlayerValues p1;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player1");
        p1 = GameObject.Find("Player1").GetComponent<PlayerValues>();

    }
        // Update is called once per frame
        void Update()
    {
            navAgent.destination = player.transform.position;
            float distance = Vector3.Distance(navAgent.transform.position, player.transform.position);
            if (distance <= 1.5f)
            {
                navAgent.destination = navAgent.transform.position;
                canAttack();

            }
        }
    

    public bool canAttack()
    {
        p1.m_Health = p1.m_Health - 0.5f;
        return true;
    }
}
