using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
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
    public static bool isInputing = false;
    static public SearchManager Instance;
    public bool IsInputing
    {
        get { return isInputing; }
        set { isInputing = value; }
    }
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
            foreach (Transform result in ResultParent)
            {
                Results.Add(result.GetComponent<ResultItem>());
            }
        }
    }
    private void Update()
    {
        if (player) transform.LookAt(player);
    }
    public void Search()
    {
        isInputing = false;
        ResultItem.turnedCount = 0;
        string key = SearchKey.text.Trim();
        LightTurnOn();
        foreach (var ri in Results)
        {
            ri.Clear();
        }
        if (key != "" && apijson.books.Count > 0)
        {
            Regex regex = new Regex(key, RegexOptions.IgnoreCase);
            int count = 0;
            foreach (var b in apijson.books)
            {
                if (regex.IsMatch(b.book.title) || regex.IsMatch(b.book.author))
                {
                    Results[count].SetValue((count + 1).ToString(), b);
                    count++;
                }
                if (count >= Results.Count) break;
            }
            if (count > 0)
            {
                NoResult.gameObject.SetActive(false);
                ResultParent.sizeDelta = new Vector2(0, 100 * count);
            }
            else
            {
                NoResult.gameObject.SetActive(true);
            }
            SearchKey.DeactivateInputField();
            ResultParent.parent.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
        }
    }
    public void LightTurnOn(bool isOn = true)
    {
        foreach (var l in lights)
        {
            l.intensity = (isOn) ? 1f : 0.4f;
        }
    }
    public void Clear()
    {
        SearchKey.text = "";
        Search();
    }
}