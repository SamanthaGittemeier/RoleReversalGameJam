using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int _randomChoice;
    [SerializeField]
    private int _gold;
    [SerializeField]
    private int _score;
    [SerializeField]
    private int _spawnedHeroes;

    [SerializeField]
    private TMP_Text _goldText;
    [SerializeField]
    private TMP_Text _scoreText;

    [SerializeField]
    private GameObject[] _store;
    [SerializeField]
    private GameObject[] _heroes;
    [SerializeField]
    private GameObject _heroPrefab;

    [SerializeField]
    private Image _nextHeroIcon;

    [SerializeField]
    private Sprite[] _nextHeroImage;

    // Start is called before the first frame update
    void Start()
    {
        _goldText = GameObject.Find("CoinText").GetComponent<TMP_Text>();
        _scoreText = GameObject.Find("Score Text").GetComponent<TMP_Text>();
        _gold = 5;
        _nextHeroIcon = GameObject.Find("NextHero").GetComponent<Image>();
        _spawnedHeroes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _goldText.text = _gold.ToString();
        _scoreText.text = _score.ToString();
        _store = GameObject.FindGameObjectsWithTag("Store Item");
        _heroes = GameObject.FindGameObjectsWithTag("Hero");
        foreach (GameObject SI in _store)
        {
            SI.GetComponent<Store>().UpdateGold(_gold);
        }
    }

    public void SpawnNextHero()
    {
        if (_heroes.Length == 0)
        {
            _randomChoice = Random.Range(0, 3);
            GameObject _newHero = Instantiate(_heroPrefab, new Vector3(-6, 0.5f, 0), Quaternion.identity);
            _newHero.GetComponent<Hero>().CheckRandomizer();
            _spawnedHeroes++;
        }
    }

    public void HeroDied()
    {
        _spawnedHeroes--;
    }

    public void AddGold(int _gainedGold)
    {
        _gold += _gainedGold;
    }

    public void SubtractGold(int _lostGold)
    {
        _gold -= _lostGold;
    }

    public void UpdateScore(int _gainScore)
    {
        _score += _gainScore;
    }

    public void Escape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit Game!");
            Application.Quit();
        }
    }
}
