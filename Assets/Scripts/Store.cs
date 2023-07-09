using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField]
    private int _cardTypeID;

    [SerializeField]
    private GameObject _roomPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        GameObject _newRoom = Instantiate(_roomPrefab, transform.position, Quaternion.identity);
        _newRoom.GetComponent<DraggableCard>().AssignCardID(_cardTypeID);
        _newRoom.GetComponent<DraggableCard>().WasSpawned();
    }
}
