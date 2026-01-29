using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public GameObject basic_resource;
    public GameObject abilityResource;
    public GameObject[][] chunk_loc = new GameObject[3][];
    private Dictionary<GameObject, List<GameObject>> resourcePool;
    private readonly int resource_amount = Upgraded.resourceSpawn;
    private readonly int veinRate = 25;
    private readonly float veinSize = 0.025f;
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
            GameObject currResource = resourcePool[the_chunk][i];
            if (i % veinRate == 0)
            {
                int givenRand = RandomVeinAmount();
                for (int k = i; k < i + givenRand; k++)
                {
                    GameObject currResource2 = resourcePool[the_chunk][k];
                    if (k >= resource_amount)
                    {
                        break;
                    }
                    ResourceActivate(currResource2);
                    currResource2.transform.localPosition = new Vector2(currResource.transform.localPosition.x + Random.Range(-veinSize, veinSize), currResource.transform.localPosition.y + Random.Range(-veinSize, veinSize));
                }
                i += givenRand;
                continue;
            }
            ResourceActivate(currResource);
        }
    }
    private int RandomVeinAmount()
    {
        return Mathf.FloorToInt(Random.Range(resource_amount / 20, resource_amount / 10));
    }
    private void ResourceActivate(GameObject givenResource)
    {
        givenResource.transform.localPosition = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        givenResource.SetActive(true);
    }

}