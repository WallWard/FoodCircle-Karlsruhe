using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImportController : MonoBehaviour
{
    public Slider slider;
    public float anteilFaktor;

    public float emissionenProEinheit = 100f;
    public float biomasseProEinheit = 22f;
    public float müllProEinheit = 120f;
    public float emission, müll, biomasse;
    public float emissionPrognose, müllPrognose, biomassePrognose;

    private float emissionsPrognoseAnteil, biomassePrognoseAnteil, müllPrognoseAnteil;


    public TextMeshProUGUI importText;

    void Start()
    {
        anteilFaktor = (100.0f - slider.value) / 100;
        // Füge einen Listener für die Wertänderungen des Sliders hinzu
        slider.onValueChanged.AddListener(OnSliderValueChanged);

        Timer.Instance.OnSecondElapsed += HandleTick;

        //feste Referenz-Werte
        emissionsPrognoseAnteil = emissionenProEinheit * anteilFaktor;
        müllPrognoseAnteil = müllProEinheit * anteilFaktor;
        biomassePrognoseAnteil = biomasseProEinheit * anteilFaktor;

    }

    private void HandleTick()
    {
        //unsere veränderbaren Werte
        emission += anteilFaktor * emissionenProEinheit;
        müll += anteilFaktor * müllProEinheit;
        biomasse += anteilFaktor * biomasseProEinheit;
        
        //Prognose Werte
        emissionPrognose += emissionsPrognoseAnteil;
        müllPrognose += müllPrognoseAnteil;
        biomassePrognose += biomassePrognoseAnteil;

        Debug.Log("Müll Import = " + müll);
    }

    void OnSliderValueChanged(float value)
    {
        anteilFaktor = (100f - value) / 100;
        importText.color = Color.magenta;
        StartCoroutine(ChangeColorOverTime(Color.white, 1.2f));
    }

    IEnumerator ChangeColorOverTime(Color targetColor, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            importText.color = Color.Lerp(Color.magenta, Color.white, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        importText.color = targetColor;
    }
}
