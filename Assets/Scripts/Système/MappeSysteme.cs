using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

static public class MappeSysteme
{
    private static string cheminDefaut = "Assets/Mappes/";
    private static string extention = ".mappe";

    private const string baliseNom = "<n>";
    private const string baliseColonne = "<c>";
    private const string baliseLigne = "<l>";
    private const string baliseMappeTerrain = "<t>";
    private const char separateurTerrain  = ':' ;

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
        TuileTerrain[,] damierTerrains = CreerDamierTerrain(damierGen.RecupDamier());

        Mappe mappe = new Mappe(nomMappe, damierGen.colonnes, damierGen.lignes, damierTerrains);

        /* DEBUG
        int index = 0;
        for (int y = 0; y < mappe.mappeTerrains.GetLength(1); y++)
        {
            for (int x = 0; x < mappe.mappeTerrains.GetLength(0); x++)
            {

                Debug.Log("tuile" + index + " = " + mappe.mappeTerrains[x, y].nom);
                index++;
            }
        }
        */

        if(nomMappe == "")
        {
            if (EditorUtility.DisplayDialog("Manque le nom", "Rentre un nom pour ta mappe !","Oui Léo..."))
            {
                if(EditorUtility.DisplayDialog(":3","C'est bien mon grand ^^ !","Ouai ouai... TG..."))
                {

                }
            }
        }
        else if(CheckerMappeExiste(nomMappe))
        {
            if(EditorUtility.DisplayDialog("La mappe existe déjà", "La mappe existe déjà, tu veux écraser la sauvegarde ?", "Oui", "Non"))
            {
                SupprimerMappe(nomMappe);

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
                codeMappeTerrain += separateurTerrain + mappeTerrain[x, y].nom;
            }
        }

        return codeMappeTerrain;
    }

    //Récupère le contenu/code d'un fichier .mappe en fonction du nom de la mappe
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

    //Créer un structure Mappe à partir du nom d'un fichier .mappe
    private static Mappe CreerMappe(string nomMappe)
    {
        string code = RecupererCodeMappe(nomMappe);

        char[] tableauCode = code.ToCharArray();
        string balise = "";
        bool estBalise = false;

        string nom = "";
        string colonnesTxt ="";
        string lignesTxt ="";
        string codeTerrain = "";

        for (int i = 0; i < tableauCode.Length; i++)
        {


            //Détecte et stock les balises.
            if (estBalise)
            {
                balise += tableauCode[i];
                
                
            }

            if (tableauCode[i] == '<')
            {
                estBalise = true;
                balise = "<";
            }
            else if (tableauCode[i] == '>')
            {
                estBalise = false;
                continue;
            }


            //Stock le contenu entre les balises.
            switch (balise)
            {
                case baliseNom:
                    nom += tableauCode[i];
                    
                    break;
                case baliseColonne:
                    colonnesTxt += tableauCode[i];
                    
                    break;
                case baliseLigne:
                    lignesTxt += tableauCode[i];
                    
                    break;
                case baliseMappeTerrain:
                    codeTerrain += tableauCode[i];
                    
                    break;
            }
        
        }

        

        int colonnes = int.Parse(colonnesTxt);
        int lignes = int.Parse(lignesTxt);

        char[] separateurs = new char[] { separateurTerrain };
        string[] listeCodeTerrain = codeTerrain.Split(separateurs, System.StringSplitOptions.RemoveEmptyEntries);
       

        TuileTerrain[,] damierTerrains = CreerDamierTerrain(listeCodeTerrain, colonnes, lignes);

        Mappe mappe = new Mappe(nom, colonnes, lignes, damierTerrains);

        return mappe;
    }


    //Extrait la carte des terrain de la carte des tuiles.
    private static TuileTerrain[,] CreerDamierTerrain(TuileManager[,] damier)
    {
        TuileTerrain[,] damierTerrains = new TuileTerrain[damier.GetLength(0),damier.GetLength(1)];

        //int index = 0;
        for (int y = 0; y < damier.GetLength(1); y++)
        {
            for (int x = 0; x < damier.GetLength(0); x++)
            {
                damierTerrains[x, y] = damier[x, y].terrainTuile;

                //Debug.Log("tuile"+index+" = "+damier[x, y].terrainTuile.nom);
                //index++;
            }
        }

        return damierTerrains;
    }
    private static TuileTerrain[,] CreerDamierTerrain(string[] codeTerrain, int colonnes, int lignes)
    {
        TuileTerrain[,] damierTerrains = new TuileTerrain[colonnes, lignes];
        int index = 0;

        for (int y = 0; y < lignes; y++)
        {
            for (int x = 0; x < colonnes; x++)
            {
                damierTerrains[x, y] = ConvertirCodeTerrain(codeTerrain[index]);
                //Debug.Log("index : " + index + " terrain : " + ConvertirCodeTerrain(codeTerrain[index]).nom);
                
                index++;
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
        listeTerrains = GameObject.FindGameObjectWithTag("ListeTerrains").GetComponents<TuileTerrain>();
        Mappe mappe = CreerMappe(nomMappe);

        damierGen.GenDamierHexa(mappe);
    }

    public static void SupprimerMappe(string nomMappe)
    {
        string chemin = cheminDefaut + nomMappe + extention;

        string cheminMeta = chemin + ".meta";

        File.Delete(chemin);
        File.Delete(cheminMeta);

    }

    //Récupère le nom de toutes les mappes
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
