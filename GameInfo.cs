[System.Serializable]
public class GameInfo
{
    public string name;
    public string processname;
    public int playedTime;

    
    public GameInfo(string name, string processname, int playedTime)
    {
        this.name = name;
        this.processname = processname;
        this.playedTime = playedTime;
    }
}
