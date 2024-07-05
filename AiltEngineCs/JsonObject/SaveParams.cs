
public class SaveParams
{
    //base params.
    public int id;
    public string name = "";


    //time played.
    public TimeSpan timeTotalInGame = new();
    public string getTimeTotalInGameStr() //get time of save in string format (for print).
    {
        string output = Math.Floor(timeTotalInGame.TotalHours).ToString() +"h "; //all hours floor att unite.

        output += timeTotalInGame.ToString(@"mm") +"m "; //concat minutes.
        output += timeTotalInGame.ToString(@"ss") +"s"; //concat secondes.

        return output;
    }


    //keyboard.
    public string keyboard = "azerty";
    public Dictionary<char, char>? customKeyboard;


    //twitch.
    public string channelTwitchToConnect = "";
    public string cmdTwitchJoinVillage = "!joinVillage";


    //villageoi.
    public Stats[] villageoiStats = new Stats[]{};


}