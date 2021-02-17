using UnityEngine;

public class Easy_SaveLoad : MonoBehaviour
{
    #region Singleton

    public static Easy_SaveLoad instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

    #endregion

    #region Floats

    public static void SaveFloat(string floatName, float _value)
    {
        ES3.Save(floatName,_value);
    }
    public static float LoadFloat(string floatName)
    {
        return ES3.Load<float>(floatName);
    }

    #endregion

    #region Ints

    public static void Save(string variableName,float VariableToSave)
    {

    }

    #endregion
   
}
