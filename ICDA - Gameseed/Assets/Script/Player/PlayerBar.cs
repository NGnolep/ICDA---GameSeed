using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Coroutine regenCoroutine;
    void Start()
    {
        currentSanity = initialSanity;
        currentStamina = initialStamina;

        //sanitySlider.maxValue = maxSanity;
        //staminaSlider.maxValue = maxStamina;
    }
    void Update()
    {
        currentSanity -= sanityDegradeRate * Time.deltaTime;
        currentSanity = Mathf.Clamp(currentSanity, 0, maxSanity);
        //Debug.Log(currentSanity);
        //sanitySlider.value = currentSanity;
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
            //staminaSlider.value = currentStamina;
            yield return null; 
        }

        regenCoroutine = null;
    }
}
