using UnityEngine;

public class ChunkBehaviour : MonoBehaviour
{
    GameObject map;
    MapGeneration mapGen;
    string dir;
    float roundedX;
    float roundedY;
    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map");
        mapGen = map.GetComponent<MapGeneration>();
    }

    private string GetDir()
    {
        Vector2 temp = (map.transform.position - transform.position) / mapGen.chunk_size;
        roundedX = Mathf.Round(temp.x);
        roundedY = Mathf.Round(temp.y);
        if (roundedX == 0 && roundedY == -1)
        {
            return "up";
        }
        else if (roundedX == 0 && roundedY == 1)
        {
            return "down";
        }
        else if (roundedX == 1 && roundedY == 0)
        {
            return "left";
        }
        else if (roundedX == -1 && roundedY == 0)
        {
            return "right";
        }
        return null;
    }
    private void OnTriggerEnter2D(Collider2D collid)
    {
        if (collid.CompareTag("Player"))
        {
            dir = GetDir();
            map.transform.position = gameObject.transform.position;
            GameObject[][] currChunkLoc = new GameObject[3][];
            for (int y = 0; y < 3; y++)
            {
                currChunkLoc[y] = new GameObject[3];
            }
            if (dir == "up")
            {
                mapGen.chunk_loc[2][0].transform.position = new Vector2(mapGen.chunk_loc[2][0].transform.position.x, mapGen.chunk_loc[2][0].transform.position.y + mapGen.chunk_size * 3);
                mapGen.chunk_loc[2][1].transform.position = new Vector2(mapGen.chunk_loc[2][1].transform.position.x, mapGen.chunk_loc[2][1].transform.position.y + mapGen.chunk_size * 3);
                mapGen.chunk_loc[2][2].transform.position = new Vector2(mapGen.chunk_loc[2][2].transform.position.x, mapGen.chunk_loc[2][2].transform.position.y + mapGen.chunk_size * 3);
                currChunkLoc[0] = mapGen.chunk_loc[2];
                currChunkLoc[1] = mapGen.chunk_loc[0];
                currChunkLoc[2] = mapGen.chunk_loc[1];
                for (int i = 0; i < 3; i++)
                {
                    mapGen.GrabFromPool(mapGen.chunk_loc[2][i]);
                }
                mapGen.chunk_loc = currChunkLoc;
            }
            else if (dir == "down")
            {
                mapGen.chunk_loc[0][0].transform.position = new Vector2(mapGen.chunk_loc[0][0].transform.position.x, mapGen.chunk_loc[0][0].transform.position.y - mapGen.chunk_size * 3);
                mapGen.chunk_loc[0][1].transform.position = new Vector2(mapGen.chunk_loc[0][1].transform.position.x, mapGen.chunk_loc[0][1].transform.position.y - mapGen.chunk_size * 3);
                mapGen.chunk_loc[0][2].transform.position = new Vector2(mapGen.chunk_loc[0][2].transform.position.x, mapGen.chunk_loc[0][2].transform.position.y - mapGen.chunk_size * 3);
                currChunkLoc[0] = mapGen.chunk_loc[1];
                currChunkLoc[1] = mapGen.chunk_loc[2];
                currChunkLoc[2] = mapGen.chunk_loc[0];
                for (int i = 0; i < 3; i++)
                {
                    mapGen.GrabFromPool(mapGen.chunk_loc[0][i]);
                }
                mapGen.chunk_loc = currChunkLoc;
            }
            else if (dir == "right")
            {
                mapGen.chunk_loc[0][0].transform.position = new Vector2(mapGen.chunk_loc[0][0].transform.position.x + mapGen.chunk_size * 3, mapGen.chunk_loc[0][0].transform.position.y);
                mapGen.chunk_loc[1][0].transform.position = new Vector2(mapGen.chunk_loc[1][0].transform.position.x + mapGen.chunk_size * 3, mapGen.chunk_loc[1][0].transform.position.y);
                mapGen.chunk_loc[2][0].transform.position = new Vector2(mapGen.chunk_loc[2][0].transform.position.x + mapGen.chunk_size * 3, mapGen.chunk_loc[2][0].transform.position.y);
                currChunkLoc[0][2] = mapGen.chunk_loc[0][0];
                currChunkLoc[1][2] = mapGen.chunk_loc[1][0];
                currChunkLoc[2][2] = mapGen.chunk_loc[2][0];

                currChunkLoc[0][1] = mapGen.chunk_loc[0][2];
                currChunkLoc[1][1] = mapGen.chunk_loc[1][2];
                currChunkLoc[2][1] = mapGen.chunk_loc[2][2];

                currChunkLoc[0][0] = mapGen.chunk_loc[0][1];
                currChunkLoc[1][0] = mapGen.chunk_loc[1][1];
                currChunkLoc[2][0] = mapGen.chunk_loc[2][1];

                for (int i = 0; i < 3; i++)
                {
                    mapGen.GrabFromPool(mapGen.chunk_loc[i][0]);
                }

                mapGen.chunk_loc = currChunkLoc;
            }
            else if (dir == "left")
            {
                mapGen.chunk_loc[0][2].transform.position = new Vector2(mapGen.chunk_loc[0][2].transform.position.x - mapGen.chunk_size * 3, mapGen.chunk_loc[0][2].transform.position.y);
                mapGen.chunk_loc[1][2].transform.position = new Vector2(mapGen.chunk_loc[1][2].transform.position.x - mapGen.chunk_size * 3, mapGen.chunk_loc[1][2].transform.position.y);
                mapGen.chunk_loc[2][2].transform.position = new Vector2(mapGen.chunk_loc[2][2].transform.position.x - mapGen.chunk_size * 3, mapGen.chunk_loc[2][2].transform.position.y);
                currChunkLoc[0][0] = mapGen.chunk_loc[0][2];
                currChunkLoc[1][0] = mapGen.chunk_loc[1][2];
                currChunkLoc[2][0] = mapGen.chunk_loc[2][2];

                currChunkLoc[0][1] = mapGen.chunk_loc[0][0];
                currChunkLoc[1][1] = mapGen.chunk_loc[1][0];
                currChunkLoc[2][1] = mapGen.chunk_loc[2][0];

                currChunkLoc[0][2] = mapGen.chunk_loc[0][1];
                currChunkLoc[1][2] = mapGen.chunk_loc[1][1];
                currChunkLoc[2][2] = mapGen.chunk_loc[2][1];

                for (int i = 0; i < 3; i++)
                {
                    mapGen.GrabFromPool(mapGen.chunk_loc[i][2]);
                }

                mapGen.chunk_loc = currChunkLoc;
            }
        }
    }
}