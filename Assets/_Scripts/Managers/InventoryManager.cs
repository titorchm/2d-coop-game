using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private const float INVENTORY_SLOTS_START_POSITION_X = 880f;
    private const float INVENTORY_SLOTS_START_POSITION_Y = 0f;
    
    private const float ACTIVE_WEAPONS_START_POSITION_X = -890f;
    private const float ACTIVE_WEAPONS_START_POSITION_Y = 220f;
    
    public PlayerInput playerInput;
    
    [SerializeField] private RectTransform inventorySlots;
    [SerializeField] private RectTransform activeWeapons;

    private List<IItem> _itemSlots = new();
    
    private bool _inventoryOpen = false;
    private float _inventoryToggleDuration = .5f;
    
    private Coroutine _inventoryToggleCoroutine;

    private void Awake()
    {
        inventorySlots.anchoredPosition = new Vector2(INVENTORY_SLOTS_START_POSITION_X * 2, INVENTORY_SLOTS_START_POSITION_Y);
        activeWeapons.anchoredPosition = new Vector2(ACTIVE_WEAPONS_START_POSITION_X * 2, ACTIVE_WEAPONS_START_POSITION_Y);
    }

    private void Start()
    {
        playerInput.OnInventoryToggled += OnInventoryToggled;
    }

    public void AddToInventory(object data)
    {
        var go = (GameObject)data;
        
        var collectable = go.GetComponent<ICollectable>();
        
        UpdateInventorySlots(collectable.Item);
    }
    
    private void OnInventoryToggled()
    {
        if (_inventoryToggleCoroutine != null)
        {
            StopCoroutine(_inventoryToggleCoroutine);
        }
        
        if (!_inventoryOpen)
        {
            _inventoryOpen = true;
            
            _inventoryToggleCoroutine = StartCoroutine(ToggleInventory(
                new Vector2(INVENTORY_SLOTS_START_POSITION_X, INVENTORY_SLOTS_START_POSITION_Y),
                new Vector2(ACTIVE_WEAPONS_START_POSITION_X, ACTIVE_WEAPONS_START_POSITION_Y)
            ));
        }
        else
        {
            _inventoryOpen = false;
            
            _inventoryToggleCoroutine = StartCoroutine(ToggleInventory(
                new Vector2(INVENTORY_SLOTS_START_POSITION_X * 2, INVENTORY_SLOTS_START_POSITION_Y),
                new Vector2(ACTIVE_WEAPONS_START_POSITION_X * 2, ACTIVE_WEAPONS_START_POSITION_Y)
            ));
        }
    }

    private IEnumerator ToggleInventory(Vector2 targetPositionIS, Vector2 targetPositionAW)
    {
        float elapsedTime = 0f;

        Vector2 startIS = inventorySlots.anchoredPosition;
        Vector2 startAW = activeWeapons.anchoredPosition;

        while (elapsedTime < _inventoryToggleDuration)
        {
            inventorySlots.anchoredPosition = Vector2.Lerp(startIS, targetPositionIS, EaseOutCubic(elapsedTime) / _inventoryToggleDuration);
            activeWeapons.anchoredPosition = Vector2.Lerp(startAW, targetPositionAW, EaseOutCubic(elapsedTime) / _inventoryToggleDuration);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        inventorySlots.anchoredPosition = targetPositionIS;
        activeWeapons.anchoredPosition = targetPositionAW;
    }
    
    private float EaseOutCubic(float x)
    {
        return -(MathF.Cos(MathF.PI * x) - 1) / 2;
    }

    private void UpdateInventorySlots(IItem item)
    {
        _itemSlots.Add(item);
        
        Debug.Log(item.Name + " added to Inventory");
    }
}
