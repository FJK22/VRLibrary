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
        textBoxs[0].text = "Primary Call: "+book.primary_call;
        textBoxs[1].text = "Author: "+book.author;
        textBoxs[2].text = "Title: "+book.title;
        textBoxs[3].text = "ISBN: "+book.isbn;
        textBoxs[4].text = "Publication Date: "+book.publication_date;
        textBoxs[5].text = "Publisher: "+book.publisher;
        textBoxs[6].text = "Tag 300: "+book.tag300;
        textBoxs[7].text = "Subjects: "+book.subjects;
        if(TextureURL != "")StartCoroutine(DownloadImage(TextureURL));
    }

   
    IEnumerator DownloadImage(string MediaUrl)
    {  
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            //  Debug.Log(request.error);
        }
        else
        this.gameObject.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }
    
   
    


}
