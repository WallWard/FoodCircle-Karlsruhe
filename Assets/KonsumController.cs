using UnityEngine;
using UnityEngine.UI;

public class KonsumController : MonoBehaviour
{
    public Slider sliderFleisch;
    public Slider sliderHommeade;
    public float anteilFaktor;

    public float emissionenProEinheit = 10f;
    public float biomasseProEinheit = 45f;
    public float m�llProEinheit = 120f;

    public float emission, m�ll, biomasse;
    public float emissionPrognose, m�llPrognose, biomassePrognose;

    private float emissionsPrognoseAnteil, biomassePrognoseAnteil, m�llPrognoseAnteil;

    void Start()
    {
        anteilFaktor = sliderFleisch.value / 100;
        // F�ge einen Listener f�r die Wert�nderungen des Sliders hinzu
        sliderFleisch.onValueChanged.AddListener(OnSliderValueChanged);

        Timer.Instance.OnSecondElapsed += HandleTick;
        //feste Referenz-Werte
        emissionsPrognoseAnteil = emissionenProEinheit * anteilFaktor;
        m�llPrognoseAnteil = m�llProEinheit * anteilFaktor;
        biomassePrognoseAnteil = biomasseProEinheit * anteilFaktor;

    }

    private void HandleTick()
    {   //unsere ver�nderbaren Werte
        emission += anteilFaktor * emissionenProEinheit;
        m�ll += anteilFaktor * m�llProEinheit;
        biomasse += anteilFaktor * biomasseProEinheit;

        //Prognose Werte
        emissionPrognose += emissionsPrognoseAnteil;
        m�llPrognose += m�llPrognoseAnteil;
        biomassePrognose += biomassePrognoseAnteil;

        Debug.Log("M�ll Konsumverhalten = " + m�ll);
    }

    void OnSliderValueChanged(float value)
    {
        anteilFaktor = value / 100;
    }

}
