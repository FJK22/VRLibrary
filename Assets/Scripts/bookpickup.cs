using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;


public class bookpickup : MonoBehaviour
{
    
   public bool checkdefinearea = false;
    public GameObject player;
    public GameObject newlocationobject;
   
   public bool clicked = false;
  public  Vector3 tempbookloc;
     public Quaternion   tempbookrot;


    public List<GameObject> activeBooks;
   
    public bool checkclicked = false;


    public bool bookPicked = false;


    public void changeclick()
    {
        checkclicked = false;


    }

  
    void Update()
    {
        if(bookPicked == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (hit.transform.gameObject.name == "Book")
                    {

                        bookPicked = true;


                          
                           
                            tempbookloc = hit.transform.gameObject.transform.position;
                            tempbookrot = hit.transform.rotation;
                            iTween.MoveTo(hit.transform.gameObject, newlocationobject.transform.position, 3f);
                            hit.transform.rotation = newlocationobject.transform.rotation;


                            for (int i = 0; i < hit.transform.GetChild(0).GetComponent<imagetexturefromur>().textBoxs.Length; i++)
                            {
                                hit.transform.GetChild(0).GetComponent<imagetexturefromur>().textBoxs[i].gameObject.transform.parent.transform.parent.transform.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;

                            }


                            hit.transform.GetChild(0).gameObject.GetComponent<imagetexturefromur>().arrow.SetActive(true);
                            hit.transform.GetChild(0).gameObject.GetComponent<imagetexturefromur>().detailpanel.SetActive(false);
                            hit.transform.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                            player.GetComponent<RigidbodyFirstPersonController>().enabled = false;

                           
                           
                        
                    }
                }

            }
        }
       
        
          /*  if (checkclicked == false)  //cheking that booked is already click or not
            {
            
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, 100.0f))
                    {
                       if (hit.transform.gameObject.name == "Book")
                        {
                       
                            if (clicked == false) //checking book is open or close or in front of user 
                            {

                           
                                hit.transform.GetChild(0).gameObject.GetComponent<imagetexturefromur>().onmousecheck = false; //switching off .. pulsing the book
                                checkclicked = true;  // doing it so that  until this object goes down no object cant be clicked
                                tempbookloc = hit.transform.gameObject.transform.position;
                                tempbookrot = hit.transform.rotation;
                                clicked = true; //this condition will let ..to keep book down to its place
                                Invoke("changeclick", 2f); //2 second locking so that no one can click for 2 second
                                iTween.MoveTo(hit.transform.gameObject, newlocationobject.transform.position, 3f);
                                hit.transform.rotation = newlocationobject.transform.rotation;

                    
                                for(int i=0;i<hit.transform.GetChild(0).GetComponent<imagetexturefromur>().textBoxs.Length;i++)
                                {
                                      hit.transform.GetChild(0).GetComponent<imagetexturefromur>().textBoxs[i].gameObject.transform.parent.transform.parent.transform.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
                               
                                }

                     
                                hit.transform.GetChild(0).gameObject.GetComponent<imagetexturefromur>().arrow.SetActive(true);
                                hit.transform.GetChild(0).gameObject.GetComponent<imagetexturefromur>().detailpanel.SetActive(false);
                                hit.transform.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                                player.GetComponent<RigidbodyFirstPersonController>().enabled = false;

                            for(int i = 0; i<activeBooks.Count;i++)
                            {
                                activeBooks[i].GetComponent<BoxCollider>().enabled = false;
                            }

                                for (int i = 0; i < GameObject.FindObjectOfType<apijson>().positionmarker.Count; i++)
                                {
                                 GameObject.FindObjectOfType<apijson>().positionmarker[i].gameObject.GetComponent<BoxCollider>().enabled = true;
                               
                                }
                            }
                        }
                    }

                }
             }*/

        
    }
   
}
