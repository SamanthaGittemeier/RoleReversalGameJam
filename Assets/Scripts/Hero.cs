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
  private GameObject _bloodSplatPrefab;

  [SerializeField]
  private Animator _heroDeathAnimation;

  [SerializeField]
  private GameManager _gameManager;

  [SerializeField]
  private Boss _boss;

  [SerializeField]
  private AudioSource _audioPlayerHero;

  [SerializeField]
  private AudioClip[] _clipChoice;

  // Start is called before the first frame update
  void Start()
  {
    _heroHealth = 5;
    _heroSpeed = 1;
    _heroDeathAnimation = gameObject.GetComponent<Animator>();
    _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    _giveGold = 5;
    _givePoints = 1;
    _boss = GameObject.Find("BossCard").GetComponent<Boss>();
    _audioPlayerHero = gameObject.GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update()
  {
    transform.Translate(Vector3.right * _heroSpeed * Time.deltaTime);
  }

  public void OnHeroDeath()
  {
    Debug.Log("Hero Is Dead");
    _audioPlayerHero.clip = _clipChoice[0];
    _audioPlayerHero.Play();
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
        else if (_heroHealth > 0)
        {
            _audioPlayerHero.clip = _clipChoice[1];
            _audioPlayerHero.Play();
        }
  }

    public void OnHeroDeath()
    {
        Debug.Log("Hero Is Dead");
        _audioPlayerHero.clip = _clipChoice[0];
        _audioPlayerHero.Play();
        _heroSpeed = 0;
        _heroDeathAnimation.SetBool("HeroDied", true);
        GameObject _blood = Instantiate(_bloodSplatPrefab, transform.position + new Vector3(-0.5f, -0.3f, 0), Quaternion.identity);
        _gameManager.AddGold(_giveGold);
        _gameManager.UpdateScore(_givePoints);
        _gameManager.HeroDied();
        Destroy(_blood, 1.1f);
        Destroy(gameObject, 1.5f);
    }

  public void CheckRandomizer()
  {
    _heroRenderer.sprite = _heroType[_randomID];
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "Boss")
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
        else if (_heroHealth > 0)
        {
            _audioPlayerHero.clip = _clipChoice[1];
            _audioPlayerHero.Play();
        }
    }

    public void CheckRandomizer()
    {
        _heroRenderer.sprite = _heroType[_randomID];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boss")
        {
            _heroSpeed = 0;
            Debug.Log("At Boss");
            _boss.TakeDamage(_heroHealth);
        }

      _heroSpeed = 0;
      Debug.Log("At Boss");
      _boss.TakeDamage(_heroHealth);
      Destroy(this.gameObject, 2.5f);
    }
  }
}
