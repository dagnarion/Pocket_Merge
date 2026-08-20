using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Slot : MonoBehaviour
{
   [SerializeField] private GridManager gridManager; // test
   private const int capacity = 10;
   public SlotType SlotStatus { get; private set; } = SlotType.Nozmal;
   private Vector2[] coinHolderPosition = new Vector2[10];
   [SerializeField] private Coin[] coin;
   private List<Coin> coins;

   public Coin GetTopCoin()
   {
      if (coins.Count == 0)
      {
         Debug.LogWarning("There was an empty Slot");
         return null;
      }
      return coins[coins.Count - 1];
   }

   public void RemoveCoin()
   {
      if(coins.Count == 0) {Debug.LogWarning("There was an empty Slot"); return;}
      coins.RemoveAt(coins.Count-1);
   }

   public void InitSlot(Vector2Int SlotPosition,float coinSize)
   {
      coins = new List<Coin>();
      for (int i = 0; i < 10; i++)
      {
         coinHolderPosition[i] = gridManager.GridToWorldPosition(SlotPosition);
         coinHolderPosition[i].y += gridManager.cellSize.y / 2;
         coinHolderPosition[i].y -= i * coinSize + coinSize/2;
      }
      for(int i = 0;i<coin.Length;i++)
         TryFillCoinToSlot(Instantiate(coin[i]));
   }

   public bool TryFillCoinToSlot(Coin coin)
   {
         if(coin == null || coins.Count >= capacity) return false;
         coins.Add(coin);
         coin.SetPosition(coinHolderPosition[coins.Count-1]);
         return true;
   }

   public bool CanMerge()
   {
      if (coins.Count == capacity)
      {
         int ID = coins[0].ID;
         for (int i = 0; i < coins.Count; i++)
         {
            if (ID != coins[i].ID) return false;
         }
         return true;
      }
      return false;
   }
}

public enum SlotType
{
   Nozmal = 0,
   Lock = 1,
   Tempory = 2
}
