using System;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class SwapHandler : MonoBehaviour
{
    [SerializeField] private GridManager grid;
    private Camera mainCamera;
    private Slot oldSlot;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            GetCoin(mousePos);
        }
    }

    public void GetCoin(Vector2 mousePos)
    {
        if (grid.GetSlotOnGrid(mousePos) != null)
        {
            Slot slot = grid.GetSlotOnGrid(mousePos);
            if (oldSlot != null && oldSlot != slot)
            {
                Coin coin = oldSlot.GetTopCoin();
                if (slot.TryFillCoinToSlot(coin))
                {
                    oldSlot?.RemoveCoin();
                }
                coin?.Deselected();
                oldSlot = null;
            }
            else
            if(oldSlot == null)
            {
                oldSlot = slot;
                oldSlot.GetTopCoin()?.Selected();
            }
            else
            {
                oldSlot.GetTopCoin()?.Deselected();
                oldSlot = null;
            }
            return;
        }
        oldSlot?.GetTopCoin()?.Deselected();
        oldSlot = null;
    }
}