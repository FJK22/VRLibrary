using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ondropclick : MonoBehaviour
{
    Quaternion rot;
    private void Start()
    {
        rot = this.transform.localRotation;
    }


    public void ondrop()
    {
        StartCoroutine(DropBook());
    }
    
   /* IEnumerator DropBook()
    {

      
    
        float angle = Quaternion.Angle(rot, this.gameObject.transform.localRotation );
    
        if (angle > 0 || angle < 0)
        {
            iTween.RotateTo(this.transform.gameObject, iTween.Hash("rotation", new Vector3(0, 180, 0), "islocal", true, "time", 1.2f));
            yield return new WaitForSeconds(.5f);

        }

        Quaternion temp = transform.localRotation;
        temp.x = 0f;
        temp.y = 1f;
        temp.z = 0f;
        temp.w = 0f;
        transform.localRotation = rot;

        
                for (int i = 0; i < GameObject.FindObjectOfType<apijson>().positionmarker.Count; i++)
                {

                    GameObject.FindObjectOfType<apijson>().positionmarker[i].gameObject.GetComponent<BoxCollider>().enabled = false;
                }
                

         for (int i = 0; i < FindObjectOfType<bookpickup>().activeBooks.Count; i++)
         {
             Debug.Log(i);
             FindObjectOfType<bookpickup>().activeBooks[i].GetComponent<BoxCollider>().enabled = true;
         }

       

        this.GetComponent<imagetexturefromur>().detailpanel.SetActive(false);
         this.GetComponent<imagetexturefromur>().arrow.SetActive(false);
         this.GetComponent<imagetexturefromur>().onmousecheck = true; //checking for pulsing the book
         GameObject.FindObjectOfType<bookpickup>().checkclicked = true;
         GameObject.FindObjectOfType<bookpickup>().clicked = false; 
         iTween.MoveTo(this.transform.parent.gameObject, GameObject.FindObjectOfType<bookpickup>().tempbookloc, 3f); 
         this.transform.parent.gameObject.transform.rotation = GameObject.FindObjectOfType<bookpickup>().tempbookrot;
         GameObject.FindObjectOfType<bookpickup>().changeclick();
         this.transform.parent.gameObject.transform.GetChild(1).gameObject.SetActive(false);
         GameObject.FindObjectOfType<bookpickup>().player.GetComponent<RigidbodyFirstPersonController>().enabled = true;
        GameObject.FindObjectOfType<bookpickup>().bookPicked = false;
    }*/


    IEnumerator DropBook()
    {
        float angle = Quaternion.Angle(rot, this.gameObject.transform.localRotation);

        if (angle > 0 || angle < 0)
        {
            iTween.RotateTo(this.transform.gameObject, iTween.Hash("rotation", new Vector3(0, 180, 0), "islocal", true, "time", 1.2f));
            yield return new WaitForSeconds(.5f);
        }

        Quaternion temp = transform.localRotation;
        temp.x = 0f;
        temp.y = 1f;
        temp.z = 0f;
        temp.w = 0f;
        transform.localRotation = rot;

        this.GetComponent<imagetexturefromur>().detailpanel.SetActive(false);
        this.GetComponent<imagetexturefromur>().arrow.SetActive(false);
       
        iTween.MoveTo(this.transform.parent.gameObject, GameObject.FindObjectOfType<bookpickup>().tempbookloc, 3f);
        this.transform.parent.gameObject.transform.rotation = GameObject.FindObjectOfType<bookpickup>().tempbookrot;
       
        this.transform.parent.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        GameObject.FindObjectOfType<bookpickup>().player.GetComponent<RigidbodyFirstPersonController>().enabled = true;
        GameObject.FindObjectOfType<bookpickup>().bookPicked = false;
    }



}
