using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectGame : MonoBehaviour
{
    [SerializeField] private Dropdown _dropdown;
    [SerializeField] private GameInfo selectedProcess;
    [SerializeField] private Data _data;
    [SerializeField] private Button _addBtn;

    private void Start()
    {
        _addBtn.interactable = false;
        Invoke(nameof(FillDropDown), 0.5f);
    }

    private void FillDropDown()
    {
        var data = _data.LoadSavedGames();
        List<string> processes = new List<string>();
        foreach (var x in Process.GetProcesses())
        {
            if (processes.Contains(x.ProcessName) || data.Any(y => y.processname == x.ProcessName))
                continue;
            processes.Add(x.ProcessName);
        }
        foreach (var x in processes)
        {
            _dropdown.options.Add(new Dropdown.OptionData(x));
        }
    }

    public void OnGameSelected(int index)
    {
        selectedProcess = new GameInfo(_dropdown.options[index].text.ToUpper().Replace(".exe", ""), _dropdown.options[index].text, 0);
        if (selectedProcess != null)
            _addBtn.interactable = true;
    }

    public void AddSelectedGame()
    {
        if (_data.LoadSavedGames().Any(x => x.processname == selectedProcess.processname))
            return;
        _data.AddGame(selectedProcess);
        ClearDropDown();
        FillDropDown();
    }

    private void ClearDropDown()
    {
        _dropdown.options.Clear();
    }
}
