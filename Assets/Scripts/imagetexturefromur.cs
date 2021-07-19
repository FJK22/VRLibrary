using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class imagetexturefromur : MonoBehaviour
{
    private bool courutineallowed;
    public string TextureURL = "";
    public GameObject arrow, detailpanel;
    public bool onmousecheck=true;//checking for pulsing the book
    public TextMeshProUGUI[] textBoxs;   //primary_call, auther, bookName, isbn, publication_date, publisher, tag300, subjects;    
    public Book book;
    private bool isExpand = false;
    [SerializeField] TextMeshProUGUI MoreButtonText;
    [SerializeField] RectTransform MoreButtonIcon;
    [SerializeField] RectTransform DetailPan;
   private void Start()
    {
        courutineallowed = true;
        if (book.book_cover_filename != null && book.book_cover_filename != "")
        {
            TextureURL = "https://www.oxvrlibrary.com/upload_book_covers/" + book.book_cover_filename + ".png";
            Debug.Log(TextureURL);
        }
        Startating();
    }
    public void Startating()
    {
        
        textBoxs[0].text = book.author;
        textBoxs[1].text = book.title;
        textBoxs[2].text = book.publisher;
        textBoxs[3].text = book.publication_date;
        textBoxs[4].text = book.primary_call;
        textBoxs[5].text = book.subjects;
        textBoxs[6].text = book.isbn;
        textBoxs[7].text = book.tag300;
        if (TextureURL != "") StartCoroutine(DownloadImage(TextureURL));
    }
    IEnumerator DownloadImage(string MediaUrl)
    {
        Debug.Log(MediaUrl);
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            //  Debug.Log(request.error);
        }
        else
        this.gameObject.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }
    public void ExpandToggle()
    {
        isExpand = !isExpand;
        Expand(isExpand);
    }
    private void Expand(bool flag)
    {
        if (flag)
        {
            DetailPan.localPosition = new Vector3(0, -12.5f, 0);
            DetailPan.anchoredPosition = new Vector2(0, -12.5f);
            DetailPan.sizeDelta = new Vector2(0, 25);
            MoreButtonIcon.localScale = new Vector3(1, -1, 1);
            MoreButtonText.text = "Less";
        }
        else
        {
            DetailPan.anchoredPosition = new Vector2();
            DetailPan.localPosition = new Vector3();
            DetailPan.sizeDelta = new Vector2();
            MoreButtonIcon.localScale = new Vector3(1, 1, 1);
            MoreButtonText.text = "More";
        }
        textBoxs[5].transform.parent.parent.parent.parent.gameObject.SetActive(flag);
        textBoxs[6].transform.parent.parent.parent.parent.gameObject.SetActive(flag);
        textBoxs[7].transform.parent.parent.parent.parent.gameObject.SetActive(flag);
    }
}
