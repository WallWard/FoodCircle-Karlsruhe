using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LokalerAnbauController : MonoBehaviour
{
    public Slider slider;
    public float anteilFaktor;

    public float emissionenProEinheit = 10f;
    public float biomasseProEinheit = 90f;
    public float müllProEinheit = 0.5f;
    public float emission, müll, biomasse;
    public float emissionPrognose, müllPrognose, biomassePrognose;

    private float emissionsPrognoseAnteil, biomassePrognoseAnteil, müllPrognoseAnteil;

    public TextMeshProUGUI lokalerAnbauText;

    void Start()
    {
        anteilFaktor = slider.value / 100;
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

        Debug.Log("Müll Lokaler Anbau = " + müll);
    }

    void OnSliderValueChanged(float value)
    {
        anteilFaktor = value / 100;

        lokalerAnbauText.color = Color.magenta;
        StartCoroutine(ChangeColorOverTime(Color.white, 1.2f));
    }

    IEnumerator ChangeColorOverTime(Color targetColor, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            lokalerAnbauText.color = Color.Lerp(Color.magenta, Color.white, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        lokalerAnbauText.color = targetColor;
    }

}
