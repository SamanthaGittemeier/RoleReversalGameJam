using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField]
    private int _heroHealth;
    [SerializeField]
    private int _heroSpeed;

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
    }

    public void TakeDamage(int Damage)
    {
        _heroHealth -= Damage;
    }
}
