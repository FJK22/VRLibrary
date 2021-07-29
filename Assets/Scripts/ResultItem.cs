using UnityEngine;
using UnityEngine.UI;
public class ResultItem : MonoBehaviour
{
    [SerializeField] Text No;
    [SerializeField] Text BookName;
    [SerializeField] Text AuthorName;
    [SerializeField] Button SpotButton;
    [SerializeField] Image LightImage;
    private imagetexturefromur Book;
    private CanvasGroup ui;
    public static Texture2D texture;
    bool isTurnOn = false;
    bool IsTurnOn
    {
        get { return isTurnOn; }
        set
        {
            isTurnOn = value;
            LightImage.enabled = isTurnOn;
            if (Book != null)
            {
                Book.SpotLight.enabled = isTurnOn;
            }
        }
    }
    public static int turnedCount;
    private void Start()
    {
        SpotButton.onClick.AddListener(delegate { SportClick(); });
        ui = GetComponent<CanvasGroup>();
        Clear();
    }
    public void SetValue(string no, imagetexturefromur book)
    {
        No.text = no;
        BookName.text = book.book.title;
        AuthorName.text = book.book.author;
        Book = book;
        ui.alpha = 1;
    }
    public void Clear()
    {
        ui.alpha = 0;
        IsTurnOn = false;
    }
    private void SportClick()
    {
        IsTurnOn = !IsTurnOn;
        turnedCount += (isTurnOn) ? 1 : -1;
        SearchManager.Instance.LightTurnOn(turnedCount == 0);
    }
}
