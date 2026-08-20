using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector2Int Size;
    [field:SerializeField] public Vector2 cellSize { get; private set; }
    [SerializeField] private Vector2 cellDistance;
    [SerializeField] private Vector2 originPosition;
    [SerializeField] private Slot slotPrefab;
    private Camera mainCam;
    private Grid<Slot> grid;

    private void Start()
    {
        grid = new Grid<Slot>(Size, (Vector2Int pos) =>
        {
            Slot slot = Instantiate(slotPrefab);
            slot.InitSlot(pos,cellSize.y/10);
            return slot;
        });
    }
    
    public Slot GetSlotOnGrid(Vector2 worldPosition)
    {
        if (TryGetValidCell(worldPosition, out Vector2Int gridPos))
        {
            return grid.GetElementAtPoint(gridPos);
        }
        return null;
    }
    
    public Vector2 GridToWorldPosition(Vector2Int gridPos)
    {
        Vector2 step = cellSize + cellDistance;
        Vector2 realOrigin = GetBottomLeftOrigin();
        Vector2 cellCenter = new Vector2(gridPos.x, gridPos.y) * step + realOrigin;
        return cellCenter;
    }
    
    public bool TryGetValidCell(Vector2 worldPosition, out Vector2Int gridPos)
    {
        Vector2 step = cellSize + cellDistance;
        Vector2 gridOrigin = GetBottomLeftOrigin();
        Vector2 localPosition = worldPosition - gridOrigin;
        int x = Mathf.RoundToInt(localPosition.x / step.x);
        int y = Mathf.RoundToInt(localPosition.y / step.y);
        gridPos = new Vector2Int(x, y);
        if (x < 0 || x >= Size.x || y < 0 || y >= Size.y) return false;
        Vector2 cellCenter = gridOrigin + new Vector2(x, y) * step;
        bool isInsideX = Mathf.Abs(worldPosition.x - cellCenter.x) <= cellSize.x / 2f;
        bool isInsideY = Mathf.Abs(worldPosition.y - cellCenter.y) <= cellSize.y / 2f;
        return isInsideX && isInsideY;
    }
    
    public Vector2 GetBottomLeftOrigin()
    {
        Vector2 step = cellSize + cellDistance;
        Vector2 offSet = new Vector2(-0.5f * step.x * (Size.x - 1), -0.5f * step.y * (Size.y - 1));
        return originPosition + offSet;
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (mainCam == null) mainCam = Camera.main;
        Handles.color = Color.red;
        Vector2 step = cellSize + cellDistance;
      Vector2 originPosition = GetBottomLeftOrigin();
        for (int x = 0; x < Size.x; x++)
        for (int y = 0; y < Size.y; y++)
        {
            Vector2 center = new Vector2(x, y) * step + originPosition;
            Handles.DrawWireCube(center, cellSize);
        }
    }
#endif
}