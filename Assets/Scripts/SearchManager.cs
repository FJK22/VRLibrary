using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SearchManager : MonoBehaviour
{
    [SerializeField] RectTransform ResultParent;
    [SerializeField] InputField SearchKey;
    [SerializeField] Text NoResult;
    [SerializeField] Transform LightParent;

    private Light[] lights;
    List<ResultItem> Results;
    Transform player;
    
    static public SearchManager Instance;
    void Awake()
    {
        if (Instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        lights = LightParent.GetComponentsInChildren<Light>();
        player = Camera.main.transform;
    }
    private void Start()
    {
        if (ResultParent)
        {
            Results = new List<ResultItem>();
            foreach(Transform result in ResultParent)
            {
                Results.Add(result.GetComponent<ResultItem>());
            }
        }
    }
    private void Update()
    {
        if(player)transform.LookAt(player);
    }
    public void Search()
    {
        ResultItem.turnedCount = 0;
        string key = SearchKey.text.Trim();
        LightTurnOn();
        foreach(var ri in Results)
        {
            ri.Clear();
        }
        ResultParent.parent.parent.GetComponent<ScrollRect>().verticalScrollbar.value = 0;
        if(key != "" && apijson.books.Count > 0)
        {
            int count = 0;
            foreach(var b in apijson.books)
            {
                if(b.book.title.Contains(key) || b.book.author.Contains(key))
                {
                    Results[count].SetValue((count + 1).ToString(), b);
                    count++;
                }
                if (count >= Results.Count) break;
            }
            if(count > 0)
            {
                NoResult.gameObject.SetActive(false);
                ResultParent.sizeDelta = new Vector2(0, 100 * count);
            }
            else
            {
                NoResult.gameObject.SetActive(true);
            }
            SearchKey.DeactivateInputField();
        }
    }
    public void LightTurnOn(bool isOn = true)
    {
        foreach(var l in lights)
        {
            l.intensity = (isOn) ? 1f: 0.4f;
        }
    }
    public void Clear()
    {
        SearchKey.text = "";
        Search();
    }
}