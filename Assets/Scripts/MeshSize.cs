using System;
using System.Collections;
using UnityEngine;

public class MeshSize : MonoBehaviour
{
    public static MeshSize Instance = null;
    public int totalBookCount = 100; // by other script set this befor start function excute
    [SerializeField] private Transform floor0; // this is floor0 transform. it is parent of all rows in floor0. 
    [SerializeField] private Transform floor1;
    [SerializeField] private Transform Roof;
    [SerializeField] private Transform CenterOrLibrary; // this is library circle center transform, you can use library object 

    [SerializeField] private Transform BookSlotPrefab; // bookslot prefab for instantiating
    [SerializeField] private Transform BookSlotParent; // parent of bookslot gameobejct newly generated.
    
    [Range(0.1f, 1f)]
    public float distanceOfBook = 1f; // this is distance between book, the available number of book in row depends on this value, you can change on inspector.
    public float radius = 0;    // this is library radius : is caculated in script.
    private Vector3 _center;

    void Awake(){
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(this);
        }
    }
    public IEnumerator Init()
    {
        if (totalBookCount == 0) return null;
        _center = CenterOrLibrary.position;
        Vector3 _radiusVector = floor0.GetChild(0).position - _center;
        radius = Mathf.Sqrt(_radiusVector.x * _radiusVector.x + _radiusVector.z * _radiusVector.z);
        int _remain = FillInFloor(floor0, totalBookCount);
        if(_remain > 0)
        {
            _remain = FillInFloor(floor1, _remain);
            int floorNum = 1;
            while(_remain > 0)
            {
                floorNum++;
                Transform newFloor = MakeNewFloor(floorNum);
                _remain = FillInFloor(newFloor, _remain);
            }
        }
        return null;
    }

    private Transform MakeNewFloor(int floorNum)
    {
        Transform newFloor = Instantiate(floor1.parent, floor1.parent.parent);
        newFloor.position = floor1.parent.position + Vector3.up * 5 * (floorNum - 1);
        newFloor.name = "Floor" + floorNum;
        Transform row = newFloor.Find("Rows");
        Roof.Translate(Vector3.up * 5);
        return row;
    }

    private int FillInFloor(Transform floor, int _remain)
    {
        Bounds _bound;
        float _length;
        int _capacity;
        float _angleDelta;
        Vector3 _direction;
        foreach (Transform row in floor)
        {
            _bound = row.GetComponent<MeshRenderer>().bounds;
            _length = Mathf.Sqrt(_bound.size.x * _bound.size.x + _bound.size.z * _bound.size.z);
            _capacity = (int)(_length / distanceOfBook);
            _angleDelta = Mathf.Asin(_length / radius / 2f) / (float)_capacity * 360 / Mathf.PI;
            for (int i = 1; i < _capacity; i++)
            {
                Transform slot = Instantiate(BookSlotPrefab, BookSlotParent);
                slot.name = row.name + "book" + i;
                slot.position = row.position;
                slot.RotateAround(_center, Vector3.up, _angleDelta * i);
                _direction = new Vector3(_center.x - slot.position.x, 0, _center.z - slot.position.z);
                slot.rotation = Quaternion.LookRotation(_direction);
                _remain -- ;
                if (_remain <= 0) return _remain;
            }
        }
        return _remain;
    }
}
