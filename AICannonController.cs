using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICannonController : MonoBehaviour
{

    public Quaternion lookRotation;
    private GameObject Enemy;

    public float detectionDist = 10f;
    public Vector3 enemyPos;    
    public float range = 10f;
    public float InstantiationTimer = 0.5f;
    public float Timer;

    public GameObject cannonBall;
    public Transform shootPosition;

    public bool canShoot ;
    public bool shootEnable ;
    public bool snapped ;

    public float distance;

    public GameObject target;
    private Transform cachedTransform;
    private Vector3 cachedPosition;

    public GameObject cannonEffect;
    public GameObject smokeEffect;

    public Material mat;

    void Start()
    {
        canShoot = false;
        shootEnable = false;
        snapped = false;

        //Instantiate(smokeEffect, this.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));

        target = FindTarget();
        cachedTransform = GetComponent<Transform>();
        if (target)
        {
            cachedPosition = target.transform.position;
            distance = Vector3.Distance(target.transform.position, transform.position);
        }
    }
    void Update()
    {     
        if (shootEnable == true)
        {           
            if (!target)
            {
                target = FindTarget();
            }
            else if (target)
            {
                distance = Vector3.Distance(target.transform.position, transform.position);
                if (distance < range)
                {
                    this.transform.LookAt(target.transform);
                    shoot();
                    distance = Vector3.Distance(target.transform.position, transform.position);
                    if (target.GetComponent<EnemyHealthController>().Health <= 0)
                    {
                        target = FindTarget();
                    }
                }
                else if (distance > range)
                {
                    target = FindTarget();
                }               
            }
        }
        else if(shootEnable == false)
        {
            this.GetComponent<Range>().enabled = true;          
        }
    }
    void shoot()
    {
        InstantiationTimer -= Time.deltaTime;

        if (InstantiationTimer <= 0)
        {           
            GameObject CannonBall= Instantiate(cannonBall, shootPosition.position, shootPosition.rotation);
            Instantiate(cannonEffect, shootPosition.position, shootPosition.rotation);
            CannonBall.transform.parent = gameObject.transform;
            InstantiationTimer = Timer;
        }             
    }
    /*if(shootEnable == true)
{
    if (!target)
    {
        target = FindTarget();
        distance = Vector3.Distance(target.transform.position, transform.position);
    }
    else
    {
        if (canShoot == true)
        {
            if (distance < range)
            {
                Debug.Log("shooting in range");
                if (target)
                {
                    if (distance > range)
                    {
                        target = FindTarget();
                        distance = Vector3.Distance(target.transform.position, transform.position);
                    }
                    else if (distance < range)
                    {
                        //distance = Vector3.Distance(target.transform.position, transform.position);
                        this.transform.LookAt(target.transform);
                        shoot();
                        distance = Vector3.Distance(target.transform.position, transform.position);
                    }                                                     
                }
                else if (!target)
                {
                    target = FindTarget();
                    if (target)
                    {
                        distance = Vector3.Distance(target.transform.position, transform.position);
                    }
                }
            }                    
            if (distance > range)
            {
                target = FindTarget();
                if (target)
                {
                    distance = Vector3.Distance(target.transform.position, transform.position);
                }
            }
            if (target.GetComponent<EnemyHealthController>().Health <= 0)
            {
                target = FindTarget();
                if (target)
                {
                    distance = Vector3.Distance(target.transform.position, transform.position);
                }
            }

            // }
        }
    }
}*/
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, range);
    }
   /* public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        if (gos.Length > 0)
        {         
            Vector3 position = transform.position;
            closest = gos[0];
            foreach (GameObject go in gos)
            {
                float distance = Vector3.Distance(go.transform.position, transform.position);
                if (distance < range)
                {
                    Vector3 direction = (go.transform.position - transform.position);
                    lookRotation = Quaternion.LookRotation(direction);

                    Ray ray = new Ray(transform.position, go.transform.position);
                    Debug.DrawRay(transform.position, direction, Color.red);


                    if (Vector3.Distance(this.transform.position, go.transform.position) < Vector3.Distance(this.transform.position, closest.transform.position))
                    {
                        closest = go;
                    }
                   // Debug.Log("Closest"); 
                    canShoot = true;      
                }
                float distance2 = Vector3.Distance(closest.transform.position, transform.position);
                if (distance2 > range)
                {
                    canShoot = false;
                }
            }
            if (closest == null)
            {
                canShoot = false;
            }
            //this.transform.LookAt(closest.transform);               
        } 
        return closest;               
    }*/

    private void OnTriggerEnter(Collider other)
    {       
        if(other.gameObject.tag == "Tower")
        {
            //Debug.Log("CannonHitTower");
            if (other.gameObject.GetComponent<towerController>().snapped == false)
            {
                //Instantiate(smokeEffect, other.transform.position + new Vector3(0,0.1f,0), Quaternion.Euler(new Vector3(-90, 0, 0)));
                //Debug.Log("CannonShouldSnap");
                shootEnable = true;
                snapped = true;
            }
        }
        if(other.gameObject.tag == "Hand")
        {
            this.GetComponent<LineRenderer>().enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Hand")
        {
            this.GetComponent<LineRenderer>().enabled = false;
        }
    }
    public GameObject FindTarget()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
                canShoot = true;
            }
        }
        return closest;
    }
}
