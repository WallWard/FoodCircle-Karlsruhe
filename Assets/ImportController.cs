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
    public float m�llProEinheit = 120f;
    public float emission, m�ll, biomasse;
    public float emissionPrognose, m�llPrognose, biomassePrognose;

    private float emissionsPrognoseAnteil, biomassePrognoseAnteil, m�llPrognoseAnteil;


    public TextMeshProUGUI importText;

    void Start()
    {
        anteilFaktor = (100.0f - slider.value) / 100;
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

        Debug.Log("M�ll Import = " + m�ll);
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
