using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
   
    private bool courutineallowed;
    void Start()
    {
        courutineallowed = true;
    }

    void OnMouseDrag()
    {
         float x = Input.GetAxis("Mouse X");
        this.transform.GetChild(0).gameObject.transform.RotateAround(transform.position, transform.up * Time.deltaTime * -1 * x, 6f);
        
    }

    private void OnMouseOver()
    {
        if (courutineallowed && this.gameObject.transform.GetChild(0).GetComponent<imagetexturefromur>().onmousecheck == true  && GameObject.FindObjectOfType<bookpickup>().bookPicked == false)
        {
            StartCoroutine("startpulsing");
        }
    }
    private IEnumerator startpulsing()
    {
        courutineallowed = false;
        for (float i = 0f; i <= 1f; i += 0.1f)
        {
            transform.localScale = new Vector3(
            (Mathf.Lerp(transform.localScale.x, transform.localScale.x + 0.025f, Mathf.SmoothStep(0f, 1f, i))),
            (Mathf.Lerp(transform.localScale.y, transform.localScale.y + 0.025f, Mathf.SmoothStep(0f, 1f, i))),
            (Mathf.Lerp(transform.localScale.z, transform.localScale.z + 0.025f, Mathf.SmoothStep(0f, 1f, i)))

                                              );
            yield return new WaitForSeconds(0.015f);
        }
        for (float i = 0f; i <= 1f; i += 0.1f)
        {
            transform.localScale = new Vector3(
            (Mathf.Lerp(transform.localScale.x, transform.localScale.x - 0.025f, Mathf.SmoothStep(0f, 1f, i))),
            (Mathf.Lerp(transform.localScale.y, transform.localScale.y - 0.025f, Mathf.SmoothStep(0f, 1f, i))),
            (Mathf.Lerp(transform.localScale.z, transform.localScale.z - 0.025f, Mathf.SmoothStep(0f, 1f, i)))

                                              );
            yield return new WaitForSeconds(0.015f);
        }
        courutineallowed = true;
    }
}
