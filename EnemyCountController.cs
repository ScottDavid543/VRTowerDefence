using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCountController : MonoBehaviour {
    public GameObject[] currentEnemies;
    public Image enemyBar;
    public Text enemyText;
    public float enemyCount;
	// Use this for initialization
	void Start () {
        currentEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = currentEnemies.Length;
        enemyText.text = enemyCount + " / 20";
	}
	
	// Update is called once per frame
	void Update () {
        currentEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = currentEnemies.Length;
        enemyText.text = enemyCount + " / 20";
        enemyBar.fillAmount = enemyCount / 20;
	}
}
