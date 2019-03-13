using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletController : MonoBehaviour {
    public Rigidbody rb;
    public float speed;
    public float range = 10f;
    public GameObject currentTarget;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        
        if (transform.parent.gameObject.GetComponent<ShotgunController>().target == null)
        {
            Destroy(this.gameObject);
        }
        currentTarget = transform.parent.gameObject.GetComponent<ShotgunController>().target;
        rb.transform.LookAt(currentTarget.transform);
        rb.transform.Rotate(Random.Range(-10, 10), Random.Range(-10, 10), 0);
        rb.AddForce(transform.forward * speed);
        gameObject.transform.SetParent(null);
    }
	
	// Update is called once per frame
	void Update () {

        if (currentTarget== null)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == ("Enemy"))
        {
            Destroy(this.gameObject);
        }
        if (col.gameObject.tag == ("Prop"))
        {
            Destroy(gameObject);
        }        
    }
}

