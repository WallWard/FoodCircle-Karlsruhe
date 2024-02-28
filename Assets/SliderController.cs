using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public TextMeshProUGUI sliderValueText;
    private Slider slider;

    private void Start()
    {
        // Finde den Slider im gleichen GameObject
        slider = GetComponent<Slider>();

        // Füge einen Listener für die Wertänderungen des Sliders hinzu
        slider.onValueChanged.AddListener(OnSliderValueChanged);

        // Initial update to display the starting value
        UpdateSliderValueText();
    }

    void UpdateSliderValueText()
    {
        // Display the slider value in the TMP text component
        sliderValueText.text = slider.value.ToString("F2") + "%"; // F2 formats the value to two decimal places
    }

    private void OnSliderValueChanged(float value)
    {
        UpdateSliderValueText();
    }

}
