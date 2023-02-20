using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonExtras : MonoBehaviour
{
    public bool flag = true;
    public GameObject SaveMenu; 
    public GameObject NewGameMenu;
    public GameObject SettingsMenu;
    GameObject CraftingMenu;

    public GameSettings settings;
    public PlayerSettings pSettings;
    public Generator generator;

    public GameObject hand;
    public GameObject pickaxe;
    public GameObject hammer;

    public GameObject buildingMenu;
    public GameObject[] buildingBlocks;

    private void Start()
    {
        if (flag) Inicialize();
        if (settings != null) 
        {
            settings.currentBlock = -1;
            SwitchBlock();
        } 
        if (buildingMenu != null) buildingMenu.SetActive(false);
    }
    public void DoExitGame()
    {
        Application.Quit();
    }
    public void LoadScene( int scene)
    {
        Inicialize();
        SceneManager.LoadScene(scene);
    }
    public void SaveGame(int saveIndex)
    {
        generator.Save(saveIndex);
    }
    public void LoadGame(int loadIndex)
    {
        if (SaveLoad.CheckSave(loadIndex))
        {
            LoadScene(1);
            settings.currentSave = loadIndex;
            SaveLoad.LoadGame(loadIndex);
        }
    }
    public void OpenCloseSaveMenu()
    {        
        if (SaveMenu.activeSelf)
        {
            SaveMenu.SetActive(false);
        }
        else
        {
            SaveMenu.SetActive(true);
        }
    }
    public void OpenCloseNewGameMenu()
    {
        if (NewGameMenu.activeSelf)
        {
            NewGameMenu.SetActive(false);
        }
        else
        {
            NewGameMenu.SetActive(true);
        }
    }
    public void OpenCloseSettingsMenu()
    {
        if (SettingsMenu.activeSelf)
        {
            SettingsMenu.SetActive(false);
        }
        else
        {
            SettingsMenu.SetActive(true);
        }
    }
    public void OpenCloseCraftingMenu()
    {
        if (CraftingMenu.activeSelf)
        {
            CraftingMenu.SetActive(false);
        }
        else
        {
            CraftingMenu.SetActive(true);
        }
    }
    void Inicialize()
    {
        SaveMenu = GameObject.Find("SaveMenu/Panel");
        NewGameMenu = GameObject.Find("NewGameMenu/Panel");
        SettingsMenu = GameObject.Find("SettingsMenu/Panel");
        CraftingMenu = GameObject.Find("CraftingMenu/Panel");
        if (SaveMenu != null)
        {
            SaveMenu.SetActive(false);
        }
        if (NewGameMenu != null)
        {
            NewGameMenu.SetActive(false);
        }
        if (SettingsMenu != null)
        {
            SettingsMenu.SetActive(false);
        }
        if (CraftingMenu != null)
        {
            CraftingMenu.SetActive(false);
        }
    }
    public void Menu()
    {
        generator.DestroyWorld();
        LoadScene(0);
    }
    public void Delete(int index)
    {
        SaveLoad.DeleteSave(index);
    }
    public void ContinueGame()
    {
        Debug.Log(settings.currentSave);
        if (SaveLoad.CheckSave(settings.currentSave)) 
        {
            LoadGame(settings.currentSave);
        }
    }

    public void NewGame(int index)
    {
        settings.currentSave = index;
        string path = Application.persistentDataPath + "/pecok" + index + ".budzogan";
        if (SaveLoad.CheckFile(path))
        {
            SaveLoad.DeleteSave(index);
        }
        LoadScene(1);
        settings.generateNew = true;
    }
    public void SwitchTool()
    {
        switch (settings.currentTool)
        {
            case 0:
                settings.currentTool++;
                hand.SetActive(false);
                pickaxe.SetActive(true);
                hammer.SetActive(false);
                buildingMenu.SetActive(false);
                break;
            case 1:
                settings.currentTool++;
                hand.SetActive(false);
                pickaxe.SetActive(false);
                hammer.SetActive(true);
                buildingMenu.SetActive(true);
                break;
            case 2:
                hand.SetActive(true);
                pickaxe.SetActive(false);
                hammer.SetActive(false);
                buildingMenu.SetActive(false);
                settings.currentTool = 0;
                break;
            default:
                Debug.LogError("There is no hand wit that numero biatch" + settings.currentTool);
                break;
        }
    }
    public void SwitchBlock()
    {
        if (buildingBlocks.Length <= 1) return;
        settings.currentBlock++;
        if (settings.currentBlock >= buildingBlocks.Length)
        {
            settings.currentBlock = 0;
        }
        for (int i = 0; i < buildingBlocks.Length; i++)
        {
            buildingBlocks[i].SetActive(false);
        }
        buildingBlocks[settings.currentBlock].SetActive(true);
    }
}
