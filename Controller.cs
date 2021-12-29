using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private List<Game> _games;

    [SerializeField] private TestFilling _filler;
    [SerializeField] private Data _data;
    public List<Game> Games { get => _games; }
    private bool isFirstIterate = true;
    public static Controller Instance;

    private void Start()
    {
        if (Instance != null)
        {
            Debug.LogError($"Failed to create second instance of {nameof(Controller)}");
            return;
        }
        Instance = this;
        Invoke(nameof(Init), 0.15f);
    }

    private async void Init()
    {
        var _gamesCache = _data.LoadSavedGames();
        _filler.FillGames(_gamesCache);
        await UpdateTime();
    }

    public void Subscribe(Game game, bool alreadyInstantiated = false)
    {

        Debug.Log($"Subscribed process {game.GameInfo.name}");
        if (!alreadyInstantiated)
            _filler.AddGame(game.GameInfo);
        _games.Add(game);
    }

    public void Unsubscribe(Game game)
    {
        _filler.RemoveGame(game.GameInfo);
        _games.Remove(game);
        _data.Save(_games.Select(x => x.GameInfo).ToList());
    }

    private async Task UpdateTime()
    {
        while (true)
        {
            if (isFirstIterate)
            {
                isFirstIterate = false;
            }
            else
            {
                foreach (var x in _games.Where(x => x.HasGameProcess()))
                {
                    x.AddPlayedTime(10);
                }
            }
            await Task.Delay(10000);
        }
    }
}
