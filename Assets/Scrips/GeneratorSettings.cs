using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GeneratorSettings : ScriptableObject
{

    public GameObject grassDefaultPrefab;
    public GameObject grassBiomePrefab;
    public GameObject dirtPrefab;
    public GameObject stonePrefab;
    public GameObject platform;
    public GameObject oreLevelOne;
    public GameObject oreLevelTwo;
    public GameObject oreLevelThree;
    public GameObject oreLevelFour;

    public int numberOfChunks = 1;

    public NoiseLayer[] noiseLayers;
    //public NoiseSettings noiseSettings;

    [System.Serializable]
    public class NoiseLayer
    {
        public bool enabled = true;
        public bool useFisrstLayerAsMask;
    }
}