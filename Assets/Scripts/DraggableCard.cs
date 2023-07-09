using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DraggableCard : MonoBehaviour //IBeginDragHandler, IDragHandler, IEndDragHandler
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
    private bool _isPressed;

    [SerializeField]
    private Vector3 _offset;

    [SerializeField]
    private TMP_Text _roomHealthText;

    [SerializeField]
    private string _heroIDText;

    private void Start()
    {
        _cardImage = gameObject.GetComponent<SpriteRenderer>();
        ChooseImage();
        _roomHealth = 1;
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
    }

    public void ChooseImage()
    {
        switch (_cardID)
        {
            case 0:
                _cardImage.sprite = _cardTypeSprite[_cardID];
                break;
            case 1:
                _cardImage.sprite = _cardTypeSprite[_cardID];
                break;
            case 2:
                _cardImage.sprite = _cardTypeSprite[_cardID];
                break;
        }
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

    private void OnMouseDown()
    {
        _isPressed = true;
        Debug.Log("I PrEsSeD iT");
    }

    private void OnMouseUp()
    {
        _isPressed = false;
        Debug.Log("I lEt Go");
        transform.position = parentAfterDrag.position;
        _roomHealthText.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            collision.GetComponent<Hero>().AssignID(_heroID);
            if (_heroID == _cardID)
            {
                collision.GetComponent<Hero>().TakeDamage(_roomHealth);
                _roomHealth--;
                if (_roomHealth <= 0)
                {
                    StartCoroutine(ShakeCard());
                }
            }
            else if (_heroID != _cardID)
            {
                _roomHealth--;
                if (_roomHealth <= 0)
                {
                    StartCoroutine(ShakeCard());
                }
            }
        }
    }
}
