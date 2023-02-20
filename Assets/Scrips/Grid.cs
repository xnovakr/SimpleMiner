using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    private int width;
    private int height;
    private int startX;
    private int startY;
    private float cellSize;
    private int chunks;

    private Vector3 originPosition;

    private int[,] gridArray;

    public Grid(int width, int height, float cellSize, int startX, int startY, int chunks)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.startX = startX;
        this.startY = startY;
        this.chunks = chunks;

        gridArray = new int[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                Debug.DrawLine(new Vector3(startX - cellSize / 2, startY - cellSize / 2) + GetWorldPosition(x, y), new Vector3(startX - cellSize / 2, startY - cellSize / 2) + GetWorldPosition(x, y + 1), Color.white, 1f);
                Debug.DrawLine(new Vector3(startX - cellSize / 2, startY - cellSize / 2) + GetWorldPosition(x, y), new Vector3(startX - cellSize / 2, startY - cellSize / 2) + GetWorldPosition(x + 1, y), Color.white, 1f);
            }
        }
        Debug.DrawLine(new Vector3(startX - cellSize / 2, startY - cellSize / 2) + GetWorldPosition(0, height), new Vector3(startX - cellSize / 2, startY - cellSize / 2) + GetWorldPosition(width, height), Color.white, 1f);
        Debug.DrawLine(new Vector3(startX - cellSize / 2, startY - cellSize / 2) + GetWorldPosition(width, 0), new Vector3(startX - cellSize / 2, startY - cellSize / 2) + GetWorldPosition(width, height), Color.white, 1f);
    }
    public Vector3 GetNearestPositionOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / cellSize);
        int yCount = Mathf.RoundToInt(position.y / cellSize);

        Vector3 result = new Vector3((float)xCount * cellSize, yCount * cellSize);

        result += transform.position;
        return result;
    }
    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }
    /*
    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }
    }
    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }
    */

    public int GetValue(int x, int y)
    {
        if (x >= startX && y >= startY && x < startX+width && y < startY+height)
        {
            return gridArray[x, y];
        }
        else
        {
            return 0;
        }
    }

    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }
}
