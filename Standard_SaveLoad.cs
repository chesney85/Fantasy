using UnityEngine;

public class Standard_SaveLoad : MonoBehaviour
{
    
   
    public static void Save(string variableName, int variableValue)
    {
        PlayerPrefs.SetInt(variableName,variableValue);
    }
    public static void SaveFloat(string floatName, float value)
    {
        PlayerPrefs.SetFloat(floatName,value);
    }
    public static float LoadInt(string floatName)
    {
        return PlayerPrefs.GetFloat(floatName);
    }

    public static int Load(string variableName)
    {
        return PlayerPrefs.GetInt(variableName);
    }
}
