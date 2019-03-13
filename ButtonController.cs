using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

    public Transform spawnPosition;
    public GameObject smokeEffect;
    public GameObject cannon;
    public bool canBuy = true;
    public float waitTime= 0;
    public float waitTimer = 0;
    public Image waitImage;
    public float cost;
    public bool purchased;

    void OnTriggerEnter(Collider col)
    {
        if (canBuy == true)
        {
            if (col.CompareTag("Button"))
            {
                if (CoinCountController.score.CurrentCurrency >= cost)
                {
                    purchased = true;
                    Debug.Log("buttonPressed");
                    StartCoroutine(spawn());
                    waitTime = 3;
                    waitTimer = 0;
                    StartCoroutine(wait());
                    CoinCountController.score.CurrentCurrency = CoinCountController.score.CurrentCurrency - cost;
                }
            }
        }
        if (col.CompareTag("Button"))
        {
            FindObjectOfType<AudioManager>().PlaySound("ButtonPress");
        }
    }
    IEnumerator spawn()
    {       
        canBuy = false;        
        Instantiate(smokeEffect, spawnPosition.transform.position, spawnPosition.transform.rotation);
        yield return new WaitForSeconds(1);
        Instantiate(cannon, spawnPosition.transform.position, spawnPosition.transform.rotation);
        yield return new WaitForSeconds(2);
        canBuy = true;
    }
    IEnumerator wait()
    {
        waitTimer += Time.deltaTime / 3;
        waitImage.fillAmount = waitTimer;
        if(waitTimer <= 1)
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(wait());
        }
        else
        {
            yield return new WaitForEndOfFrame();
        }      
    }
}
