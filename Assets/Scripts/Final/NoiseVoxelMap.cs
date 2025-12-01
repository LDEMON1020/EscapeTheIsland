using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseVoxelMap : MonoBehaviour
{
    public GameObject[] blockPrefab;
    public int width = 20;
    public int depth = 20;
    public int maxHeight = 16;



    [SerializeField] float noiseScale = 20f;
    // Start is called before the first frame update

    [SerializeField]
    int Water = 4;
    void Start()
    {
        float offsetX = Random.Range(-9999f, 9999f);
        float offsetZ = Random.Range(-9999f, 9999f);

        for (int x = 0; x < width; x++)
        {
            for(int z = 0; z< depth; z++)
            {
                float nx = (x + offsetX) / noiseScale;
                float nz = (z + offsetZ) / noiseScale;

                float noise = Mathf.PerlinNoise(nx, nz);

                int h = Mathf.FloorToInt(noise * maxHeight);

                if (h <= 0) continue;

                for (int y = 0; y <= h; y++)
                {
                    if (y == h)
                    {
                        PlaceGrass(x, y, z);
                    }
                    else
                    {
                        PlaceDirt(x, y, z);
                    }
                }

                for (int y = h+1;y<=Water;y++)
                {
                    PlaceWater(x, y, z);
                }
            }
        }
    }

   private void PlaceGrass(int x,int y, int z)
    {
        var go = Instantiate(blockPrefab[0], new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"A_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = BlockType.Grass;
        b.maxHP = 1;
        b.dropCount = 1;
        b.mineable = true;
    }
    private void PlaceDirt(int x, int y, int z)
    {
        var go = Instantiate(blockPrefab[1], new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"B_{x}_{y}_{z}";
        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = BlockType.Dirt;
        b.maxHP = 3;
        b.dropCount = 1;
        b.mineable = true;
    }
    private void PlaceWater(int x, int y, int z)
    {
        var go = Instantiate(blockPrefab[2], new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"B_{x}_{y}_{z}";
    }

    public void PlaceTile(Vector3Int pos, BlockType type)
    {
        switch(type)
        {
            case BlockType.Grass:
                PlaceGrass(pos.x, pos.y, pos.z);
                break;
            case BlockType.Dirt:
                PlaceDirt(pos.x, pos.y, pos.z);
                break;
            case BlockType.Water:
                PlaceWater(pos.x, pos.y, pos.z);
                break;
        }
    }
}
