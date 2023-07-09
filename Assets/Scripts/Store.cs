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
    private int _goldCost;

    [SerializeField]
    private GameObject _roomPrefab;

    [SerializeField]
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        if (_timer > 0 && _currentGold >= _goldCost)
        {
            GameObject _newRoom = Instantiate(_roomPrefab, transform.position, Quaternion.identity);
            _newRoom.GetComponent<DraggableCard>().GetTimer(_timer);
            _newRoom.GetComponent<DraggableCard>().AssignCardID(_cardTypeID);
            _newRoom.GetComponent<DraggableCard>().OnMouseDown();
            _gameManager.SubtractGold(_goldCost);
        }
    }
}
