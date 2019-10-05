using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFlashOnEvent : MonoBehaviour
{
    private float damageAlphaValue = 0.0f;
    private float damagePerHit = 0.2f;
    private float decayRate = 0.35f;

    private Image overlayImage;

    private void Awake()
    {
        overlayImage = GetComponent<Image>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            TakeDamage();
        }

        if(damageAlphaValue > 0.0f)
        {
            damageAlphaValue -= Time.deltaTime * decayRate;
        }

        overlayImage.color = new Color(overlayImage.color.r, overlayImage.color.g, overlayImage.color.b, damageAlphaValue);
    }

    public void TakeDamage()
    {
        damageAlphaValue += damagePerHit;
        damageAlphaValue = Mathf.Clamp(damageAlphaValue, 0, 1);
    }
}
