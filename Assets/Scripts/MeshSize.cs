using System;
using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class MeshSize : MonoBehaviour
{
    public static MeshSize Instance = null;
    public int totalBookCount = 100; // by other script set this befor start function excute
    [SerializeField] private Transform floor0; // this is floor0 transform. it is parent of all rows in floor0. 
    [SerializeField] private Transform floor1;
    [SerializeField] private Transform miniFloor1;
    [SerializeField] private Transform miniFloor0;

    [SerializeField] private Transform elevator1;
    [SerializeField] private Elevator ElevatorPrefab;
    [SerializeField] private Transform Roof;
    [SerializeField] private Transform CenterOrLibrary; // this is library circle center transform, you can use library object 

    [SerializeField] private Transform BookSlotPrefab; // bookslot prefab for instantiating
    [SerializeField] private Transform MiniBookPrefab;
    [SerializeField] private Transform MiniBookParent;
    [SerializeField] private Transform BookSlotParent; // parent of bookslot gameobejct newly generated.
    
    [Range(0.1f, 1f)]
    public float distanceOfBook = 1f; // this is distance between book, the available number of book in row depends on this value, you can change on inspector.
    public float radius = 0;    // this is library radius : is caculated in script.
    private Vector3 _center;
    private float miniModelFloorDelta = 0;
    private float miniModelScale = 1;

    bool flag = true;
    void Awake(){
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(this);
        }
        miniModelFloorDelta = miniFloor1.position.y - miniFloor0.position.y;
        miniModelScale = miniFloor1.parent.localScale.x;
    }
    public IEnumerator Init()
    {
        if (totalBookCount == 0) return null;
        _center = CenterOrLibrary.position;
        Vector3 _radiusVector = floor0.GetChild(0).position - _center;
        radius = Mathf.Sqrt(_radiusVector.x * _radiusVector.x + _radiusVector.z * _radiusVector.z);
        int _remain = FillInFloor(floor0, totalBookCount, 0);
        if(_remain > 0)
        {
            _remain = FillInFloor(floor1, _remain, 1);
            int floorNum = 1;
            while(_remain > 0)
            {
                floorNum++;
                Transform newFloor = MakeNewFloor(floorNum);
                Transform newMiniFloor = MakeNewMiniFloor(floorNum);
                _remain = FillInFloor(newFloor, _remain, floorNum);
            }
        }
        ElevatorManager.Instance.WasStarted();
        GC.Collect();
        return null;
    }

    private Transform MakeNewFloor(int floorNum)
    {
        Transform newFloor = Instantiate(floor1.parent, floor1.parent.parent);
        Elevator newElevator = Instantiate(ElevatorPrefab, elevator1.parent);
        newElevator.transform.rotation = elevator1.rotation;
        newElevator.transform.localScale = elevator1.localScale;
        newElevator.CurrentFloor = floorNum;
        newElevator.transform.position = elevator1.position + Vector3.up * 5 * (floorNum - 1);
        newFloor.position = floor1.parent.position + Vector3.up * 5 * (floorNum - 1);
        newFloor.name = "Floor" + floorNum;
        Transform row = newFloor.Find("Rows1");
        Roof.Translate(Vector3.back * 5);
        return row;
    }

    private Transform MakeNewMiniFloor(int floorNum)
    {
        Transform newFloor = Instantiate(miniFloor1, miniFloor1.parent);
        newFloor.position = miniFloor1.position + Vector3.up * miniModelFloorDelta * (floorNum - 1);
        newFloor.name = "Floor" + floorNum;
        return newFloor;
    }

    private int FillInFloor(Transform floor, int _remain, int floorNum)
    {
        Bounds _bound;
        float _length;
        int _capacity;
        float _angleDelta;
        Vector3 _direction;
        Vector3[] _verts;
        float _miniDeltaZ = floorNum * (5 - miniModelFloorDelta / miniModelScale) + 0.38f;
        foreach (Transform row in floor)
        {
            _length = GetLength(row.GetComponent<MeshFilter>().mesh, row.name) * 393.70f;
            if (_length > 0)
            {
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

                    Transform miniBook = Instantiate(MiniBookPrefab, MiniBookParent);
                    miniBook.localEulerAngles = slot.localEulerAngles;
                    if(floorNum < 1)
                    {
                        miniBook.localPosition = slot.localPosition;
                    }
                    else
                    {
                        miniBook.localPosition = slot.localPosition - new Vector3(0, _miniDeltaZ, 0);
                    }
                    _remain--;
                    if (_remain <= 0) return _remain;
                }
            }
        }
        return _remain;
    }
    private float GetLength(Mesh mesh, string name)
    {
        List<Vector3> endPoints = new List<Vector3>();
        foreach(int i in mesh.triangles)
        {
            if (mesh.triangles.Count(x => x == i) == 1) endPoints.Add(mesh.vertices[i]);
        }
        if(endPoints.Count == 2)
        {
            return Mathf.Sqrt((endPoints[0].x - endPoints[1].x) * (endPoints[0].x - endPoints[1].x)
                + (endPoints[0].y - endPoints[1].y) * (endPoints[0].y - endPoints[1].y));
        }
        else
        {
            if (flag)
            {
                flag = false;
                Debug.Log($"{name}: {mesh.vertexCount}");
                Debug.Log(string.Join(",", mesh.triangles));
            }
            Debug.LogError($"Find mesh endpoints count Error: {endPoints.Count}");
            return -1;
        }
    }
}
