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
    public float m�llProEinheit = 0.5f;
    public float emission, m�ll, biomasse;
    public float emissionPrognose, m�llPrognose, biomassePrognose;

    private float emissionsPrognoseAnteil, biomassePrognoseAnteil, m�llPrognoseAnteil;

    public TextMeshProUGUI lokalerAnbauText;

    void Start()
    {
        anteilFaktor = slider.value / 100;
        // F�ge einen Listener f�r die Wert�nderungen des Sliders hinzu
        slider.onValueChanged.AddListener(OnSliderValueChanged);

        Timer.Instance.OnSecondElapsed += HandleTick;

        //feste Referenz-Werte
        emissionsPrognoseAnteil = emissionenProEinheit * anteilFaktor;
        m�llPrognoseAnteil = m�llProEinheit * anteilFaktor;
        biomassePrognoseAnteil = biomasseProEinheit * anteilFaktor;
    }

    private void HandleTick()
    {
        //unsere ver�nderbaren Werte
        emission += anteilFaktor * emissionenProEinheit;
        m�ll += anteilFaktor * m�llProEinheit;
        biomasse += anteilFaktor * biomasseProEinheit;

        //Prognose Werte
        emissionPrognose += emissionsPrognoseAnteil;
        m�llPrognose += m�llPrognoseAnteil;
        biomassePrognose += biomassePrognoseAnteil;

        Debug.Log("M�ll Lokaler Anbau = " + m�ll);
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
