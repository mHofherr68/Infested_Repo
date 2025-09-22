using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

public class IncreaseRound : MonoBehaviour
{   // Reference to the RadialFillManager script
    [SerializeField] private RadialFillManager radialFillManager;
    // Maximum number of rounds
    [SerializeField] int maxRound = 3;

    private int currentRound = 0;
    public void IncreaseFill()
    {
        currentRound++;

        if (currentRound > maxRound)
        {
            currentRound = maxRound;
        }

        radialFillManager.UpdateRadialProgressCircle(currentRound, maxRound);
    }


    public void DecreaseFill()
    {
        currentRound--;

        if(currentRound < 0)
        {
            currentRound = 0;
        }
        radialFillManager.UpdateRadialProgressCircle(currentRound, maxRound);
}
    // Start is called once before the first execution of Update after the MonoBehaviour is created

}
