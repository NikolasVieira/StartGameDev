using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class SlotFarm : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite crop;

    [Header("Settings")]
    [SerializeField] private int digAmount;
    [SerializeField] private float waterAmount;
    
    [SerializeField] private bool detecting;

    private int initialDigAmount;
    private float currentWater;

    private bool dugHole;
    SCR_PLAYER_ITEMS PlayerItems;

    private void Start()
    {
        PlayerItems = FindObjectOfType<SCR_PLAYER_ITEMS>();
        initialDigAmount = digAmount;
    }

    private void Update()
    {
        if (dugHole)
        {
            if (detecting)
            {
                currentWater += 0.01f;
            }

            if (currentWater >= waterAmount)
            {
                spriteRenderer.sprite = crop;

                if (UnityEngine.Input.GetKeyDown(KeyCode.E))
                {
                    spriteRenderer.sprite = hole;
                    PlayerItems.propCurrentCrop++;
                    currentWater = 0;
                }
            }
        }
    }

    public void OnHit()
    {
        digAmount--;

        if (digAmount <= initialDigAmount / 2)
        {
            spriteRenderer.sprite = hole;
            dugHole = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dig"))
        {
            OnHit();
        }
        if (collision.CompareTag("Water"))
        {
            detecting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            detecting = false;
        }
    }
}
