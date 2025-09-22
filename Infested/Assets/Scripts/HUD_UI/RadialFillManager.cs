using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialFillManager: MonoBehaviour
{
    // Reference to the Image component that represents the radial fill
    [SerializeField] Image radialFillImage;
    // Duration of the fill animation
    [SerializeField] float fillDuration = 1f;

  

    // Variable storing the current fill coroutine, prevents multiple coroutines running at once
    private Coroutine fillCoroutine = null;

    public void UpdateRadialProgressCircle(int valueToFill, int maxFill) 
    {
        float targetFillAmount = (float)(valueToFill + 1) / maxFill;

        if (fillCoroutine != null)
        {
            StopCoroutine(fillCoroutine);
        }

        fillCoroutine = StartCoroutine(AnimateFill(targetFillAmount));
    }


    private IEnumerator AnimateFill(float targetFillAmount) 
    {
        // Store the initial fill amount
        float initialFillAmount = radialFillImage.fillAmount;
        // Initialize time to track the animation's progress
        float elapsedTime = 0f;

        // While-loop: Animate the fill amount until the fill duration is reached
        while (elapsedTime < fillDuration)
        {
            // Increment elapsed time
            elapsedTime += Time.deltaTime;
            // Gradually fill the fill amount using linear interpolation (lerp)
            radialFillImage.fillAmount = Mathf.Lerp(initialFillAmount, targetFillAmount, elapsedTime / fillDuration);
            // Wait until the next frame to continue the animation
            yield return null;
        }

        // Ensure the fill amount is set to the target value at the end of the animation
        radialFillImage.fillAmount = targetFillAmount;
        // Clear reference to the coroutine
        fillCoroutine = null;

    }
}
