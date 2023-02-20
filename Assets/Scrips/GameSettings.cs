using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GameSettings : ScriptableObject
{
    public int currentSave = 0;
    public bool generateNew = false;
    [Range(0,2)]
    public int currentTool = 0;
    public int currentBlock = 0;
}   