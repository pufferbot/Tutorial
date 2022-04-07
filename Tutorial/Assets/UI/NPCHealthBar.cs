using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCHealthBar : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] float lerpDuration = .05f;

    public void SetHealth(float amount)
    {
        amount = 1 / amount;
        StartCoroutine(BarLerp(healthBar, healthBar.fillAmount, amount));
    }

    IEnumerator BarLerp(Image valueToLerp, float startValue, float endValue)
    {
        float timeElapsed = 0;

        while (timeElapsed < lerpDuration)
        {
            valueToLerp.fillAmount = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        valueToLerp.fillAmount = endValue;
    }

}
