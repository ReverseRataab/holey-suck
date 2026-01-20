using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public GameObject basic_resource;
    public GameObject[][] chunk_loc = new GameObject[3][];
    private Dictionary<GameObject, List<GameObject>> resourcePool;
    private readonly int resource_amount = Upgraded.resourceSpawn;
    public readonly float chunk_size = 120;
    void Awake()
    {
        resourcePool = new Dictionary<GameObject, List<GameObject>>();
        basic_resource.SetActive(false);
        for (int y = 0; y < 3; y++)
        {
            chunk_loc[y] = new GameObject[3];
            for (int x = 0; x < 3; x++)
            {
                GameObject chunk = new GameObject();
                chunk.AddComponent<BoxCollider2D>().isTrigger = true;
                chunk.AddComponent<ChunkBehaviour>();
                chunk.transform.localScale = new Vector2(chunk_size, chunk_size);
                chunk.name = "Chunk" + x + " " + y;
                chunk.transform.position = new Vector2(transform.position.x - chunk_size + (chunk_size * x), transform.position.y + chunk_size - (chunk_size * y));
                chunk_loc[y][x] = chunk;
                resourcePool.Add(chunk, new List<GameObject>());
                LoadResources(chunk);
                GrabFromPool(chunk);
            }
        }
    }
    // Spawns Resources in pool
    private void LoadResources(GameObject the_chunk)
    {
        for (int i = 0; i < resource_amount; i++)
        {
            GameObject res = Instantiate(basic_resource, the_chunk.transform.position, Quaternion.identity, the_chunk.transform);
            resourcePool[the_chunk].Add(res);
        }
    }
    public void GrabFromPool(GameObject the_chunk)
    {
        for (int i = 0; i < resource_amount; i++)
        {
            resourcePool[the_chunk][i].transform.localPosition = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            resourcePool[the_chunk][i].SetActive(true);
        }
    }

}