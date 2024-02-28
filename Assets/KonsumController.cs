using UnityEngine;
using UnityEngine.UI;

public class KonsumController : MonoBehaviour
{
    public Slider sliderFleisch;
    public Slider sliderHommeade;
    public float anteilFaktor;

    public float emissionenProEinheit = 10f;
    public float biomasseProEinheit = 45f;
    public float müllProEinheit = 120f;

    public float emission, müll, biomasse;
    public float emissionPrognose, müllPrognose, biomassePrognose;

    private float emissionsPrognoseAnteil, biomassePrognoseAnteil, müllPrognoseAnteil;

    void Start()
    {
        anteilFaktor = sliderFleisch.value / 100;
        // Füge einen Listener für die Wertänderungen des Sliders hinzu
        sliderFleisch.onValueChanged.AddListener(OnSliderValueChanged);

        Timer.Instance.OnSecondElapsed += HandleTick;
        //feste Referenz-Werte
        emissionsPrognoseAnteil = emissionenProEinheit * anteilFaktor;
        müllPrognoseAnteil = müllProEinheit * anteilFaktor;
        biomassePrognoseAnteil = biomasseProEinheit * anteilFaktor;

    }

    private void HandleTick()
    {   //unsere veränderbaren Werte
        emission += anteilFaktor * emissionenProEinheit;
        müll += anteilFaktor * müllProEinheit;
        biomasse += anteilFaktor * biomasseProEinheit;

        //Prognose Werte
        emissionPrognose += emissionsPrognoseAnteil;
        müllPrognose += müllPrognoseAnteil;
        biomassePrognose += biomassePrognoseAnteil;

        Debug.Log("Müll Konsumverhalten = " + müll);
    }

    void OnSliderValueChanged(float value)
    {
        anteilFaktor = value / 100;
    }

}
