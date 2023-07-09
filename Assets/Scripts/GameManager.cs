using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int _randomChoice;
    [SerializeField]
    private int _gold;
    [SerializeField]
    private int _score;

    [SerializeField]
    private TMP_Text _goldText;
    [SerializeField]
    private TMP_Text _scoreText;

    [SerializeField]
    private GameObject[] _store;

    // Start is called before the first frame update
    void Start()
    {
        _goldText = GameObject.Find("CoinText").GetComponent<TMP_Text>();
        _scoreText = GameObject.Find("Score Text").GetComponent<TMP_Text>();
        _gold = 5;
    }

    // Update is called once per frame
    void Update()
    {
        _goldText.text = _gold.ToString();
        _scoreText.text = _score.ToString();
        _store = GameObject.FindGameObjectsWithTag("Store Item");
        foreach (GameObject SI in _store)
        {
            SI.GetComponent<Store>().UpdateGold(_gold);
        }
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
