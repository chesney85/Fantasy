using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BaseStats))]
public class BaseStatEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        BaseStats baseStats = (BaseStats)target;
        
        if (GUILayout.Button("Invoke event"))
        {
            baseStats.InvokeEventss();
        }
    }
}
