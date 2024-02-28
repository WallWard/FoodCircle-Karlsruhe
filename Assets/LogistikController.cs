using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogistikController : MonoBehaviour
{
    public Slider slider;
    public float anteilFaktor;

    public float emissionenProEinheit = 200f;
    public float biomasseProEinheit = 66f;
    public float müllProEinheit = 355f;
    public float emission, müll, biomasse;
    public float emissionPrognose, müllPrognose, biomassePrognose;

    private float emissionsPrognoseAnteil, biomassePrognoseAnteil, müllPrognoseAnteil;

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

        Debug.Log("Müll Logistik = " + müll);
    }

    void OnSliderValueChanged(float value)
    {
        anteilFaktor = value / 100;
    }

}
