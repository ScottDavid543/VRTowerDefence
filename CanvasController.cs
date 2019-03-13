using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasController : MonoBehaviour {

    //public GameObject[] gos;
    //public Transform canvas;
    //public Image healthBar;

        public GameObject Player;
   // public Image healthBar;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        Player = GameObject.FindGameObjectWithTag("Player");
        this.transform.LookAt(Player.transform.position);
       /* gos = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in gos)
        {
            var enemyHealth = Instantiate(healthBar, canvas.position, canvas.rotation);
            healthBar.transform.parent = gameObject.transform;
            healthBar.transform.position = go.transform.position;
        }*/
    }
}
