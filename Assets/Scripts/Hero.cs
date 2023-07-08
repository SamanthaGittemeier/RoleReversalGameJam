using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField]
    private int _heroHealth;
    [SerializeField]
    private int _heroSpeed;
    [SerializeField]
    private int _randomID;

    [SerializeField]
    private SpriteRenderer _heroRenderer;

    [SerializeField]
    private Sprite[] _heroType;

    [SerializeField]
    private string _heroID;

    // Start is called before the first frame update
    void Start()
    {
        _heroHealth = 5;
        _heroSpeed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * _heroSpeed * Time.deltaTime);
        if (_heroHealth <= 0)
        {
            Debug.Log("Hero Is Dead");
            _heroSpeed = 0;
        }
    }

    public void AssignID(int _randomChoice)
    {
        _randomID = _randomChoice;
    }

    public void TakeDamage(int Damage)
    {
        _heroHealth -= Damage;
    }

    public void CheckRandomizer()
    {
        _heroRenderer.sprite = _heroType[_randomID];
        switch (_randomID)
        {
            case 0:
                _heroID = "Palladin";
                break;
            case 1:
                _heroID = "Mage";
                break;
            case 2:
                _heroID = "Rogue";
                break;
        }
    }
}
