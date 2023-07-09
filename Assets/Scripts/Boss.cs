using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

    void Start()
    {
        _bossHealth = 10;
        _bossHealthText = GameObject.Find("BossHealth").GetComponent<TMP_Text>();
        _bossAnim = gameObject.GetComponent<Animator>();
        _audioPlayer = gameObject.GetComponent<AudioSource>();
        _bossHealthText.text = _bossHealth.ToString();
    }

    void Update()
    {
        
    }

    IEnumerator LoadEndScene()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("EndGame");
    }

    public void TakeDamage(int _damageAmount)
    {
        _bossHealth -= _damageAmount;
        _bossHealthText.text = _bossHealth.ToString();
        if (_bossHealth <= 0)
        {
            _bossAnim.SetBool("BossDead", true);
            _audioPlayer.clip = _audioClips[0];
            _audioPlayer.Play();
            StartCoroutine(LoadEndScene());
            Destroy(this.gameObject, 3f);
        }
        else if (_bossHealth > 0)
        {
            Debug.Log("Updating Health");
            _bossHealthText.text = _bossHealth.ToString();
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
