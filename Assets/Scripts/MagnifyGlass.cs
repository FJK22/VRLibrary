using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
public class MagnifyGlass : MonoBehaviour, IPointerClickHandler
{
    public Canvas SearchCanvas;
    public Transform Mgposition;
    Transform OriginPlace;
    public GameObject window;


    private void Start()
    {
       
        OriginPlace = transform.parent;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (SearchCanvas.enabled)
        {
            transform.SetParent(OriginPlace);
            transform.DOMove(OriginPlace.position, 1).OnStart(() =>
            {
                transform.DORotate(OriginPlace.eulerAngles, 1);
            });
            SearchManager.Instance.LightTurnOn();
            
        }
        else
        {
            transform.SetParent(SearchCanvas.transform);
            transform.DOMove(SearchCanvas.transform.GetChild(0).position, 0.5f).OnStart(() =>
            {
                transform.DORotate(SearchCanvas.transform.GetChild(0).eulerAngles, 0.5f);
            });
            SearchCanvas.transform.parent.Find("Particle").gameObject.SetActive(false);
        }
        SearchCanvas.enabled = !SearchCanvas.enabled;
        window.SetActive(!window.activeSelf);
        if (SearchCanvas.enabled)
        {
            // SearchManager.Instance.SearchKey.ActivateInputField();
        }
        else
        {
            SearchManager.Instance.Clear();
        }
    }
}