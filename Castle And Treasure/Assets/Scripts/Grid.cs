using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Grid<TGridObject>
{
    private int width;
    private int height;
    private TGridObject[,] gridArray;
    private float cellSize;
    private Vector3 originPosition;
    private MapGenerator MapGeneratorScript;
    private int checker = 0;

    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new TGridObject[width, height];
        for (int x=0;x<gridArray.GetLength(0); x++)
        {
            for (int y=0;y<gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this,x,y);
                //for (int i = 0; i < MapGeneratorScript.ImportantObjectListPathFinding.Count; i++)
                //{
                //    if (x == MapGeneratorScript.ImportantObjectListPathFinding[i].transform.position.x && y== MapGeneratorScript.ImportantObjectListPathFinding[i].transform.position.y)
                //    {
                //        checker++;
                //    }
                //}
                //if(checker== MapGeneratorScript.ImportantObjectListPathFinding.Count)
                //{

                //    checker = 0;
                //}
                //else
                //{
                //    checker = 0;
                //}
            }
        }
    }
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }
    public void GetXY(Vector3 worldPosition,out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }
    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
            return default(TGridObject);
    }
    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
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
}
