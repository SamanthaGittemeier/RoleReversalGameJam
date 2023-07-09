using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int _randomChoice;
    [SerializeField]
    private int _upcomingHero;
    [SerializeField]
    private int _spawnNextHero;
    [SerializeField]
    private int _spawnedHeroes;
    [SerializeField]
    private int _waveSize;
    [SerializeField]
    private int _totalSpawned;
    [SerializeField]
    private int _gold;
    [SerializeField]
    private int _score;
    [SerializeField]
    private int _timerSecondsLeft;

    [SerializeField]
    private TMP_Text _goldText;
    [SerializeField]
    private TMP_Text _scoreText;

    [SerializeField]
    private GameObject[] _store;
    [SerializeField]
    private GameObject _heroPrefab;
    [SerializeField]
    private GameObject _boss;

    [SerializeField]
    private Image _nextHeroIcon;

    [SerializeField]
    private Sprite[] _nextHeroImage;

    [SerializeField]
    private Timer _timer;

    void Start()
    {
        _goldText = GameObject.Find("CoinText").GetComponent<TMP_Text>();
        _scoreText = GameObject.Find("Score Text").GetComponent<TMP_Text>();
        _gold = 5;
        _nextHeroIcon = GameObject.Find("NextHero").GetComponent<Image>();
        _spawnedHeroes = 0;
        _timer = GameObject.Find("TimerText").GetComponent<Timer>();
        _boss = GameObject.Find("BossCard");
        _waveSize = 1;
    }

    void Update()
    {
        _goldText.text = _gold.ToString();
        _scoreText.text = _score.ToString();
        _store = GameObject.FindGameObjectsWithTag("Store Item");
        foreach (GameObject SI in _store)
        {
            SI.GetComponent<Store>().UpdateGold(_gold);
        }
        if (_timerSecondsLeft <= 0 && _boss != null)
        {
            SpawnNextHero();
        }
    }

    public void GetRemaingingTimer(int _timeLeft)
    {
        _timerSecondsLeft = _timeLeft;
    }

    public void SpawnNextHero()
    {
        if (_timerSecondsLeft <= 0)
        {
            if (_spawnedHeroes == 0)
            {
                //choose a random one on first wave
                _randomChoice = Random.Range(0, 3);
                Debug.Log(_randomChoice + "is Random Choice");
                //choose a new upcoming hero and set icon
                _upcomingHero = Random.Range(0, 3);
                Debug.Log(_upcomingHero + "is Upcoming Hero Choice");
                _nextHeroIcon.sprite = _nextHeroImage[_upcomingHero];
                //spawn the upcoming hero from there on out
                GameObject _newHero = Instantiate(_heroPrefab, new Vector3(-6, 0.5f, 0), Quaternion.identity);
                _newHero.GetComponent<Hero>().AssignID(_randomChoice);
                //choose a newer upcoming hero
                _spawnedHeroes++;
                _totalSpawned++;
                if (_totalSpawned == _waveSize)
                {
                    Debug.Log(_totalSpawned + "total spawned" + _waveSize + "wave size");
                    _timer.ResetTimer();
                    _waveSize++;
                    _totalSpawned = 0;
                }
            }
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
