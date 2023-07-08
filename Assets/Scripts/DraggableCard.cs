using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    private bool _isPressed;

    [SerializeField]
    private Vector3 _offset;

    private void Start()
    {
        _cardImage = gameObject.GetComponent<SpriteRenderer>();
        ChooseImage();
        _roomHealth = 5;
    }

    private void Update()
    {
        IsPressed();
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the tag is palladin and the card type is the specific room then deal damage
        if (collision.tag == "Palladin" && _cardID == 0)
        {
            collision.GetComponent<Hero>().TakeDamage(_roomHealth);
            _roomHealth--;
        }
        if (collision.tag == "Rogue" && _cardID == 1)
        {
            collision.GetComponent<Hero>().TakeDamage(_roomHealth);
            _roomHealth--;
        }
        if (collision.tag == "Mage" && _cardID == 2)
        {
            collision.GetComponent<Hero>().TakeDamage(_roomHealth);
            _roomHealth--;
        }
        else if (collision.tag == "Palladin" && _cardID != 0 || collision.tag == "Rogue" && _cardID != 1 || collision.tag == "Mage" && _cardID != 2)
        {
            _roomHealth--;
        }
    }
}
