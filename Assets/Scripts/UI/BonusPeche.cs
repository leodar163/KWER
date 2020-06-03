using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPeche : MonoBehaviour
{

    
    [SerializeField]float positionZ;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AfficherBonusPeche(Vector3 position, float gainNourriture)
    {
        if(position != new Vector3())
        {
            Vector3 direction = position;
            direction.z = positionZ;
            transform.position = direction;;
        }
    }

    public void CacherBonusPeche()
    {
        Vector3 positionCachee = transform.position;
        positionCachee.z = -40;

        transform.position = positionCachee;
    }
}
