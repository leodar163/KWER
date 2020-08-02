using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Consommable),true)]
public class ConsommableEditor : Editor
{
    private Consommable consommable;
    public override void OnInspectorGUI()
    {
        consommable = (Consommable)target;
        base.OnInspectorGUI();

        if(consommable.type == Consommable.typeConsommable.amenagement)
        {
            GUILayout.Space(10);
            GUILayout.Label("Aménagement");
            consommable.amenagement = (Amenagement)EditorGUILayout.ObjectField(consommable.amenagement, typeof(Amenagement),true);
        }
        else if (consommable.type == Consommable.typeConsommable.buff)
        {
            GUILayout.Space(10);
            GUILayout.Label("Buff");
            consommable.buff = (Buff)EditorGUILayout.ObjectField(consommable.buff, typeof(Buff), true);
        }
        GUILayout.Space(20);
        if (GUILayout.Button("SAUVEGARDER"))
        {
            Sauvegarder();
        }

        if(consommable.buff)
        {
            consommable.texteRetour = consommable.buff.Retours;
        }
        else if(consommable.amenagement)
        {
            consommable.texteRetour = consommable.amenagement.Effets;
        }
    }

    private void Sauvegarder()
    {
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(target);
        AssetDatabase.SaveAssets();
    }
}
