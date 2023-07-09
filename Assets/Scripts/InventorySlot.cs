using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _grabText;

    private void Start()
    {
        _grabText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Card")
        {
            collision.GetComponent<DraggableCard>().SetTransform(transform);
            collision.GetComponent<DraggableCard>().AssignText(_grabText);
        }
    }
}
