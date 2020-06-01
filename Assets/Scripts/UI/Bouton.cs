using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Bouton : MonoBehaviour
{
    [Header("Sprite")]
    [SerializeField] private Color couleurOver;
    [SerializeField] private Color couleurEnfonce;
    private SpriteRenderer spR;

    public UnityEvent eventBouton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (spR == null)spR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        if(enabled)spR.color = couleurOver;
    }

    private void OnMouseExit()
    {
        if (enabled) spR.color = Color.white;
    }

    private void OnMouseDown()
    {
        if (enabled) spR.color = couleurEnfonce;
    }

    private void OnMouseUpAsButton()
    {
        if (enabled)
        {
            spR.color = Color.white;
            eventBouton.Invoke();
        }
    }
}
