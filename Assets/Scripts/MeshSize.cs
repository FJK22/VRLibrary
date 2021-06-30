using UnityEngine;

public class MeshSize : MonoBehaviour
{
    [HideInInspector] public static int totalBookCount = 100; // by other script set this befor start function excute
    [SerializeField] private Transform floor0;
    [SerializeField] private Transform floor1;
    [SerializeField] private Transform CenterOrLibrary;

    [SerializeField] private Transform BookSlotPrefab;
    [SerializeField] private Transform BookSlotParent;
    
    [Range(0.1f, 0.5f)]
    public float distanceOfBook = 1f;
    public float radius = 0;
    
    void Awake()
    {
        
        if (totalBookCount == 0) return;
        Vector3 _center = CenterOrLibrary.position;
        Vector3 _radiusVector = floor0.GetChild(0).position - _center;
        radius = Mathf.Sqrt(_radiusVector.x * _radiusVector.x + _radiusVector.z * _radiusVector.z);
        
        int _count = 0; // book count
        Bounds _bound;
        float _length;
        int _capacity;
        float _angleDelta;
        Vector3 _direction;
        foreach (Transform row in floor0)
        {
            _bound = row.GetComponent<MeshRenderer>().bounds;
            _length = Mathf.Sqrt(_bound.size.x * _bound.size.x + _bound.size.y * _bound.size.y);
            Debug.Log(_bound.size);
            _capacity = (int)(_length / distanceOfBook);
            Debug.Log(_capacity);
            _angleDelta = Mathf.Asin(_length / radius / 2f) / (float)_capacity * 360 / Mathf.PI;
            Debug.Log(_angleDelta);

            for(int i = 0; i < _capacity; i++)
            {
                Debug.Log(i);
                Transform slot = Instantiate(BookSlotPrefab, BookSlotParent);
                slot.name = row.name + "book" + i;
                slot.position = row.position;
                slot.RotateAround(_center, Vector3.up, _angleDelta * i);
                _direction = new Vector3(_center.x - slot.position.x, 0, _center.z - slot.position.z);
                slot.rotation = Quaternion.LookRotation(_direction);
                _count++;
                if (_count >= totalBookCount) return;
            }
        }
    }
}
