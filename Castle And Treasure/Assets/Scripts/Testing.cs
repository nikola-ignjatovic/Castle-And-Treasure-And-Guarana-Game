using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private EnemyController characterPathfinding;
    private PathFinding pathfinding;
    private Grid<PathNode> grid;
    private Transform LocationWhereToGo;
    private MapGenerator MapGeneratorScript;
    private int checker;
    void Start()
    {
        checker = 0;
        pathfinding = new PathFinding(16, 16);
        LocationWhereToGo = GameObject.Find("House").GetComponent<Transform>();
        MapGeneratorScript = GameObject.Find("GameController").GetComponent<MapGenerator>();
        for (int x = 1; x < MapGeneratorScript.mapWidth; x++)
        {
            for (int y = 1; y < MapGeneratorScript.mapHeight; y++)
            {
                for (int i = 0; i < MapGeneratorScript.ImportantObjectListPathFinding.Count; i++)
                {
                    if (x == MapGeneratorScript.ImportantObjectListPathFinding[i].transform.position.x && y == MapGeneratorScript.ImportantObjectListPathFinding[i].transform.position.y)
                    {
                        checker++;
                    }
                }
                if (checker == MapGeneratorScript.ImportantObjectListPathFinding.Count)
                {
                    grid.GetGridObject(x, y).isWalkable = false;
                    checker = 0;
                }
                else
                {
                    checker = 0;
                }
            }
        }

        StartCoroutine(StartWithPathFinding());
    }

    void Update()
    {
        
    }
    private IEnumerator StartWithPathFinding()
    {
        yield return new WaitForSeconds(3);
        characterPathfinding.isMoving = true;
        characterPathfinding.speed = characterPathfinding.speedSave;
        Vector3 mouseWorldPosition = new Vector3(LocationWhereToGo.transform.position.x, LocationWhereToGo.transform.position.y, 0);
        //pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);

        List<PathNode> path = pathfinding.FindPath(0, 0, (int)LocationWhereToGo.transform.position.x, (int)LocationWhereToGo.transform.position.y);

        characterPathfinding.SetTargetPosition(mouseWorldPosition);
    }
}
