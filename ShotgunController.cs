using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunController : MonoBehaviour {
    public bool canShoot;
    public bool shootEnable;
    public bool snapped;

    public GameObject target;
    public GameObject pellet;
    public GameObject[] shootPos;

    public float distance;
    public float range;

    public float InstantiationTimer = 0.5f;
    public float Timer;

    // Use this for initialization
    void Start () {
        //int i = 0;
        target = FindTarget();
       
        
	}
	
	// Update is called once per frame
	void Update () {
         if (shootEnable == true)
         {
            //this.GetComponent<LineRenderer>().enabled = false; 
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
                 else if(distance > range)
                 {
                     target = FindTarget();
                 }
             }
         }
    }
    void shoot()
    {        
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.   
         InstantiationTimer -= Time.deltaTime;
         if (InstantiationTimer <= 0)
         {
            for(int i = 0; i <= 5; i++)
            {
                int spawnPointIndex = Random.Range(0, shootPos.Length);
                GameObject Pellet = Instantiate(pellet, shootPos[spawnPointIndex].transform.position,shootPos[spawnPointIndex].transform.rotation);           
                Pellet.transform.parent = gameObject.transform;       
            }
            FindObjectOfType<AudioManager>().PlaySound("ShotgunFire");
            InstantiationTimer = Timer;                    
         }    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tower")
        {
            if (other.gameObject.GetComponent<towerController>().snapped == false)
            {
                shootEnable = true;
                snapped = true;
            }
        }
        if (other.gameObject.tag == "Hand")
        {
            this.GetComponent<LineRenderer>().enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hand")
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
