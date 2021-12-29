using System.Collections.Generic;
using UnityEngine;

public class TestFilling : MonoBehaviour
{
    public GameObject prefab;

    public void FillGames(List<GameInfo> games)
    {
        for (int i = 0; i < games.Count; i++)
        {
            AddGame(games[i]);
        }
    }

    public void AddGame(GameInfo info)
    {
        GameObject obj = Instantiate(prefab, transform);
        var game = obj.GetComponent<Game>();
        game.SetInfo(info);
        obj.name = info.processname;

        Controller.Instance.Subscribe(game, true);
    }

    public void RemoveGame(GameInfo info)
    {
        GameObject obj = GameObject.Find($"{info.processname}");
        if (obj != null)
        {
            Destroy(obj);
        }
    }
}
