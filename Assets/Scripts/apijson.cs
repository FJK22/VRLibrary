using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using TMPro;
using System;
using System.Linq;
using UnityStandardAssets.Characters.FirstPerson;
using DG.Tweening;

public class apijson : MonoBehaviour
{
    public float done = 0; //chechking for all books are loaded    
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject group1;
    [SerializeField] Transform MiniBookSlot;
    public List<GameObject> positionmarker;
    public static List<imagetexturefromur> books;
    public GameObject obj;
    [SerializeField]
    GameObject logs;
    [SerializeField]
    TextMeshProUGUI logText;
    public GameObject progresspanel;
    public GameObject player;
    float val = 0;

    //In this script I make the connection with api to get the book metadata
    void Start()
    {
        progresspanel.SetActive(true);
        player.GetComponent<RigidbodyFirstPersonController>().enabled = false;
        StartCoroutine(GetRequest("https://www.oxvrlibrary.com/api/fjAkl22m9bEpqs/apifj22qasywz/book/get_book/all")); // Api here: 
    }

    void SetPositionMarker()
    {
        for(int i =0; i<2400; i++)
        positionmarker.Add(group1.transform.GetChild(i).gameObject);
        positionmarker = positionmarker.OrderBy(tile => tile.name).ToList(); // done
    }

    IEnumerator GetRequest(string uri)
    {
        yield return new WaitForSeconds(0.1f);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            webRequest.SendWebRequest();
            while (!webRequest.isDone)
            {
                yield return null;
            }
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    logs.SetActive(true);
                    logText.text = "Connection Error";
                    break;

                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(": Error: " + webRequest.error);
                    logs.SetActive(true);
                    logText.text = webRequest.error;
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + webRequest.error);
                    logs.SetActive(true);
                    logText.text = webRequest.error;
                    break;
                case UnityWebRequest.Result.Success:
                     Root r = new Root();
                     r = JsonConvert.DeserializeObject<Root>( webRequest.downloadHandler.text);
                    //Make request. Don't yield
                    if (r.status=="true")//checking that books have any value or not
                    {
                        int _bookCount = 100;//r.books.Count;
                        MeshSize.Instance.totalBookCount = _bookCount;
                        yield return MeshSize.Instance.Init();
                        string[] bookprimarytext = new string[_bookCount];
                        _bookCount = (_bookCount > group1.transform.childCount) ? group1.transform.childCount : _bookCount;
                        for (int i = 0; i < _bookCount; i++)
                        {
                            bookprimarytext[i] = r.books[i].primary_call;
                            positionmarker.Add(group1.transform.GetChild(i).gameObject);
                        }
                        Array.Sort(bookprimarytext);

                        if (_bookCount > 0) books = new List<imagetexturefromur>();
                        for (int i = 0; i < _bookCount; i++)
                        {
                            obj = Instantiate(prefab, positionmarker[i].transform.position, positionmarker[i].transform.rotation);
                            obj.transform.SetParent(transform);
                            obj.transform.Rotate(Vector3.up, -90);
                            obj.transform.Translate(Vector3.right * 0.2f);
                            imagetexturefromur itfu = obj.transform.GetChild(0).gameObject.GetComponent<imagetexturefromur>();
                            itfu.minibook = MiniBookSlot.GetChild(i);
                            itfu.arrow.SetActive(true);
                            obj.gameObject.name = "Book";
                            int j = 0;
                            while (true)  //finding the value of first book 
                            {
                                if (r.books[j].primary_call == bookprimarytext[i])  //as we have sorted the bookprimarytext we will now sort all books according to this
                                {
                                    break;
                                }
                                j++; //founded the value of sorted books
                            }
                            itfu.book = r.books[j];
                            books.Add(itfu);
                        }
                        progresspanel.GetComponent<CanvasGroup>().DOFade(0, 2).OnComplete(() =>
                        {
                            progresspanel.SetActive(false);
                        });
                        player.GetComponent<RigidbodyFirstPersonController>().enabled = true;
                    }
                    else
                    {
                        logs.SetActive(true);
                        logText.text = r.message;
                    }
                    break;
            }
        }
    }
}
public class Book
{
    public string id { get; set; }
    public string current_sub_library_id { get; set; }
    public string collection_id { get; set; }
    public string remote_storage_id { get; set; }
    public string barcode { get; set; }
    public string primary_call { get; set; }
    public string description { get; set; }
    public string item_status_id { get; set; }
    public string process_status_id { get; set; }
    public string process_status_update_date { get; set; }
    public string material_type_id { get; set; }
    public string holding_doc_no { get; set; }
    public string doc_no { get; set; }
    public string format_id { get; set; }
    public string author { get; set; }
    public string title { get; set; }
    public string edition { get; set; }
    public string isbn { get; set; }
    public string publication_date { get; set; }
    public string publisher { get; set; }
    public string tag300 { get; set; }
    public string a300 { get; set; }
    public string subjects { get; set; }
    public string tag050 { get; set; }
    public string language_id { get; set; }
    public string item_open_date { get; set; }
    public string update_date { get; set; }
    public string last_loan_date { get; set; }
    public string last_return_date { get; set; }
    public string no_current_loans { get; set; }
    public string item_total_no_loans { get; set; }
    public string solo_link { get; set; }
    public string book_cover_filename { get; set; }
}
public class Root
{
    public string status { get; set; }
    public string message { get; set; }
    public List<Book> books { get; set; }
}