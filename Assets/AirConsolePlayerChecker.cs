using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class AirConsolePlayerChecker : MonoBehaviour
{
    public GameObject [] PlayerPicture;
    Dictionary<int, int> skinMapping = new Dictionary<int, int>();
    Dictionary<int, bool> lockedMapping = new Dictionary<int, bool>();
    int skinsLength = 2;

    void Update()
    {
        var ready = true;
        var collection = AirConsole.instance.GetActivePlayerDeviceIds;
        Dictionary<int, int>.KeyCollection keyColl = skinMapping.Keys;
        foreach (int key in keyColl)
        {
            if(!collection.Contains(key))
            {
                skinMapping.Remove(key);
                lockedMapping.Remove(key);
            }
        }
        foreach(int id in collection)
        {
            if(!skinMapping.ContainsKey(id))
            {
                skinMapping[id] = 0;
                lockedMapping[id] = false;
            }
        }
        for (int i=0; i < 4; i++)
        {
            int id = AirConsole.instance.ConvertPlayerNumberToDeviceId(i);
            if (collection.Contains(id))
            {
                PlayerPicture[i].SetActive(true);
                if(lockedMapping[id] == false)
                {
                    ready = false;
                }
            }
            else
            {
                PlayerPicture[i].SetActive(false);
            }
        }
        if(ready == true && collection.Count > 0)
        {
            SceneManager.LoadScene(sceneName: "Scene");
        }
    }

    public int GetSkinNumber(int number)
    {
        skinMapping.TryGetValue(AirConsole.instance.ConvertPlayerNumberToDeviceId(number), out int skinNumber);
        return skinNumber;
    }

    public void ChangeSkin(int id, int change)
    {
        skinMapping.TryGetValue(id, out int skinNumber);
        skinNumber = (skinNumber + change) % skinsLength;
        if (skinNumber < 0)
        {
            skinNumber = skinsLength - 1;
        }
        skinMapping[id] = skinNumber;
    }

    public bool GetLockedState(int number)
    {
        lockedMapping.TryGetValue(AirConsole.instance.ConvertPlayerNumberToDeviceId(number), out bool locked);
        return locked;
    }

    public void ChangeLocked(int id)
    {
        lockedMapping.TryGetValue(id, out bool locked);
        lockedMapping[id] = !locked;
    }
}
