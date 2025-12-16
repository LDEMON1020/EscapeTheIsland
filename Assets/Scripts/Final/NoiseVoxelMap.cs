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
                    else if (y >= h - 3)   // 표면에서 3칸까지는 흙
                    {
                        PlaceDirt(x, y, z);
                    }
                    else
                    {
                        if (Random.value < 0.2f) // 20%
                        {
                            PlaceIron(x, y, z);
                        }
                        else
                        {
                            PlaceStone(x, y, z);
                        }
                    }
                }

                for (int y = h+1;y<=Water;y++)
                {
                    PlaceWater(x, y, z);
                }
            }
        }
    }

    private void PlaceGrass(int x, int y, int z)
    {
        var go = Instantiate(blockPrefab[0], new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"A_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = ItemType.Grass;
        b.maxHP = 1;
        b.dropCount = 1;
        b.mineable = true;

        // ⭐ Grass 위에 20% 확률로 Stick
        if (Random.value < 0.1f)
        {
            PlaceStick(x, y + 1, z);
        }
    }
    private void PlaceDirt(int x, int y, int z)
    {
        var go = Instantiate(blockPrefab[1], new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"B_{x}_{y}_{z}";
        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = ItemType.Dirt;
        b.maxHP = 3;
        b.dropCount = 1;
        b.mineable = true;
    }
    private void PlaceWater(int x, int y, int z)
    {
        var go = Instantiate(blockPrefab[2], new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"B_{x}_{y}_{z}";
    }

    private void PlaceStone(int x, int y, int z)
    {
        var go = Instantiate(blockPrefab[3], new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"S_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = ItemType.Stone;
        b.maxHP = 5;
        b.dropCount = 1;
        b.mineable = true;
    }

    private void PlaceIron(int x, int y, int z)
    {
        var go = Instantiate(blockPrefab[4], new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"I_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = ItemType.Iron;
        b.maxHP = 6;
        b.dropCount = 1;
        b.mineable = true;
    }

    private void PlaceStick(int x, int y, int z)
    {
        var go = Instantiate(blockPrefab[5], new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"T_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = ItemType.Stick;
        b.maxHP = 1;
        b.dropCount = 1;
        b.mineable = true;
    }

    public void PlaceTile(Vector3Int pos, ItemType type)
    {
        switch(type)
        {
            case ItemType.Grass:
                PlaceGrass(pos.x, pos.y, pos.z);
                break;
            case ItemType.Dirt:
                PlaceDirt(pos.x, pos.y, pos.z);
                break;
            case ItemType.Water:
                PlaceWater(pos.x, pos.y, pos.z);
                break;
            case ItemType.Stone:
                PlaceStone(pos.x, pos.y, pos.z);
                break;
            case ItemType.Iron:
                PlaceIron(pos.x, pos.y, pos.z);
                break;
            case ItemType.Stick:
                PlaceStick(pos.x, pos.y, pos.z);
                break;
        }
    }
}
