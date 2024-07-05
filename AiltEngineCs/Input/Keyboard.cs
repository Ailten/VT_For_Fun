
public class Keyboard
{
    // <--- Keyboard managing (START) --->
    private static Keyboard[] keyboards = new Keyboard[]{ //all keyboard remap.
        //new(){ //not used.
        //    name = "qwerty",
        //    remapedKey = new(){ 
        //        {'ā', '\n'}, //enter.
        //        {'Ā', '\r'} //escape.
        //    }
        //},
        new(){
            name = "azerty",
            remapedKey = new(){
                {'Q', 'A'},
                {'A', 'Q'},
                {'W', 'Z'},
                {'Z', 'W'},
                {';', 'M'},
                {'ā', '\n'}, //enter.
                {'Ā', '\r'} //escape.
            }
        }
    };

    public static char remap(char charToRemap)
    {
        foreach(Keyboard k in keyboards){
            if(k.name == MenuSave.level.getSaveSelected.keyboard){ //keyboard find.
                if(k.remapedKey.ContainsKey(charToRemap)) //keyboard has remap for this key.
                    return k.remapedKey[charToRemap];
            }
        }
        return charToRemap;
    }
    public static char remap(string keyboardName, char charToRemap)
    {
        foreach(Keyboard k in keyboards){
            if(k.name == keyboardName){ //keyboard find.
                if(k.remapedKey.ContainsKey(charToRemap)) //keyboard has remap for this key.
                    return k.remapedKey[charToRemap];
            }
        }
        return charToRemap;
    }
    public static char reverceRemap(char charToReverceRemap)
    {
        foreach(Keyboard k in keyboards){
            if(k.name == MenuSave.level.getSaveSelected.keyboard){
                foreach(KeyValuePair<char, char> remapKeyValue in k.remapedKey){
                    if(remapKeyValue.Value == charToReverceRemap)
                        return remapKeyValue.Key;
                }
            }
        }
        return charToReverceRemap;
    }
    public static char reverceRemap(string keyboardName, char charToReverceRemap)
    {
        foreach(Keyboard k in keyboards){
            if(k.name == keyboardName){
                foreach(KeyValuePair<char, char> remapKeyValue in k.remapedKey){
                    if(remapKeyValue.Value == charToReverceRemap)
                        return remapKeyValue.Key;
                }
            }
        }
        return charToReverceRemap;
    }

    public static void loadKeyboard(Dictionary<char, char> customKeyboardDictionary)
    {
        List<Keyboard> keyboardNewList = new List<Keyboard>(keyboards);
        keyboardNewList.Add(new Keyboard(){
            name = "customKeyboard",
            remapedKey = customKeyboardDictionary
        });
        keyboards = keyboardNewList.ToArray();
    }
    public static void makeACustomKeyboard()
    {
        Dictionary<char, char> copyDictionary = new(); //duplicate dictionary of current keyboard.
        //foreach(Keyboard k in keyboards){
        //    if(k.name == MenuSave.level.getSaveSelected.keyboard){
        //        foreach(KeyValuePair<char, char> keyValue in k.remapedKey){
        //            copyDictionary.Add(keyValue.Key, keyValue.Value);
        //        }
        //    }
        //}
        
        List<Keyboard> keyboardNewList = new List<Keyboard>(keyboards);
        keyboardNewList.Add(new Keyboard(){
            name = "customKeyboard",
            remapedKey = copyDictionary
        });
        keyboards = keyboardNewList.ToArray();
    }

    public static void editKeyValue(char key, char value)
    {
        foreach(Keyboard k in keyboards){
            if(k.name == MenuSave.level.getSaveSelected.keyboard){
                if(k.remapedKey.ContainsKey(key)){
                    k.remapedKey[key] = value;
                }else{
                    k.remapedKey.Add(key, value);
                }
            }
        }
    }

    public static Dictionary<char, char> getCustomKeyboard()
    {
        foreach(Keyboard k in keyboards){
            if(k.name == "customKeyboard"){
                return k.remapedKey;
            }
        }
        throw new Exception("error : customKeyboard not found");
    }

    public static char castCharEnToFr(char charEn)
    {
        foreach(Keyboard k in keyboards){
            if(k.name == "azerty" && k.remapedKey.ContainsKey(charEn)){
                return k.remapedKey[charEn];
            }
        }
        return charEn;
    }
    public static char castCharFrToEn(char charFr)
    {
        foreach(Keyboard k in keyboards){
            if(k.name == "azerty" && k.remapedKey.ContainsValue(charFr)){
                return k.remapedKey.ToList().Find((keyValue) => {
                    return keyValue.Value == charFr;
                }).Key;
            }
        }
        return charFr;
    }

    public static string charToStringPrintable(char charToPrint)
    {
        switch(charToPrint){
            case('\n'):
                return "entrer";
            case('\r'):
                return "echappe";
            case('ĉ'):
                return "haut";
            case('ć'):
                return "gauche";
            case('Ĉ'):
                return "bas";
            case('Ć'):
                return "droite";
            default:
                return charToPrint.ToString();
        }

    }
    // <--- Keyboard managing (END) --->

    private string name = "";

    private Dictionary<char, char> remapedKey = new();

}