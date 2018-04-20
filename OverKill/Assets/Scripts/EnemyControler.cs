using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControler : MonoBehaviour {
    [Range(0f, 10f)]
    [SerializeField]
    private float closeEnoughDistance = 2f;

    private float time = 1.5f;
    public float interpolationPeriod = 0.1f;
    private float nextActionTime = 0.0f;
    public float period = 0.1f;

    float fireTimer;
    public float fireRate = 0.1f;
    public float range = 3f;
    public float damage = 10f;
    public Transform ATCKpoint;
    public float huehue;

    public AnimationClip Anim;
    public AnimationClip Anim2;
    private Animation myAnimation;

    public float lookRadius = 10000f;
    Transform target;
    Transform enemy;
    NavMeshAgent agent;
    // Use this for initialization
    void Start() {
        target = PlayerManager.instance.player.transform;
        enemy = EnemyManeger.instance.enemy.transform;
        agent = GetComponent<NavMeshAgent>();
        


    }

    // Update is called once per frame
    void Update() {
        time += Time.deltaTime;
        float distance = Vector3.Distance(target.position, transform.position);
        float enemyDistance = Vector3.Distance(enemy.position, transform.position);
       // Debug.Log(distance + "distance");
        //Debug.Log(closeEnoughDistance + "tuvums");

        if (closeEnoughDistance < distance)
        {
            myAnimation = GetComponent<Animation>();
            myAnimation.Play(Anim2.name);
            agent.SetDestination(target.position);
            
        }
            else
            {
            agent.SetDestination(enemy.position);
            
            if (Time.time > nextActionTime)
            {
                nextActionTime = Time.time + period;

                Fire1();
               
                

            }

        }
	}

    private void Fire1()
    {
        myAnimation = GetComponent<Animation>();
        myAnimation.Play(Anim.name);
        Debug.Log("Fire1");

        RaycastHit hit;
        if (Physics.Raycast(ATCKpoint.position, ATCKpoint.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name + "found");// hit detect


            
            if (hit.transform.GetComponent<PlayerHealthControler>())
            {
                hit.transform.GetComponent<PlayerHealthControler>().ApplyDamageP(damage);
            }
       }


        fireTimer = 0.0f; // reset fire timer
    }



    void OnDrawRange()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
