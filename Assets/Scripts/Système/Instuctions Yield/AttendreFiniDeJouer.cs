using UnityEngine;

public class AttendreFiniDeJouer<T> : CustomYieldInstruction 
    where T : Pion
{
    public override bool keepWaiting
    {
        get
        {
            return CheckerZonFiniDeJouer();
        }
    }

    //constructeur public 
    public AttendreFiniDeJouer() { }

    //exemple : yield return new AttendreFiniDeJouer<Tribu>(); 
    //signification : la coroutine est mis en pause tant que toutes les tribus ont pas joué.

    // Vérifie que toutes les instances du type de pion ont passé leur tour
    private bool CheckerZonFiniDeJouer()
    {
        T[] pions = UnityEngine.Object.FindObjectsOfType<T>();

        foreach(T pion in pions)
        {
            if (!pion.aPasseSonTour)
                return false;
        }
        return true;
    }
}
