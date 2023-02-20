using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Generator : MonoBehaviour
{
    public GeneratorSettings settings;
    public GameSettings gameSettings;
    // 15 23
    public GameObject background;

    GameObject dirtPrefab;
    GameObject grassPrefab;
    GameObject stonePrefab;
    GameObject oreLevelOne;
    GameObject oreLevelTwo;
    GameObject oreLevelThree;
    GameObject oreLevelFour;


    GameObject[][,] world;
    int[][,] worldSave;

    public int chunksCount;

    public int baseMinX = -16;
    public int baseMaxX = 16;
    public int baseMinY = -16;
    public int baseMaxY = 16;

    int minX = 0;
    int maxX = 0;
    int minY = 0;
    int maxY = 0;

    int baseSeed;

    int chunkX;
    int chunkY;

    public float scale = 1;
    //public float blockWidth = 1;//dirtPrefab.transform.lossyScale.x;
    //public float blockHeight = 1;//dirtPrefab.transform.lossyScale.y;

    PerlinNoise noise;
    void Start()
    {
        dirtPrefab = settings.dirtPrefab;
        grassPrefab = settings.grassDefaultPrefab;
        stonePrefab = settings.stonePrefab;
        oreLevelOne = settings.oreLevelOne;
        oreLevelTwo = settings.oreLevelTwo;
        oreLevelThree = settings.oreLevelThree;
        oreLevelFour = settings.oreLevelFour;

        //Debug.Log("dirt id je " + dirtPrefab.GetComponent<BlockMiner>().blockID);

        chunksCount = settings.numberOfChunks;

        ResetPosition();
        world = new GameObject[chunksCount][,];
        chunkX = (baseMinX < 0) ? -baseMinX + baseMaxX : baseMinX + baseMaxX;
        chunkY = (baseMinY < 0) ? -baseMinY + baseMaxY : baseMinY + baseMaxY;

        if (gameSettings.generateNew)
        {
            GenerateWorld();
        } else
        {
            Load(gameSettings.currentSave);
        }

        Grid grid = new Grid(chunkX, chunkY, 1.2f, baseMinX, baseMinY, chunksCount);
    }

    public void GenerateWorld()
    {
        baseSeed = UnityEngine.Random.Range(0, 10000000);
        //Debug.Log("World seed is " + baseSeed);
        for (int i = 0; i < chunksCount; i++)
        {
            if (world[i] == null)
            {
                world[i] = new GameObject[chunkX, chunkY];
            }
            world[i] = GenerateChunk(baseSeed, world[i]);
            Instantiate(background, new Vector2(minX + 15, minY + 23), Quaternion.identity);
            minX += baseMaxX - baseMinX;
            maxX += baseMaxX - baseMinX;
        }
        gameSettings.generateNew = false;
        SaveLoad.SaveGame(SaveWorld(), gameSettings.currentSave);
    }
    public void ResetPosition()
    {
        minX = baseMinX;
        maxX = baseMaxX;
        minY = baseMinY;
        maxY = baseMaxY;
    }
    public void DestroyWorld()
    {
        for (int i = 0; i < world.Length; i++)
        {
            DestroyChunk(world[i]);
        }
    }
    public void RegenerateWorld()
    {
        ResetPosition();
        baseSeed = UnityEngine.Random.Range(0, 10000000);
        for (int i = 0; i < world.Length; i++)
        {
            DestroyChunk(world[i]);
        }
        chunksCount = settings.numberOfChunks;
        if (world.Length != chunksCount)
        {
            Array.Resize(ref world, chunksCount);
        }
        GenerateWorld();
    }
    public GameObject[,] GenerateChunk(int baseSeed, GameObject[,] chunk)
    {
        int oreSeed = UnityEngine.Random.Range(0, 7000000);

        noise = new PerlinNoise(baseSeed);

        for (int i = minX; i < maxX; i++)//columns x
        {
            int columnHeigh = 27 + noise.GetNoise(i - minY, maxY - minY - 27 - 17); // 27 na odsadenie od spodku a 17 odsadenie od vrchu
            int stoneLevel = UnityEngine.Random.Range((minY + columnHeigh - 10), minY + columnHeigh - 5);
            for (int j = minY; j < maxY; j++)//rows y
            {
                if (j < minY + columnHeigh)
                {
                    GameObject block = (j == minY + columnHeigh - 1) ? grassPrefab : dirtPrefab;
                    if (j < stoneLevel)
                    {
                        block = stonePrefab;
                    }
                    chunk[i - minX, j - minY] = Instantiate(block, new Vector2(i * scale, j * scale), Quaternion.identity);
                }
                else
                {
                    chunk[i - minX, j - minY] = null;
                }
            }
        }
        GenerateOre(oreSeed, chunk);
        return chunk;
    }
    void DestroyBlockType(GameObject prefab, GameObject[,] chunk)
    {
        foreach (GameObject selectedTile in chunk)
        {
            if (selectedTile != null && selectedTile.GetComponent<SpriteRenderer>().sprite == prefab.GetComponent<SpriteRenderer>().sprite)
            {
                Destroy(selectedTile);
            }
        }
    }
    void BlockSwapper(GameObject prefab, int X, int Y, GameObject[,] chunk)
    {
        Vector2 positionHolder = chunk[X, Y].transform.position;
        Destroy(chunk[X, Y]);
        chunk[X, Y] = Instantiate(prefab, positionHolder, Quaternion.identity);
    }
    void GenerateOre(int seed, GameObject[,] chunk)
    {
        float frequency = 2;
        for (int i = 0; i < chunkX; i++)
        {
            for (int j = 0; j < chunkY; j++)
            {
                if (chunk[i, j] != null && chunk[i, j].GetComponent<SpriteRenderer>().sprite == stonePrefab.GetComponent<SpriteRenderer>().sprite)
                {// If map index is a filled tile (not empty)
                    float oreNoise = Mathf.PerlinNoise((i / frequency) + seed, (j / frequency) + seed);
                    if (oreNoise > 0.80 && j <= 5) // ore lvl 4
                    {
                        BlockSwapper(oreLevelFour, i, j, chunk);
                    }
                    else if (oreNoise > 0.70 && j >= 5 && j <= 10) // ore lvl 3
                    {
                        BlockSwapper(oreLevelThree, i, j, chunk);
                    }
                    else if (oreNoise > 0.60 && j >= 10 && j <= 15) //ore lvl 2
                    {
                        BlockSwapper(oreLevelTwo, i, j, chunk);
                    }
                    else if (oreNoise >= 0.50 && j >= 15)//ore lvl 1
                    {
                        BlockSwapper(oreLevelOne, i, j, chunk);
                    }
                    else
                    {

                    }
                }
            }
        }
    }
    public void RegenerateChunk(GameObject[,] chunk)
    {
        DestroyChunk(chunk);
        chunk = GenerateChunk(baseSeed, chunk);
    }
    public void DestroyChunk(GameObject[,] chunk)
    {
        if (chunk != null)
        {
            foreach (GameObject block in chunk)
            {
                Destroy(block);
            }
        }
    }
    public void WS()
    {
        worldSave = SaveWorld();
    }
    public int[][,] SaveWorld()
    {
        int[][,] worldSav = new int[world.Length][,];
        for (int i = 0; i < world.Length; i++)
        {
            worldSav[i] = SaveChunk(world[i]);
        }
        return worldSav;
    }
    public int[,] SaveChunk(GameObject[,] chunk)
    {
        int[,] chunkSave = new int[chunkX, chunkY];//vracia pole chunku [x,y] s informaciami bloku [pos x, pos y, blockID]
        for (int i = minX; i < maxX; i++)//columns x
        {
            for (int j = minY; j < maxY; j++)//rows y
            {
                chunkSave[i - minX, j - minY] = 0;
                if (chunk[i - minX, j - minY] != null)
                {
                    chunkSave[i - minX, j - minY] = chunk[i - minX, j - minY].GetComponent<BlockMiner>().blockID;
                }
                else
                {
                    chunkSave[i - minX, j - minY] = 0;
                }
            }
        }
        return chunkSave;
    }
    public void LW()
    {
        LoadWorld(worldSave);
    }
    public void LoadWorld(int[][,] worldLoad)
    {
        ResetPosition();
        DestroyWorld();
        for (int i = 0; i < worldLoad.Length; i++)
        {
            if (i > world.Length-1) continue;
            world[i] = new GameObject[chunkX,chunkY];
            world[i] = LoadChunk(worldLoad[i]);
            minX += baseMaxX - baseMinX;
            maxX += baseMaxX - baseMinX;
        }
    }
    public GameObject[,] LoadChunk(int[,] chunkLoad)
    {
        GameObject[,] chunkTemp = new GameObject[chunkX, chunkY];
        for (int i = minX; i < maxX; i++)//columns x
        {
            for (int j = minY; j < maxY; j++)//rows y
            {
                GameObject block = GetPrefabByID(chunkLoad[i - minX, j - minY]);
                if (block != null)
                {
                    chunkTemp[i - minX, j - minY] = Instantiate(block, new Vector2(i * scale, j * scale), Quaternion.identity);
                }
            }
        }
        return chunkTemp;
    }
    public float[] BlockCheck(GameObject block, float posX, float posY)
    {
        float[] pos = new float[2];
        if (world == null)
        {
            Debug.Log("embty bliad");
        }
        for (int w = 0; w < world.Length; w++)
        {
            for (int i = 0; i < world[w].GetLength(0); i++)
            {
                for (int j = 0; j < world[w].GetLength(1); j++)
                {
                    if (world[w][i, j] != null)
                    {
                        if (world[w][i, j] == block && world[w][i, j].transform.position.x == posX && world[w][i, j].transform.position.y == posY)
                        {
                            pos[0] = world[w][i, j].transform.position.x;
                            pos[1] = world[w][i, j].transform.position.y;
                        }
                    }
                }
            }
        }
        return pos;
    }
    public void BlockReplace(GameObject blockOld, float posX, float posY, int ID)
    {
        float[] pos = new float[2];
        if (world == null)
        {
            Debug.Log("embty bliad");
        }
        for (int w = 0; w < world.Length; w++)
        {
            for (int i = 0; i < world[w].GetLength(0); i++)
            {
                for (int j = 0; j < world[w].GetLength(1); j++)
                {
                    if (world[w][i, j] != null)
                    {
                        if (world[w][i, j] == blockOld && world[w][i, j].transform.position.x == posX && world[w][i, j].transform.position.y == posY)
                        {
                            world[w][i, j] = Instantiate(GetPrefabByID(ID), blockOld.transform.position, Quaternion.identity);
                        }
                    }
                }
            }
        }
    }
    public GameObject GetPrefabByID(int ID)
    {
        GameObject result;
        switch (ID)
        {
            case 1:
                result = dirtPrefab;
                break;
            case 2:
                result = grassPrefab;
                break;
            case 3:
                result = stonePrefab;
                break;
            case 11:
                result = oreLevelOne;
                break;
            case 12:
                result = oreLevelTwo;
                break;
            case 13:
                result = oreLevelThree;
                break;
            case 14:
                result = oreLevelFour;
                break;
            default:
                result = null;
                break;
        }
        return result;
    }
    public void Save(int index)
    {   
        SaveLoad.SaveGame(SaveWorld(), index);
    }
    public void Load(int index)
    {
        LoadWorld(SaveLoad.LoadGame(index));
    }
}
