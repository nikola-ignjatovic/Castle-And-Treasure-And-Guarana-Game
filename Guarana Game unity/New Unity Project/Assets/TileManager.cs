using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap tilemap;
    public List<Tile> tiles;
    public Tile newsprite;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int y = tilemap.origin.y; y < (tilemap.origin.y + tilemap.size.y); y++)
        {
            for (int x = tilemap.origin.x; x < (tilemap.origin.x + tilemap.size.x); x++)
            {
                Tile tile = tilemap.GetTile<Tile>(new Vector3Int(x, y, 0));
                if (tile != null)
                {
                    tiles.Add(tile);
                }
            }
        }
        Debug.Log(tiles.Count);
        Debug.Log(tiles[1]);

        tilemap.SetTile(new Vector3Int(1,1,0), newsprite);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
