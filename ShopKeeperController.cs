using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeperController : MonoBehaviour {


    public GameObject[] buttons;   
    
    public ButtonController buttonController;
    public Animation animation;
    public int size;
    public int i;
	void Start () {
        animation = GetComponent<Animation>();
        buttons = GameObject.FindGameObjectsWithTag("ButtonBack");
        size = buttons.Length;
    }
	
	
	void Update ()
    {
        for(i = 0; i<size; i++)
        {
            if (buttons[i].GetComponent<ButtonController>().purchased == true)
            {
                animation["M_Warlock_buff_spell_B"].wrapMode = WrapMode.Once;
                animation.Play("M_Warlock_buff_spell_B");
                StartCoroutine(WaitForAnimation());
                buttons[i].GetComponent<ButtonController>().purchased = false;
            }
            
        }
    }
   private IEnumerator WaitForAnimation ()
    {
        yield return new WaitForSeconds(3f);        
        animation.Play("Warlock_Standing_Free");              
    }  
}
