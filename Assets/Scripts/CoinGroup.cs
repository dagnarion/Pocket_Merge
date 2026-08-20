using System.Collections.Generic;
using UnityEngine.Rendering.VirtualTexturing;

public struct CoinGroup
{
    public Coin[] coins { get; private set; }

    public CoinGroup(int size)
    {
        coins = new Coin[size];
    }
    
    public void SetCoin(int index, Coin coin)
    {
        if (index >= coins.Length) return;
        coins[index] = coin;
    }

    public CoinGroup Union(CoinGroup otherGroup)
    {
        int size = coins.Length + otherGroup.coins.Length;
        CoinGroup coinGroup = new CoinGroup(size);
        int index = 0;
        for (int i = 0; i < coins.Length; i++)
        {
            coinGroup.SetCoin(index++, coins[i]);
        }

        for (int i = 0; i < otherGroup.coins.Length; i++)
        {
            coinGroup.SetCoin(index++, otherGroup.coins[i]);
        }

        return coinGroup;
    }
}