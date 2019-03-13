using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallController : MonoBehaviour {
    public Rigidbody rb;
    public float speed;
    public float range = 10f;
    public GameObject currentTarget;
    
 
    void Start ()
    {
        FindObjectOfType<AudioManager>().PlaySound("CannonFire");
        rb = GetComponent<Rigidbody>();
        //currentTarget = FindClosestEnemy();
        if(transform.parent.gameObject.GetComponent<AICannonController>().target != null)
        {
            currentTarget = transform.parent.gameObject.GetComponent<AICannonController>().target;
        }
        else if(transform.parent.gameObject.GetComponent<AICannonController>().target == null)
        {
            Destroy(this.gameObject);
        }
    }
	
	void Update ()
    {
        if (currentTarget !=null)
        {
            this.transform.LookAt(currentTarget.transform.position);
            this.transform.Rotate(new Vector3(90, 0, 0));
            if (currentTarget == null)
            {
                Destroy(this.gameObject);
            }
            if (currentTarget)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.deltaTime);
            }
        }
        else if(currentTarget == null)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == ("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
    public GameObject FindClosestEnemy()
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
            }          
        }
        if (gos.Length <= 0)
        {
            Destroy(gameObject);
        }        
        return closest;
    }
}
