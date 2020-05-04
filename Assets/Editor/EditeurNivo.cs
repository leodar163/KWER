using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class EditeurNiveau : EditorWindow
{
    string textBoutonActiverModePeinture = "activer mode peinture";
    bool modePeinture = false;
    string nomMappeSauvegarde = "Nom de la sauvegarde";

    TuileTerrain[] listeTerrains;
    TuileTerrain terrainSelectionne;

    string nomTerrainSelectionne;

    Rect rectangleAirePalette;

    Sprite spriteTerrain = null;

    Vector3 positionCameraMemoire;

    [MenuItem("Window/Editeur de Nivo")]
    public static void AfficherFenetre()
    {
        GetWindow<EditeurNiveau>("Editeur de Nivo");
    }

    private void OnFocus()
    {
        Init();
    }


    private void Init()
    {
        listeTerrains = GameObject.FindGameObjectWithTag("ListeTerrains").GetComponents<TuileTerrain>();
    }

    private void Update()
    {
        Repaint();
    }

    private void OnGUI()
    {
        GUILayout.Space(15);

        if (GUILayout.Button(textBoutonActiverModePeinture))
        {
            if(modePeinture)
            {
                textBoutonActiverModePeinture = "activer mode peinture";
                modePeinture = false;
            }
            else
            {
                textBoutonActiverModePeinture = "désactiver mode peinture";
                modePeinture = true;
            }
        }

        GUILayout.Space(13);

        GUILayout.Label("Palette des terrains");

        

        Peindre();

        rectangleAirePalette = new Rect(5, 75, 150, 300);
        Rect rectanglePrevisu = new Rect(160, 75, 150, 300);

        GUILayout.BeginArea(rectanglePrevisu);

        if (spriteTerrain)
        {

            GUIStyle texteCentre = GUI.skin.GetStyle("Label");
            texteCentre.alignment = TextAnchor.UpperCenter;

            GUILayout.Label(nomTerrainSelectionne, texteCentre);
            

            Graphics.DrawTexture(new Rect(23,25, 100, 100), spriteTerrain.texture);
            
        }
            
        GUILayout.EndArea();


        GUILayout.BeginArea(rectangleAirePalette);

        CreerBoutonsTerrain();

        GUILayout.EndArea();

        GUILayout.Space(150);

        GUILayout.Label("Sauvegardes");

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        nomMappeSauvegarde = GUILayout.TextField(nomMappeSauvegarde);
        if(GUILayout.Button("Sauvegarder Mappe"))
        {
            MappeSysteme.SauvergarderMappe(nomMappeSauvegarde);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        AfficherInterfacesSauvegarde();
    }

    private void CreerBoutonsTerrain()
    {
        for (int i = 0; i < listeTerrains.Length; i++)
        {
            if (GUILayout.Button(listeTerrains[i].nom))
            {
                Selection.objects = new GameObject[0];
                //Debug.Log(listeTerrains[i].nom);

                terrainSelectionne = listeTerrains[i];
                spriteTerrain = listeTerrains[i].sprite;
                nomTerrainSelectionne = listeTerrains[i].nom;

                //Debug.Log(terrainSelectionne);
            }
        }
    }

    #region SAUVEGARDES

    private void AfficherInterfacesSauvegarde()
    {
        List<string> listeMappes = MappeSysteme.RecuprererNomMappes();
        for (int i = 0; i < listeMappes.Count; i++)
        {
            CreerIntefaceSauvegarde(listeMappes[i]);
        }
    }

    private void CreerIntefaceSauvegarde(string nomMappe)
    {
        GUILayout.BeginHorizontal();

        GUILayout.Label(nomMappe);

        GUI.backgroundColor = new Color(0.3f, 0.8f, 0.3f);
        if (GUILayout.Button("CHARGER"))
        {
            MappeSysteme.ChargerMappe(nomMappe);
        }

        GUI.backgroundColor = new Color(0.8f, 0.3f, 0.3f);
        if (GUILayout.Button("SUPPRIMER"))
        {
            MappeSysteme.SupprimerMappe(nomMappe);
        }

        GUILayout.EndHorizontal();
    }
    #endregion

    private void Peindre()
    {
        if (modePeinture)
        {
            GameObject[] gOScene = Resources.FindObjectsOfTypeAll<GameObject>();

            foreach (GameObject go in gOScene)
            {
                if(go.name == "Canvas")
                {
                    go.gameObject.SetActive(false);
                }
               
            }

            foreach (GameObject go in Selection.gameObjects)
            {
                if (go.GetComponent<TuileManager>())
                {
                    TuileManager tuile = go.GetComponent<TuileManager>();

                    //Debug.Log(terrainSelectionne.nom);
                    if(tuile.gameObject.name != "TuileHexa")
                    {
                    tuile.SetTerrain(terrainSelectionne);
                    }
                }
            }
        }
        else
        {
            GameObject[] gOScene = Resources.FindObjectsOfTypeAll<GameObject>();

            foreach (GameObject go in gOScene)
            {
                if (go.name == "Canvas")
                {
                    go.gameObject.SetActive(true);
                }
                    

            }

        }
    }

    private void Deselection()
    {
        Selection.objects = new GameObject[0];
    }
}
