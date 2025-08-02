using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerBar : MonoBehaviour
{
    public float maxSanity = 100f;
    public float maxStamina = 100f;

    public float initialSanity = 50f;
    public float initialStamina = 100f;

    public float currentSanity;
    public float currentStamina;

    public float staminaRegenRate = 3f;
    public float sanityDegradeRate = 0.2f;
    public bool isBeingChased = false;
    public float chaseSanityMultiplier = 3f;
    private Coroutine regenCoroutine;
    public Slider sanitySlider;
    public Slider staminaSlider;
    void Start()
    {
        currentSanity = initialSanity;
        currentStamina = initialStamina;

        sanitySlider.maxValue = maxSanity;
        staminaSlider.maxValue = maxStamina;

        sanitySlider.value = currentSanity;
        staminaSlider.value = currentStamina;
    }
    void Update()
    {
        float drainRate = isBeingChased ? sanityDegradeRate * chaseSanityMultiplier : sanityDegradeRate;
        currentSanity -= drainRate * Time.deltaTime;
        currentSanity = Mathf.Clamp(currentSanity, 0, maxSanity);
        sanitySlider.value = currentSanity;
        staminaSlider.value = currentStamina;
    }

    public void UseStamina(float amount)
    {
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        if (regenCoroutine != null)
        {
            StopCoroutine(regenCoroutine);
        }

        regenCoroutine = StartCoroutine(RegenerateStamina());
    }

    public void RegainSanity(float amount)
    {
        currentSanity += amount;
        currentSanity = Mathf.Clamp(currentSanity, 0, maxSanity);
    }
    private IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds(3f);
        while (!Input.GetKey(KeyCode.LeftShift) && currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
            staminaSlider.value = currentStamina;
            yield return null; 
        }

        regenCoroutine = null;
    }
}
