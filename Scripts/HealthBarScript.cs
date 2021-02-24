using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    private Image image;
    private Color color;

    private void Start()
    {
        image = GetComponentInChildren<Image>();
        color = Color.black;
        color.a = (float)161 / 255;

    }

    public void SetHealth(int health, int maxHealth)
    {
        float value = (float)health / maxHealth;
        healthText.text = health.ToString() + "/" + maxHealth.ToString();
        healthSlider.value = value;
        if (value>0.5f)
        {
            color.g = 1;
            color.r = (1 - value) * 2;
        }
        else
        {
            color.r = 1;
            color.g = value * 2;
        }
        image.color = color;
    }
}
