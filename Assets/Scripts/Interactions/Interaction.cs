using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Interaction : MonoBehaviour
{
    public static Interaction EnCours
    {
        get
        {
            Interaction[] interactions = FindObjectsOfType<Interaction>();
            foreach (Interaction interaction in interactions)
            {
                if (interaction.enInteraction) return interaction;
            }
            return null;
        }
    }
    public bool enInteraction;
    public bool interactable;
    [SerializeField] protected Button boutonInteraction;
    [SerializeField] protected Button boutonRetour;

    protected virtual void Start()
    {
        boutonRetour.onClick.AddListener(delegate { EntrerEnInteraction(false); });
    }

    public virtual void EntrerEnInteraction(bool entrer)
    {
        if(enInteraction != entrer)
        {
            enInteraction = entrer;

            if (enInteraction)
            {
                ControleSouris.Actuel.ActiverModeInteraction(this,true); 
                CameraControle.Actuel.CentrerCamera(transform.position, true);
            }
            else
            {
                CameraControle.Actuel.controlesActives = true;
                ControleSouris.Actuel.ActiverModeInteraction(this, false);
            }
        }
    }

    public virtual void ActiverInteraction()
    {
        interactable = true;
        boutonInteraction.gameObject.SetActive(true);
    }
    public virtual void DesactiverInteraction()
    {
        interactable = false;
        boutonInteraction.gameObject.SetActive(false);
    }

    protected void ActiverBouton(bool activer)
    {
        boutonInteraction.interactable = activer;
    }
}
