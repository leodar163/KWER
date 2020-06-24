using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

    [CustomEditor(typeof(Evenement), true)]
public class EvenementEditor : Editor
{
    private Evenement evenement;
    int oih = 0;
    public override void OnInspectorGUI()
    {
        evenement = (Evenement)target;

        base.OnInspectorGUI();

        EcrireEffets();


        GUILayout.Space(20);
        if(GUILayout.Button("SAUVEGARDER"))
        {
            evenement.Sauvegarder();
        }
    }

    private void EcrireEffets()
    {
        SerializedProperty effet;
        SerializedProperty cible;
        SerializedProperty methode;
        SerializedProperty argument;

        SerializedObject so = new SerializedObject(evenement);

        if (oih == 0)
        {
            
            //SerializedProperty it = so.GetIterator();
            //while (it.Next(true))
            //{ // or NextVisible, also, the bool argument specifies whether to enter on children or not
            //    Debug.Log(it.propertyPath);
            //}
            oih++;
        }

        for (int i = 0; i < evenement.listeChoix.Count; i++)
        {
            effet = so.FindProperty("listeChoix.Array.data[" + i + "].effets.m_PersistentCalls.m_Calls.Array");
            Evenement.Choix choix = evenement.listeChoix[i];
            InitiliserListeRetour(choix.retoursEffets, effet.arraySize);

            for (int j = 0; j < effet.arraySize; j++)
            {

                cible = effet.FindPropertyRelative("data[" + j + "].m_target");
                methode = effet.FindPropertyRelative("data[" + j + "].m_MethodName");

                if (methode.stringValue.Contains("Gain"))
                {
                    argument = effet.FindPropertyRelative("data[" + j + "].m_Arguments.m_FloatArgument");
                    string retour = methode.stringValue.Remove(0, 4);

                    if (argument.floatValue > 0)
                    {

                        retour += " <color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.CouleurTexteBonus) + ">+" + argument.floatValue;
                    }
                    else if (argument.floatValue < 0)
                    {
                        retour += " <color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + "> " + argument.floatValue;
                    }
                    else if (argument.floatValue == 0)
                    {
                        retour = "";
                    }
                
                    choix.retoursEffets[j] = retour;
                }
            }
        }
    }

    private void InitiliserListeRetour(List<string> retours, int ettendue)
    {
        int difference = ettendue - retours.Count;
        if(difference > 0)
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
        else if(difference <= 0)
        {
            for (int i = 0; i < retours.Count; i++)
            {
                retours[i] = "";
            }
        }
    }
}
