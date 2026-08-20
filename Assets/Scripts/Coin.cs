using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    private SpriteRenderer sprite;
    [field:SerializeField] public int ID { get; private set; }
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        switch (ID)
        {
            case 1:
                sprite.color = Color.red;
                break;
            case 2:
                sprite.color = Color.blue;
                break;
            case 3:
                sprite.color = Color.yellow;
                break;
            case 4:
                sprite.color = Color.green;
                break;
            case 5:
                sprite.color = Color.purple;
                break;
            case 6: 
                sprite.color = Color.deepPink;
                break;
            default:
                sprite.color = Color.white;
                break;
        }
    }

    //test
    public void Selected()
    {
        sprite.color = Color.brown;
    }

    public void Deselected()
    {
        switch (ID)
        {
            case 1:
                sprite.color = Color.red;
                break;
            case 2:
                sprite.color = Color.blue;
                break;
            case 3:
                sprite.color = Color.yellow;
                break;
            case 4:
                sprite.color = Color.green;
                break;
            case 5:
                sprite.color = Color.purple;
                break;
            case 6: 
                sprite.color = Color.deepPink;
                break;
            default:
                sprite.color = Color.white;
                break;
        }
    }

    public void SetPosition(Vector2 position)
    {
        this.transform.position = position;
    }
    
}
