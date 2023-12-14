using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObsticleController : MonoBehaviour
{
    private MapGenerator MapGeneratorScriptToAccessList;
    // Start is called before the first frame update
    void Start()
    {
        MapGeneratorScriptToAccessList = GameObject.Find("GameController").GetComponent<MapGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroyDestructibleObsticle()
    {
        MapGeneratorScriptToAccessList.ImportantObjectList.RemoveAt(MapGeneratorScriptToAccessList.ImportantObjectList.IndexOf(this.gameObject));
        Destroy(this.gameObject);
    }
}
