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
    public float m�llProEinheit = 355f;
    public float emission, m�ll, biomasse;
    public float emissionPrognose, m�llPrognose, biomassePrognose;

    private float emissionsPrognoseAnteil, biomassePrognoseAnteil, m�llPrognoseAnteil;

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

        Debug.Log("M�ll Logistik = " + m�ll);
    }

    void OnSliderValueChanged(float value)
    {
        anteilFaktor = value / 100;
    }

}
