using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Buff), true)]
public class BuffEditor : Editor 
{
    [SerializeField] private GameObject effetRef;
    Buff buff;
    Color typeSelectionne = new Color(20, 20, 150);

    public override void OnInspectorGUI()
    {
        buff = (Buff)target;

        base.OnInspectorGUI();

        if(buff.compteurTour)
        {
            GUILayout.Space(10);
            GUILayout.Label("Nombre de tour que dure l'effet");
            buff.nombreTour = EditorGUILayout.IntField(buff.nombreTour);
            GUILayout.Space(30);
        }

        DessinerBoutonType();

        GUILayout.Space(30);

        if (GUILayout.Button("SAUVEGARDER"))
        {
            Sauvegarder();
        }

        EcrireEffets();

        buff.nombreTour = Math.Abs(buff.nombreTour);
    }

    private void DessinerBoutonType()
    {
        GUILayoutOption[] options = new GUILayoutOption[2] { GUILayout.Width(150), GUILayout.Height(30) };

        GUILayout.Label("Type de décompte");
        GUILayout.Space(10);

        ColorerSelection(buff.compteurTour);
        if (GUILayout.Button("Compteur de Tour", options))
        {
            buff.compteurTour = true;
            buff.tpsDuneTechno = false;
            buff.tpsDunEvent = false;
        }

        ColorerSelection(buff.tpsDunEvent);
        if (GUILayout.Button("Buff d'événement", options))
        {
            buff.compteurTour = false;
            buff.tpsDuneTechno = false;
            buff.tpsDunEvent = true;
        }

        ColorerSelection(buff.tpsDuneTechno);
        if (GUILayout.Button("Buff de technologie", options))
        {
            buff.compteurTour = false;
            buff.tpsDuneTechno = true;
            buff.tpsDunEvent = false;
        }
        ColorerSelection(false);
    }

    private void ColorerSelection(bool selectionne)
    {
        if (selectionne)
        {
            GUI.backgroundColor = typeSelectionne;
        }
        else
        {
            GUI.backgroundColor = Color.white;
        }
    }

    private void EcrireEffets()
    {
        SerializedProperty effet;
        SerializedProperty cible;
        SerializedProperty methode;
        SerializedProperty argumentFloat;
        SerializedProperty argumentInt;

        SerializedObject so = new SerializedObject(buff);

        effet = so.FindProperty("effets.m_PersistentCalls.m_Calls.Array");

        InitialiserListeRetour(buff.listeEffetsRetours, effet.arraySize);
        
        for (int i = 0; i < effet.arraySize; i++)
        {
            cible = effet.FindPropertyRelative("data[" + i + "].m_Target");
            methode = effet.FindPropertyRelative("data[" + i + "].m_MethodName");
            argumentFloat = effet.FindPropertyRelative("data[" + i + "].m_Arguments.m_FloatArgument");
            argumentInt = effet.FindPropertyRelative("data[" + i + "].m_Arguments.m_IntArgument");
            string retour = "";


            if (methode.stringValue.Contains("Bonus"))
            {
                if (methode.stringValue.Contains("Attaque"))
                {
                    char pluriel = '\0';
                    if (Math.Abs(argumentInt.intValue) != 1) pluriel = 's';
                    if (argumentInt.intValue > 0)
                    {
                        retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus) + ">+"
                            + argumentInt.intValue + "<color=\"white\">" + " point" + pluriel + " d'attaque ";
                    }
                    else if (argumentInt.intValue < 0)
                    {
                        retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">"
                            + argumentInt.intValue + "<color=\"white\">" + " point" + pluriel + " d'attaque ";
                    }

                    buff.antiEffets.effets.Add(delegate
                    {
                        effetRef.GetComponent<EffetBonus>().ajouterBonusAttaque(argumentInt.intValue, buff.antiEffets.tribuAffectee);
                    });
                }
                else if (methode.stringValue.Contains("Defense"))
                {
                    char pluriel = '\0';
                    if (Math.Abs(argumentInt.intValue) != 1) pluriel = 's';
                    if (argumentInt.intValue > 0)
                    {
                        retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus) + ">+"
                            + argumentInt.intValue + "<color=\"white\">" + " point" + pluriel + " de défense ";
                    }
                    else if (argumentInt.intValue < 0)
                    {
                        retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">"
                            + argumentInt.intValue + "<color=\"white\">"+ " point" + pluriel + " de défense ";
                    }

                    buff.antiEffets.AddListener(delegate {
                        effetRef.GetComponent<EffetBonus>().ajouterBonusDefense(argumentInt.intValue * -1);});
                }
            }

            if (buff.compteurTour)
            {
                char pluriel = '\0';
                if (buff.nombreTour > 0) pluriel = 's';
                retour += "pendant " + buff.nombreTour + " tour" + pluriel;
            }
            else if (buff.tpsDuneTechno)
            {

            }
            else if(buff.tpsDunEvent)
            {
                retour += "jusqu'à la fin de l'évenement";
            }

            buff.listeEffetsRetours[i] = retour;
        }
    
    }
    private void InitialiserListeRetour(List<string> retours, int ettendue)
    {
        int difference = ettendue - retours.Count;
        if (difference > 0)
        {
            for (int i = 0; i < retours.Count; i++)
            {
                retours[i] = "";
            }
            for (int i = 0; i < difference; i++)
            {
                retours.Add("");
            }
        }
        else if (difference <= 0)
        {
            for (int i = 0; i < retours.Count; i++)
            {
                retours[i] = "";
            }
        }
    }

    private void Sauvegarder()
    {
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(target);
        AssetDatabase.SaveAssets();
        EcrireEffets();
    }
}
