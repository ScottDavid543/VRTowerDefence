using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    public GameObject winTxt;
    public GameObject waveSpawner;
    public EnemyHealthController myHealth;
	
	void Start () {
        winTxt = FindObjectOfType<FindWinText>().winText;
        myHealth = GetComponent<EnemyHealthController>();
        waveSpawner = GameObject.FindGameObjectWithTag("WaveSpawner");
        Destroy(waveSpawner);
	}
	
	// Update is called once per frame
	void Update () {
		if(myHealth.Health<=0)
        {
            winTxt.SetActive(true);
        }
        
	}
}
