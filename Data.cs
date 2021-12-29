using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class Data : MonoBehaviour
{
    private List<GameInfo> dataCache;
    private string filepath;

    private void Start()
    {
        filepath = Application.dataPath + "data.json";
        Debug.Log(filepath);
        dataCache = new List<GameInfo>();
    }

    public List<GameInfo> LoadSavedGames()
    {
        if (dataCache == null || dataCache?.Count < 1 )
        LoadJson();
        return dataCache;
    }    

    private void LoadJson()
    {
        if (File.Exists(filepath))
        {
            dataCache = JsonConvert.DeserializeObject<List<GameInfo>>(File.ReadAllText(filepath));
        }
        else
        {
            File.WriteAllText(filepath, JsonConvert.SerializeObject(dataCache));
        }
    }

    public void AddGame(GameInfo game, bool save = true)
    {
        dataCache.Add(game);
        if (save)
            Save();
    }
    public void Save(List<GameInfo> info = null)
    {
        File.WriteAllText(filepath, JsonConvert.SerializeObject(info == null ? dataCache : info));
    }
    private void OnApplicationQuit()
    {
        Debug.Log("saved");
        Save();
    }
}
