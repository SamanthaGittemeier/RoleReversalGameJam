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
    private TMP_Text _goldText;

    // Start is called before the first frame update
    void Start()
    {
        _goldText = GameObject.Find("CoinText").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _goldText.text = _gold.ToString();
    }

    public void AddGold(int _gainedGold)
    {
        _gold += _gainedGold;
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
