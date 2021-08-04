using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class bookpickup : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    public bool checkdefinearea = false;
    public GameObject player;
    public GameObject newlocationobject; 
    public bool clicked = false;
    public Vector3 tempbookloc;
    public Quaternion tempbookrot;
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
                if (eventSystem.IsPointerOverGameObject()) return;
                if (eventSystem.currentSelectedGameObject != null) return;
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (hit.transform.gameObject.name == "Book")
                    {
                        bookPicked = true;
                        tempbookloc = hit.transform.position;
                        tempbookrot = hit.transform.rotation;
                        iTween.MoveTo(hit.transform.gameObject, newlocationobject.transform.position, 3f);
                        hit.transform.rotation = newlocationobject.transform.rotation;
                        var img = hit.transform.GetChild(0).GetComponent<imagetexturefromur>();
                        for (int i = 0; i < img.textBoxs.Length; i++)
                        {
                            img.textBoxs[i].transform.parent.parent.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
                        }
                        img.arrow.SetActive(true);
                        img.detailpanel.SetActive(false);
                        hit.transform.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                        player.GetComponent<RigidbodyFirstPersonController>().enabled = false;
                    }
                }
            }
        }   
    }
}
