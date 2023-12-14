using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MapGenerator : MonoBehaviour
{
    public GameObject WallPrefab;
    public GameObject ObsticlePrefab;
    public GameObject FloorPrefab;
    public GameObject DestructibleObsticlePrefab;
    public GameObject BasePrefab;
    public GameObject EmptyObject;
    public SpriteRenderer BackgroundSprite;
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;

    public List<GameObject> ImportantObjectListPathFinding;
    public List<GameObject> ImportantObjectList;

    public int HousePositionX = 0;
    public int HousePositionY = 0;

    public int mapWidth = 18;
    public int mapHeight = 17;

    public int MaxNumberOfObsticles;
    public int NumberOfObsticles;
    private int numberOfDestructibleObsticles;

    public float TileOffset = 1.05f;

    public int ObsticleCounter = 0;
    public float DestructibleObsticleCounter = 0;

    public int SpaceBetweenObsticleAndHouse = 2;
    public int SpaceBetweenEnemyAndHouse = 4;
    public int SpacingHouseFromWalls = 2;

    public int amountOfEnemies = 0;
    public int numberOfEnemies = 4;
    public float enemiesSpawnCounter = 0;

    private int[] PlayerSpawnOptions = new int[] { -1, 1 };
    /// </summary>

    public int Checker; // If it is != size of ImportantObjectList. Count wont spawn  Look at Generate Destructable obsticles

    void Start()
    {
        MaxNumberOfObsticles = ((mapWidth - 2) * (mapHeight - 2)) / 6;
        NumberOfObsticles = Random.Range(6, MaxNumberOfObsticles + 1);
        numberOfDestructibleObsticles = 4;
        CreateMap();
    }

    void Update()
    {
    }
    void CreateMap()
    {
        ResizeBackground();
        GenerateWalls();
        GenerateFloor();
        GenerateHouse();
        GenerateObsitcles();
        GenerateDestructibleObsitcles();
        SpawnPlayer();
        SpawnEnemies();

    }

    void ResizeBackground()
    {
        BackgroundSprite.size = new Vector2(mapWidth * 6, mapHeight * 6);

    }
    void GenerateWalls()
    {
        for (int x = 0; x <= mapWidth; x++) // Generating Horizontal Walls
        {
            GameObject TemporaryObject = Instantiate(WallPrefab);
            TemporaryObject.transform.position = new Vector2(x, 0);

            GameObject TemporaryObject2 = Instantiate(WallPrefab);
            TemporaryObject2.transform.position = new Vector2(x, mapHeight);

            TemporaryObject.transform.parent = transform;
            TemporaryObject2.transform.parent = transform;
        }
        for (int y = 0; y <= mapHeight; y++) // Generating Vertical Walls
        {
            GameObject TemporaryObject = Instantiate(WallPrefab);
            TemporaryObject.transform.position = new Vector2(0, y);

            GameObject TemporaryObject2 = Instantiate(WallPrefab);
            TemporaryObject2.transform.position = new Vector2(mapWidth, y);

            TemporaryObject.transform.parent = transform;
            TemporaryObject2.transform.parent = transform;
        }
    }

    void GenerateFloor()
    {
        for (int x = 1; x <= mapWidth - 1; x++)
        {
            for (int y = 1; y <= mapHeight - 1; y++)
            {
                GameObject TemporaryObject = Instantiate(FloorPrefab);
                TemporaryObject.transform.position = new Vector2(x, y);

                SetTileInfo(TemporaryObject, x, y);
            }
        }
    }

    void GenerateHouse()
    {

        HousePositionX = Random.Range(SpacingHouseFromWalls, mapWidth - SpacingHouseFromWalls);
        HousePositionY = Random.Range(SpacingHouseFromWalls, mapHeight - SpacingHouseFromWalls);

        GameObject TemporaryObject = Instantiate(BasePrefab);
        TemporaryObject.transform.position = new Vector2(HousePositionX, HousePositionY);


        TemporaryObject.transform.parent = transform;
        TemporaryObject.name = "House";

        ImportantObjectList.Add(TemporaryObject);
    }

    void GenerateObsitcles()
    {
        while (ObsticleCounter <= NumberOfObsticles)
        {
            int TempX = Random.Range(1, mapWidth);
            int TempY = Random.Range(1, mapHeight);
            if (Mathf.Abs(HousePositionX - TempX) >= SpaceBetweenObsticleAndHouse && Mathf.Abs(HousePositionY - TempY) >= SpaceBetweenObsticleAndHouse)
            {

                GameObject TemporaryObject = Instantiate(WallPrefab);
                TemporaryObject.transform.position = new Vector2(TempX, TempY);

                TemporaryObject.transform.parent = transform;
                ImportantObjectListPathFinding.Add(TemporaryObject);
                ImportantObjectList.Add(TemporaryObject);
                ObsticleCounter++;
            }
        }
    }

    void GenerateDestructibleObsitcles()
    {
        int x = numberOfDestructibleObsticles - Random.Range(0, 3);
        while (DestructibleObsticleCounter <= x)
        {
            int TempX = Random.Range(1, mapWidth);
            int TempY = Random.Range(1, mapHeight);

            int TemporaryListCount = ImportantObjectList.Count; // so we dont count it twice for no need
            for (int i = 0; i < TemporaryListCount; i++)
            {
                if (Mathf.Abs(HousePositionX - TempX) >= SpaceBetweenObsticleAndHouse && Mathf.Abs(HousePositionY - TempY) >= SpaceBetweenObsticleAndHouse)
                {
                    if (TempX != ImportantObjectList[i].transform.position.x && TempY != ImportantObjectList[i].transform.position.y) // Checking to see if space is free  (UwU it Rhymes, I am a god)
                    {
                        Checker++;
                    }
                }
            }
            if (Checker == TemporaryListCount)
            {
                GameObject TemporaryObject = Instantiate(DestructibleObsticlePrefab);
                TemporaryObject.transform.position = new Vector2(TempX, TempY);

                TemporaryObject.name = "DestructibleObsticle" + TempX +"," + TempY;
                TemporaryObject.transform.parent = transform;
                ImportantObjectList.Add(TemporaryObject); // Addin destructible object to list
                DestructibleObsticleCounter++;
            }
            Checker = 0; // Resetting ma checker
            DestructibleObsticleCounter += 0.0001f;
        }
    }

    void SpawnPlayer()
    {
        GameObject TemporaryObject = Instantiate(PlayerPrefab);
        TemporaryObject.transform.position = new Vector2(HousePositionX + PlayerSpawnOptions[Random.Range(0, 2)], HousePositionY + PlayerSpawnOptions[Random.Range(0, 2)]);

        TemporaryObject.transform.parent = transform;
        TemporaryObject.name = "Player";
    }

    void SpawnEnemies()
    {
        int TemporaryListCount = ImportantObjectList.Count; // so we dont count it twice for no need
        while (enemiesSpawnCounter < numberOfEnemies)
        {
            int TempX = Random.Range(1, mapWidth);
            int TempY = Random.Range(1, mapHeight);
            for (int i = 0; i < TemporaryListCount; i++)
            {
                if (Mathf.Abs(HousePositionX - TempX) >= SpaceBetweenEnemyAndHouse || Mathf.Abs(HousePositionY - TempY) >= SpaceBetweenEnemyAndHouse)
                {
                    if (TempX != ImportantObjectList[i].transform.position.x && TempY != ImportantObjectList[i].transform.position.y) // Checking to see if space is free  (UwU it Rhymes, I am a god)
                    {
                        Checker++;
                    }
                }
            }
            if (Checker == TemporaryListCount)
            {
                GameObject TemporaryObject = Instantiate(EnemyPrefab);
                TemporaryObject.transform.position = new Vector2(TempX, TempY);

                TemporaryObject.transform.parent = transform;
                TemporaryObject.name = "Enemy" + amountOfEnemies;
                enemiesSpawnCounter++;
                amountOfEnemies++;
            }
            Checker = 0;
            enemiesSpawnCounter += 0.0001f;
        }
        if(amountOfEnemies==0)
        {
            SceneManager.LoadScene("InGameScene"); 
        }
    }

    void SetTileInfo(GameObject Tile, int x, int y)
    {
        Tile.transform.parent = transform;
        Tile.name = x.ToString() + "," + y.ToString();

        Tile.transform.parent = transform;
        Tile.name = x.ToString() + "," + y.ToString();
    }
}
