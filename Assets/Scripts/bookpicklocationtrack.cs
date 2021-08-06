using UnityEngine;

public class bookpicklocationtrack : MonoBehaviour
{
    [SerializeField]
    string name;
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Player")
        //{
        //    GameObject[] objs = GameObject.FindGameObjectsWithTag(name);
        //    for (int i = 0; i < objs.Length; i++)
        //    {
        //        objs[i].gameObject.GetComponent<BoxCollider>().enabled = true;
        //    }
        //}
    }
}
