using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

static public class MappeSysteme
{
    private static string cheminDefaut = "Assets/Mappes/";
    private static string extention = ".mappe";

    private static string baliseNom = "<n>";
    private static string baliseColonne = "<c>";
    private static string baliseLigne = "<l>";
    private static string baliseMappeTerrain = "<t>";

    private static TuileTerrain[] listeTerrains;

    public struct Mappe
    {
        public string nom;
        public TuileTerrain[,] mappeTerrains;
        public int colonnes;
        public int lignes;


        public Mappe(string nomMappe, int nbrColonnes, int nbrLignes, TuileTerrain[,] listeTerrains)
        {
            nom = nomMappe;
            colonnes = nbrColonnes;
            lignes = nbrLignes;
            mappeTerrains = listeTerrains;
        }
    }

    public static void SauvergarderMappe(string nomMappe)
    {
        string cheminMappe = cheminDefaut + nomMappe + extention;

        listeTerrains = GameObject.FindGameObjectWithTag("ListeTerrains").GetComponents<TuileTerrain>();

        DamierGen damierGen = Object.FindObjectOfType<DamierGen>();

        Mappe mappe = new Mappe(nomMappe, damierGen.colonnes, damierGen.lignes, CreerDamierTerrain(damierGen.RecupDamier()));

        if(nomMappe == "")
        {
            if (EditorUtility.DisplayDialog("Manque le nom", "Rentre un nom pour ta mappe !","Oui Léo..."))
            {

            }
        }
        else if(CheckerMappeExiste(nomMappe))
        {
            if(EditorUtility.DisplayDialog("La mappe existe déjà", "La mappe existe déjà, tu veux écraser la sauvegarde ?", "Oui", "Non"))
            {
                SupprimerMappe(cheminMappe);

                CreerFichierMappe(cheminMappe, CreerCodeMappe(mappe));
            }
        }
        else
        {
            CreerFichierMappe(cheminMappe, CreerCodeMappe(mappe));
        }
    }

    //Crée le fichier .mappe et y inscrit le code de la mappe
    private static void CreerFichierMappe(string cheminMappe, string codeMappe)
    {
        StreamWriter ecriteur = new StreamWriter(cheminMappe);

        ecriteur.Write(codeMappe);
        ecriteur.Close();
    }

    //Créer le code qui va être inscrit dans le fichier .mappe
    private static string CreerCodeMappe(Mappe mappe)
    {
        string codeMappe = baliseNom + mappe.nom + baliseColonne + mappe.colonnes + baliseLigne + mappe.lignes + CreerCodeMappeTerrain(mappe.mappeTerrains);

        return codeMappe;
    }


    //Créer la partie du code de la mappe qui concerne les types de terrain
    private static string CreerCodeMappeTerrain(TuileTerrain[,] mappeTerrain)
    {
        string codeMappeTerrain = baliseMappeTerrain;

        for (int y = 0; y < mappeTerrain.GetLength(1); y++)
        {
            for (int x = 0; x < mappeTerrain.GetLength(0); x++)
            {
                codeMappeTerrain += ':' + mappeTerrain[x, y].nom;
            }
        }

        return codeMappeTerrain;
    }

    private static string RecupererCodeMappe(string nomMappe)
    {
        string chemin = cheminDefaut + nomMappe + extention;
        string code = "";

        if(CheckerMappeExiste(nomMappe))
        {
            code = File.ReadAllText(chemin);
        }

        return code;
    }

    private static Mappe ConvertirCodeMappe(string code)
    {
        

        Mappe mappe = new Mappe();

        return mappe;
    }


    //Extrait la carte des terrain de la carte des tuiles.
    private static TuileTerrain[,] CreerDamierTerrain(TuileManager[,] damier)
    {
        TuileTerrain[,] damierTerrains = new TuileTerrain[damier.GetLength(0),damier.GetLength(1)];

        for (int y = 0; y < damier.GetLength(1); y++)
        {
            for (int x = 0; x < damier.GetLength(0); x++)
            {
                damierTerrains[x, y] = damier[x, y].terrainTuile;
            }
        }

        return damierTerrains;
    }
    private static TuileTerrain[,] CreerDamierTerrain(string[] codeTerrain, int colonnes, int lignes)
    {
        TuileTerrain[,] damierTerrains = new TuileTerrain[colonnes, lignes];

        for (int i = 0; i < codeTerrain.Length; i++)
        {
            for (int y = 0; y < lignes; y++)
            {
                for (int x = 0; x < colonnes; x++)
                {
                    damierTerrains[x, y] = ConvertirCodeTerrain(codeTerrain[i]);
                }
            }
        }

        return damierTerrains;
    }

    //Convertit le code du terrain en référence au terrain
    private static TuileTerrain ConvertirCodeTerrain(string code)
    {
        foreach(TuileTerrain terrain in listeTerrains)
        {
            if(terrain.nom == code)
            {
                return terrain;
            }
        }

        return null;
    }

    //Vérifie si la mappe existe en fonction de son nom
    public static bool CheckerMappeExiste(string nomMappe)
    {
        string nomMappeFichier = nomMappe + extention;
        string[] mappes = Directory.GetFiles(cheminDefaut);

        for (int i = 0; i < mappes.Length; i++)
        {
            if(mappes[i] == cheminDefaut + nomMappeFichier)
            {
                return true;
            }
        }
        return false;
    }

    public static void ChargerMappe(string nomMappe)
    {
        DamierGen damierGen = Object.FindObjectOfType<DamierGen>();
        listeTerrains = damierGen.GetComponents<TuileTerrain>();
        //Mappe mappe;

        //damierGen.genererEnTuileHexa(mappe);
    }

    public static void SupprimerMappe(string nomMappe)
    {
        string chemin = cheminDefaut + nomMappe + extention;

        string cheminMeta = chemin + ".meta";

        File.Delete(chemin);
        File.Delete(cheminMeta);

    }

    public static List<string> RecuprererNomMappes()
    {
        List<string> listeSauvegardes = new List<string>(Directory.GetFiles(cheminDefaut));

        for (int i = 0; i < listeSauvegardes.Count; i++)
        {
            if(listeSauvegardes[i].EndsWith(".mappe"))
            {
                string nom = listeSauvegardes[i];

                int indexExtention = nom.IndexOf(extention);
                int indexChemin = nom.IndexOf(cheminDefaut);

                nom = nom.Remove(indexExtention);
                nom = nom.Remove(indexChemin, cheminDefaut.Length);

                listeSauvegardes[i] = nom;

                //Debug.Log(nom);
            }
            else
            {
                listeSauvegardes.RemoveAt(i);
            }
        }

        return listeSauvegardes;
    }
}
