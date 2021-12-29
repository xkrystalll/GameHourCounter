using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private Text _nameText; 
    [SerializeField] private Text _processText; 
    [SerializeField] private Text _playedTimeText; 
    [SerializeField] private Text _isStartedText;

    [SerializeField] private GameInfo gameInfo;

    public bool IsStarted { get => HasGameProcess(); }
    public GameInfo GameInfo { get => gameInfo; private set => gameInfo = value; }

    public void Delete()
    {
        Controller.Instance.Unsubscribe(this);
    }

    public void AddPlayedTime(int seconds)
    {
        GameInfo.playedTime += seconds;
        UpdateInfo();
    }

    public void SetInfo(GameInfo info)
    {
        GameInfo = info;
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        _nameText.text = GameInfo.name;
        _processText.text = GameInfo.processname;
        _playedTimeText.text = FormatTime();

        _isStartedText.text = IsStarted ? "<color=green>yes</color>" : "<color=red>no</color>";
    }

    public string FormatTime()
    {
        string formatedTime = "N/A";
        TimeSpan t = TimeSpan.FromSeconds(GameInfo.playedTime);
        try
        {
            formatedTime = string.Format("{0:D2}h:{1:D2}m",
                            t.Hours,
                            t.Minutes);
        }
        catch { }
        return formatedTime;
    }

    public bool HasGameProcess()
    {
        Process[] foundedProcesses = Process.GetProcessesByName(GameInfo.processname);
        return foundedProcesses.Length > 0;
    }

    private void Start()
    {
        Invoke(nameof(Init), 0.15f);
    }

    private void Init()
    {
        /*Controller.Instance.Subscribe(this);*/
    }
}
