using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerController : MonoBehaviour {

    public bool snapped = false;	

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Cannon")
        {
            //Debug.Log("Cannon has hit Tower");
            //snapped = true;
            StartCoroutine(snapWait());
        }
        if (col.gameObject.tag == "Ballista")
        {
            //Debug.Log("Cannon has hit Tower");
            //snapped = true;
            StartCoroutine(snapWait());
        }
    }

    IEnumerator snapWait()
    {
        yield return new WaitForSeconds(0.1f);
        FindObjectOfType<AudioManager>().PlaySound("PlaceTower");
        snapped = true;
    }
}
