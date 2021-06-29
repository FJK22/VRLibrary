using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class loadinscreencontrol : MonoBehaviour
{

    public GameObject progresspanel;
    public Slider progressbar;
    public GameObject player;
    float val=0;
    private void Start()
    {
        progresspanel.SetActive(true);
        player.GetComponent<RigidbodyFirstPersonController>().enabled = false;

    }
    private void Update()
    {
       // changeprogressvalue();
       // progressbar.value = val;
       
    }
    


    void changeprogressvalue()
    {

        //  val= GameObject.FindObjectOfType<apijson>().done;
        val = val + 5;
      

        if (val>95&&val<110)
        {
            progresspanel.SetActive(false);
            player.GetComponent<RigidbodyFirstPersonController>().enabled = true;
            this.GetComponent<loadinscreencontrol>().enabled = false;
        }


    }

}
