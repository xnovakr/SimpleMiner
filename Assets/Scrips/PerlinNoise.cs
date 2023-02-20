using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise
{
    long seed;
    public PerlinNoise(long seed)
    {
        this.seed = seed;
    }
    int Random (int x, int range)
    {
        return (int)((x + seed) ^ 5) % range;
    }
    public int GetNoise(int x, int range)
    {
        int chunkSize = 16;  //distance between samples cim mensia chunkSize tym vecsia frekvencia
        float noise = 0;
        range /= 3;  //mensi range zjemni provrch

        while (chunkSize > 0)
        {
            int chunkIndex = x / 16;
            float prog = (x % chunkSize) / (chunkSize * 1f);

            float left_random = Random(chunkIndex, range);
            float right_random = Random(chunkIndex + 1, range);

            noise += ((1 - prog) * left_random + prog * right_random);

            chunkSize /= 2;
            range /= 2;

            range = Mathf.Max(1, range);
        }
        return (int)Mathf.Round(noise);
    }
}
