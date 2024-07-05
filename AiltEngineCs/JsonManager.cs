
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

public static class JsonManager
{

    public static T? loadFileJson<T>(string pathFileJson, bool isCreateIfNull=false) where T : class, new()
    {
        T? output = null;

        if (File.Exists(pathFileJson)){
            
            try{

                string fileContent = File.ReadAllText(pathFileJson);

                output = JsonConvert.DeserializeObject<T>(fileContent);

            }catch(Exception e){
                Console.WriteLine($"Exception catch: {e.Message}");
            }

        }else if(isCreateIfNull){

            output = new T();

            if(!saveFileJson(pathFileJson, output)){
                output = null;
            }

        }

        return output;
    }

    public static bool saveFileJson<T>(string pathFileJson, T jsonObj) where T : class, new()
    {
        bool isSuccesslySaved;

        try{

            string fileContent = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);

            File.WriteAllText(pathFileJson, fileContent);

            isSuccesslySaved = true;
        }catch(Exception e){
            isSuccesslySaved = false;
            Console.WriteLine($"Exception catch: {e.Message}");
        }

        return isSuccesslySaved;
    }

    public static string[] getAllNameFiles(string path, string pattern)
    {
        //if path look at no where.
        if (!Directory.Exists(path)){
            throw new Exception("error : path getAllNameFiles look at no where");
        }

        //get all full path match.
        string[] allPathFound = Directory.GetFiles(path, pattern);

        Regex regexWord = new Regex("[a-zA-Z0-9_-]{1,}");
        for(int i=0; i<allPathFound.Length; i++){
            MatchCollection matches = regexWord.Matches(allPathFound[i]); //get all name (folder, file, extention).
            allPathFound[i] = matches[matches.Count-2].Value; //get name file.
        }

        return allPathFound;
    }

}