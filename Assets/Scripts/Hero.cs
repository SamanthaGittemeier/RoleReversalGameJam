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
    private int _giveGold;
    [SerializeField]
    private int _givePoints;

    [SerializeField]
    private SpriteRenderer _heroRenderer;

    [SerializeField]
    private Sprite[] _heroType;

    [SerializeField]
    private string _heroID;

    [SerializeField]
    private GameObject _bloodSplatPrefab;

    [SerializeField]
    private Animator _heroDeathAnimation;

    [SerializeField]
    private GameManager _gameManager;

    [SerializeField]
    private Boss _boss;

    // Start is called before the first frame update
    void Start()
    {
        _heroHealth = 5;
        _heroSpeed = 3;
        _heroDeathAnimation = gameObject.GetComponent<Animator>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _giveGold = 5;
        _givePoints = 1;
        _boss = GameObject.Find("BossCard").GetComponent<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * _heroSpeed * Time.deltaTime);
    }

    public void OnHeroDeath()
    {
        Debug.Log("Hero Is Dead");
        _heroSpeed = 0;
        _heroDeathAnimation.SetBool("HeroDied", true);
        GameObject _blood = Instantiate(_bloodSplatPrefab, transform.position + new Vector3(-0.5f, -0.3f, 0), Quaternion.identity);
        _gameManager.AddGold(_giveGold);
        _gameManager.UpdateScore(_givePoints);
        Destroy(_blood, 1.1f);
        Destroy(gameObject, 1.5f);
    }

    public void AssignID(int _randomChoice)
    {
        _randomID = _randomChoice;
    }

    public void TakeDamage(int Damage)
    {
        _heroHealth -= Damage;
        if (_heroHealth <= 0)
        {
            OnHeroDeath();
        }
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
                _heroID = "Rogue";
                break;
            case 2:
                _heroID = "Mage";
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boss")
        {
            _heroSpeed = 0;
            Debug.Log("At Boss");
            _boss.TakeDamage(_heroHealth);
        }
    }
}
