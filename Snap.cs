using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour {
    public GameObject tower;
    public OVRGrabbable ovrGrabbable;
    public Rigidbody rb;
 
	void Start () {
        ovrGrabbable = GetComponent<OVRGrabbable>();
        rb = GetComponent<Rigidbody>();       
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Tower")
        {
            //Debug.Log("Cannon hit Tower");
            if (col.gameObject.GetComponent<towerController>().snapped == false)            
            {
                this.GetComponent<LineRenderer>().enabled = false;
                // Debug.Log("Cannon should snap");
                tower = col.gameObject;
                this.transform.parent = tower.transform;

                Destroy(ovrGrabbable);
                Destroy(rb);                

                transform.rotation = tower.transform.rotation;
                transform.localPosition = Vector3.zero;
                transform.localPosition = new Vector3(0, 2.5f, 0);
                transform.localScale = Vector3.one;
            }        
        }
    }
}
