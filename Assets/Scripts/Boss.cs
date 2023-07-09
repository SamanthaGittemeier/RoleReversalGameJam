using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private int _bossHealth;

    [SerializeField]
    private TMP_Text _bossHealthText;

    // Start is called before the first frame update
    void Start()
    {
        _bossHealth = 10;
        _bossHealthText = GameObject.Find("BossHealth").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _bossHealthText.text = _bossHealth.ToString();
    }

    public void TakeDamage(int _damageAmount)
    {
        _bossHealth -= _damageAmount;
    }
}
