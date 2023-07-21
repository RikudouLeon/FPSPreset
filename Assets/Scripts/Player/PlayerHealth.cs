using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{   
    private float health;
    private float lerpTimer;

    [Header("Health Bar")]

    public float maxHealth = 100;
    public float chipSpeed = 2f;

    public Image frontHealthBar;
    public Image backHealthBar;

    [Header("Damage Overlay")]
    public Image overlay; // the DamageOverlay GameObject
    public float duration; // how long the image stays fully opaque
    public float fadeSpeed; // how quickly the image will fade

    private float durationTimer; // timer to check against the duration

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0); // no damage overlay when game starts
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth); // Limits health to never drop below 0 and never rises above 100
        UpdateHealthUI();

        if (overlay.color.a > 0)
        {
            if (health < 30)
            {
                return;
            }
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                // fade image
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha); // every tick of update, temp value is set to the alpha of the image, then decrement the temp value, then assigned back into alpha channel. Fade until alpha < 0
            }
        }
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;

        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }

        //Debug.Log("Your health is: " + health);
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        lerpTimer = 0f;
        durationTimer = 0;

        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1); // full damage overlay when player is damaged
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }
}
