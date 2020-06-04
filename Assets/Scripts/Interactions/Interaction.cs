using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public abstract class Interaction : MonoBehaviour
{
    public bool enInteraction;
    public bool interactable;
    [SerializeField] protected Button boutonInteraction;

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
