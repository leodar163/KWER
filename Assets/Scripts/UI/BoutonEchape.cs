using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoutonEchape : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            CliquerBoutonEchappe();
        }
    }

    public void CliquerBoutonEchappe()
    {
        FenetreValidation.OuvrirFenetreValidation("Etes-vous sûr de vouloir quitter le jeu ? " +
            "\nIl n 'y a pas de système de sauvegarde, toute votre progression sera perdue", QuitterJeu);
    }

    private void QuitterJeu()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
