using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField]
    private int _cardTypeID;
    [SerializeField]
    private int _currentGold;
    [SerializeField]
    private int _timer;

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

    public void TimerAmount(int _timerCount)
    {
        _timer = _timerCount;
    }

    public void UpdateGold(int _goldAmount)
    {
        _currentGold = _goldAmount;
    }

    private void OnMouseDown()
    {
        if (_timer > 0)
        {
            GameObject _newRoom = Instantiate(_roomPrefab, transform.position, Quaternion.identity);
            _newRoom.GetComponent<DraggableCard>().GetTimer(_timer);
            _newRoom.GetComponent<DraggableCard>().AssignCardID(_cardTypeID);
            _newRoom.GetComponent<DraggableCard>().OnMouseDown();
        }
    }
}
