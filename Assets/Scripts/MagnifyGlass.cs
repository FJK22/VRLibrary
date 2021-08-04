using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class MagnifyGlass : MonoBehaviour, IPointerClickHandler
{
    public Canvas SearchCanvas;
    public Transform mg;
    public Transform SearchPlace;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (SearchCanvas.enabled)
        {
            mg.SetParent(SearchPlace);
            mg.DOMove(SearchPlace.position, 2).OnStart(() =>
            {
                mg.DORotate(SearchPlace.eulerAngles, 2);
            });
        }
        else
        {
            mg.SetParent(Camera.main.transform.GetChild(0));
            mg.DOMove(Camera.main.transform.GetChild(0).position, 0.5f).OnStart(() =>
            {
                mg.DORotate(Camera.main.transform.GetChild(0).eulerAngles, 0.5f);
            });
            SearchCanvas.transform.parent.Find("Particle").gameObject.SetActive(false);
        }
        SearchCanvas.enabled = !SearchCanvas.enabled;
    }
}
