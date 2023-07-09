using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DraggableCard : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    [SerializeField]
    private Transform parentAfterDrag;

    [SerializeField]
    private Sprite[] _cardTypeSprite;

    [SerializeField]
    private SpriteRenderer _cardImage;

    [SerializeField]
    private int _cardID;
    [SerializeField]
    private int _roomHealth;
    [SerializeField]
    private int _heroID;
    [SerializeField]
    private int _timerAmount;

    [SerializeField]
    private bool _isPressed;

    [SerializeField]
    private Vector3 _offset;

    [SerializeField]
    private TMP_Text _roomHealthText;

    [SerializeField]
    private string _heroIDText;

    [SerializeField]
    private Timer _timer;

    [SerializeField]
    private AudioSource _roomPlayer;

    [SerializeField]
    private AudioClip[] _clips;

    private void Start()
    {
        _cardImage = gameObject.GetComponent<SpriteRenderer>();
        ChooseImage();
        _roomHealth = 5;
        _roomPlayer = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        IsPressed();
        if (_roomHealthText != null)
        {
            _roomHealthText.text = _roomHealth.ToString();
        }

        if (_roomHealth <= 0)
        {
            transform.Translate(Vector3.down * 3 * Time.deltaTime);
            if (transform.position.y <= -7)
            {
                Destroy(this.gameObject);
            }
        }

        if (_timerAmount <= 0)
        {
            OnMouseUp();
        }
    }

    public void GetTimer(int _timerSeconds)
    {
        _timerAmount = _timerSeconds;
    }

    public void AssignCardID(int _storeID)
    {
        _cardID = _storeID;
    }

    public void ChooseImage()
    {
        _cardImage.sprite = _cardTypeSprite[_cardID];
    }

    private void IsPressed()
    {
        if (_isPressed == true)
        {
            Vector2 MousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPosition = Camera.main.ScreenToWorldPoint(MousePosition);
            transform.position = objPosition;
        }
    }

    IEnumerator ShakeCard()
    {
        transform.Rotate(new Vector3(0, 0, -7) * 5 * Time.deltaTime);
        yield return new WaitForSeconds(.1f);
        transform.Rotate(new Vector3(0, 0, 7) * 5 * Time.deltaTime);
        yield return new WaitForSeconds(.1f);
        StartCoroutine(ShakeCard());
    }

    public void AssignText(TMP_Text _text)
    {
        _roomHealthText = _text;
    }

    public void SetTransform(Transform _slotTransform)
    {
        parentAfterDrag = _slotTransform;
    }

    public void OnMouseDown()
    {
        if (_timerAmount > 0)
        {
            _isPressed = true;
            if (_roomHealthText != null)
            {
                _roomHealthText.gameObject.SetActive(false);
            }
            if (parentAfterDrag != null)
            {
                parentAfterDrag.gameObject.SetActive(true);
            }
        }
    }

    public void OnMouseUp()
    {
        _isPressed = false;
        if (parentAfterDrag != null)
        {
            transform.position = parentAfterDrag.position;
            parentAfterDrag.gameObject.SetActive(false);
            _roomHealthText.gameObject.SetActive(true);
            if (_cardID != 3)
            {
                _roomPlayer.clip = _clips[4];
                _roomPlayer.Play();
            }
            else if (_cardID == 3)
            {
                _roomPlayer.clip = _clips[3];
                _roomPlayer.Play();
                Destroy(this.gameObject, 1f);
            }
        }
    }

    private void CheckRoomHealth()
    {
        if (_roomHealth <= 0)
        {
            StartCoroutine(ShakeCard());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            _roomPlayer.clip = _clips[_cardID];
            _roomPlayer.Play();
            collision.GetComponent<Hero>().AssignID(_heroID);
            if (_heroID == _cardID)
            {
                collision.GetComponent<Hero>().TakeDamage(_roomHealth);
                _roomHealth--;
            }
            else if (_heroID != _cardID)
            {
                _roomHealth--;
                if (_heroID == 0 && _cardID == 4 || _heroID == 0 && _cardID == 5)
                {
                    collision.GetComponent<Hero>().TakeDamage(_roomHealth);
                    _roomHealth--;
                }
                if (_heroID == 1 && _cardID == 4 || _heroID == 1 && _cardID == 6)
                {
                    collision.GetComponent<Hero>().TakeDamage(_roomHealth);
                    _roomHealth--;
                }
                if (_heroID == 2 && _cardID == 5 || _heroID == 2 && _cardID == 6)
                {
                    collision.GetComponent<Hero>().TakeDamage(_roomHealth);
                    _roomHealth--;
                }
            }
            CheckRoomHealth();
        }

        if (collision.tag == "Card" && _isPressed == false)
        {
            if (_cardID == 3)
            {
                collision.GetComponent<DraggableCard>()._roomHealth++;
                _roomPlayer.clip = _clips[3];
                _roomPlayer.Play();
            }
            if (_cardID != collision.GetComponent<DraggableCard>()._cardID)
            {
                Debug.Log(_cardID + "combined with" + collision.GetComponent<DraggableCard>()._cardID);
                if (this.gameObject.GetComponent<DraggableCard>()._cardID == 0)
                {
                    if (collision.GetComponent<DraggableCard>()._cardID == 1)
                    {
                        Destroy(this.gameObject);
                        collision.GetComponent<DraggableCard>()._cardImage.sprite = _cardTypeSprite[4];
                        collision.GetComponent<DraggableCard>()._cardID = 4;
                        parentAfterDrag.gameObject.SetActive(true);
                    }
                    if (collision.GetComponent<DraggableCard>()._cardID == 2)
                    {
                        Destroy(this.gameObject);
                        collision.GetComponent<DraggableCard>()._cardImage.sprite = _cardTypeSprite[5];
                        collision.GetComponent<DraggableCard>()._cardID = 5;
                        parentAfterDrag.gameObject.SetActive(true);
                    }
                }
                if (this.gameObject.GetComponent<DraggableCard>()._cardID == 1)
                {
                    if (collision.GetComponent<DraggableCard>()._cardID == 0)
                    {
                        Destroy(this.gameObject);
                        collision.GetComponent<DraggableCard>()._cardImage.sprite = _cardTypeSprite[4];
                        collision.GetComponent<DraggableCard>()._cardID = 4;
                        parentAfterDrag.gameObject.SetActive(true);
                    }
                    if (collision.GetComponent<DraggableCard>()._cardID == 2)
                    {
                        Destroy(this.gameObject);
                        collision.GetComponent<DraggableCard>()._cardImage.sprite = _cardTypeSprite[6];
                        collision.GetComponent<DraggableCard>()._cardID = 6;
                        parentAfterDrag.gameObject.SetActive(true);
                    }
                }
                if (this.gameObject.GetComponent<DraggableCard>()._cardID == 2)
                {
                    if (collision.GetComponent<DraggableCard>()._cardID == 0)
                    {
                        Destroy(this.gameObject);
                        collision.GetComponent<DraggableCard>()._cardImage.sprite = _cardTypeSprite[5];
                        collision.GetComponent<DraggableCard>()._cardID = 5;
                        parentAfterDrag.gameObject.SetActive(true);
                    }
                    if (collision.GetComponent<DraggableCard>()._cardID == 1)
                    {
                        Destroy(this.gameObject);
                        collision.GetComponent<DraggableCard>()._cardImage.sprite = _cardTypeSprite[6];
                        collision.GetComponent<DraggableCard>()._cardID = 6;
                        parentAfterDrag.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
