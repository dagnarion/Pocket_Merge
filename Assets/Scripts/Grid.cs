using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Grid<T>
{
    private T[,] grids;
    private Vector2Int Size;
    
    public Grid(Vector2Int Size,Func<Vector2Int,T> action)
    {
        grids = new T[Size.x, Size.y];
        this.Size = Size;
        
        for(int x = 0;x<Size.x;x++)
        for (int y = 0; y < Size.y; y++)
           SetElementAtPoint(new Vector2Int(x,y),action(new Vector2Int(x,y)));
    }
    
    public void GridTraversal(Action<Vector2Int,T> action)
    {
        for(int i = 0;i<Size.x;i++)
            for(int j = 0;j<Size.y;j++)
                action?.Invoke(new Vector2Int(i,j),grids[i,j]);
    }

    public T GetElementAtPoint(Vector2Int pos)
    {
        if (!IsOnRange(pos)) return default;
        return grids[pos.x, pos.y];
    }

    public void SetElementAtPoint(Vector2Int pos,T value)
    {
        if (!IsOnRange(pos)) return;
        grids[pos.x, pos.y] = value;
    }

    public bool IsOnRange(Vector2Int pos)
    {
        if (pos.x < 0 || pos.x >= Size.x || pos.y < 0 || pos.y >= Size.y)
        {
            Debug.LogWarning(pos+ " Out Of Range ");
            return false;
        }
        return true;
    }
    
}