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
    private int _upcomingHero;
    [SerializeField]
    private int _gold;
    [SerializeField]
    private int _score;
    [SerializeField]
    private int _spawnedHeroes;
    [SerializeField]
    private int _waveSize;
    [SerializeField]
    private int _timerSecondsLeft;

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
    private GameObject _boss;

    [SerializeField]
    private Image _nextHeroIcon;

    [SerializeField]
    private Sprite[] _nextHeroImage;

    [SerializeField]
    private Timer _timer;

    [SerializeField]
    private bool _firstWave;
    [SerializeField]
    private bool _timerUp;

    // Start is called before the first frame update
    void Start()
    {
        _goldText = GameObject.Find("CoinText").GetComponent<TMP_Text>();
        _scoreText = GameObject.Find("Score Text").GetComponent<TMP_Text>();
        _gold = 5;
        _nextHeroIcon = GameObject.Find("NextHero").GetComponent<Image>();
        _spawnedHeroes = 0;
        _timer = GameObject.Find("TimerText").GetComponent<Timer>();
        _waveSize = 1;
        _boss = GameObject.Find("BossCard");
        _firstWave = true;
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
        if (_timerSecondsLeft <= 0)
        {
            _timerUp = true;
        }
    }

    public void GetRemaingingTimer(int _timeLeft)
    {
        _timerSecondsLeft = _timeLeft;
    }

    public void SpawnNextHero()
    {
        if (_boss != null)
        {
            if (_timerUp == true)
            {
                if (_firstWave == true)
                {
                    _randomChoice = Random.Range(0, 3);
                    GameObject _newHero = Instantiate(_heroPrefab, new Vector3(-6, 0.5f, 0), Quaternion.identity);
                    _upcomingHero = Random.Range(0, 3);
                    _nextHeroIcon.sprite = _nextHeroImage[_upcomingHero];
                    _newHero.GetComponent<Hero>().AssignID(_randomChoice);
                    _spawnedHeroes++;
                    _firstWave = false;
                }
                else if (_firstWave == false && _heroes.Length == 0 && _spawnedHeroes != _waveSize)
                {
                    _randomChoice = _upcomingHero;
                    GameObject _newHero = Instantiate(_heroPrefab, new Vector3(-6, 0.5f, 0), Quaternion.identity);
                    _upcomingHero = Random.Range(0, 3);
                    _nextHeroIcon.sprite = _nextHeroImage[_upcomingHero];
                    _newHero.GetComponent<Hero>().AssignID(_randomChoice);
                    _spawnedHeroes++;
                }
                //else if the heroes array is empty and the spawned heroes is equal to the wave size
                else if (_heroes.Length == 0 && _spawnedHeroes == _waveSize)
                {
                    //reset the timer
                    _timer.ResetTimer();
                    //set new wave size to be same as before plus one
                    _waveSize = _waveSize++;
                    //call to spawn again
                    SpawnNextHero();
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
