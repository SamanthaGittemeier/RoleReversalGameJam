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

    [SerializeField]
    private Animator _bossAnim;

    [SerializeField]
    private AudioSource _audioPlayer;

    [SerializeField]
    private AudioClip[] _audioClips;

    // Start is called before the first frame update
    void Start()
    {
        _bossHealth = 10;
        _bossHealthText = GameObject.Find("BossHealth").GetComponent<TMP_Text>();
        _bossAnim = gameObject.GetComponent<Animator>();
        _audioPlayer = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _bossHealthText.text = _bossHealth.ToString();
    }

    public void TakeDamage(int _damageAmount)
    {
        _bossHealth -= _damageAmount;
        if (_bossHealth <= 0)
        {
            _bossAnim.SetBool("BossDead", true);
            _audioPlayer.clip = _audioClips[0];
            _audioPlayer.Play();
            Destroy(this.gameObject, 2.5f);
        }
        else if (_bossHealth > 0)
        {
            _audioPlayer.clip = _audioClips[1];
            _audioPlayer.Play();
            StartCoroutine(ResetDamageAnimation());
        }
    }

    IEnumerator ResetDamageAnimation()
    {
        _bossAnim.SetBool("BossDamage", true);
        yield return new WaitForSeconds(0.5f);
        _bossAnim.SetBool("BossDamage", false);
    }
}
