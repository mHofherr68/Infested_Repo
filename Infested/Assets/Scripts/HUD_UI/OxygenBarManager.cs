using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBarManager : MonoBehaviour
{
    public float maxOxygen= 100;
    
    public float currentOxygen;

    [Header("Auto Depletion Settings")]
    public bool enableAutoDepletion = true; // Toggle for enabling/disabling O2 depletion
    public float depletionInterval = 15f; // Interval of O2 depletion in seconds
    public float depletionPercentage = 1f; // Percentage of depletion per interval


    // [SerializeField] private Slider slider;
    // public Gradient gradient;
    [SerializeField] private Image oxygenBarFilling;

    private Coroutine depletionCoroutine;

    void Start()
    {
        oxygenBarFilling = GameObject.Find("OxygenBarCircular/Fill").GetComponent<Image>();
        currentOxygen = maxOxygen;

        if (enableAutoDepletion) 
        {
            StartOxygenDepletion();
        }
    }

    /*
    public void SetMaxOxygen(int oxygen) 
    {
        slider.maxValue = oxygen;
        slider.value = oxygen;

        // oxygenBarFilling.color = gradient.Evaluate(1f); 
    }

    public void SetOxygen(int oxygen) 
    {
        slider.value = oxygen;

        // oxygenBarFilling.color = gradient.Evaluate(slider.normalizedValue);
    }
    */

    public void AddOxygen(float oxygen)
    {
        currentOxygen += oxygen;
        if (currentOxygen > maxOxygen)
            currentOxygen = maxOxygen;
        UpdateOxygen();
    }

    public bool RemoveOxygen(float oxygen)
    {
        currentOxygen -= oxygen;
        if (currentOxygen <= 0)
        {
            currentOxygen = 0;
            Debug.Log("Game Over");
            UpdateOxygen();
            return true;

        }
        UpdateOxygen();
        return false;
    }

    private void UpdateOxygen()
    {
        oxygenBarFilling.fillAmount = (currentOxygen / maxOxygen);
    }

    // Coroutine for automatic oxygen depletion
    private IEnumerator OxygenDepletionCoroutine()
    {
        while (enableAutoDepletion && currentOxygen > 0)
        {
            yield return new WaitForSeconds(depletionInterval);

            if (enableAutoDepletion && currentOxygen > 0)
            {
                float oxygenToRemove = maxOxygen * (depletionPercentage / 100f);
                RemoveOxygen(oxygenToRemove);
            }
        }
    }

    // Public methods to control auto depletion
    public void StartOxygenDepletion()
    {
        if (depletionCoroutine == null && currentOxygen > 0)
        {
            enableAutoDepletion = true;
            depletionCoroutine = StartCoroutine(OxygenDepletionCoroutine());
        }
    }

    public void StopOxygenDepletion()
    {
        enableAutoDepletion = false;
        if (depletionCoroutine != null)
        {
            StopCoroutine(depletionCoroutine);
            depletionCoroutine = null;
        }
    }

    public void PauseOxygenDepletion()
    {
        enableAutoDepletion = false;
    }

    public void ResumeOxygenDepletion()
    {
        if (currentOxygen > 0)
        {
            enableAutoDepletion = true;
            if (depletionCoroutine == null)
            {
                StartOxygenDepletion();
            }
        }
    }

    void OnDisable()
    {
        StopOxygenDepletion();
    }

}
