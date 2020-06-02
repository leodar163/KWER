using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEditor.UIElements;

[CreateAssetMenu(fileName = "nvlleRecette", menuName = "Recette")]
public class Recette : ScriptableObject
{
    public string nom;
    public int slots;
    public Production production;
    public Production cout;



    public void Sauvegarder()
    {
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
}
