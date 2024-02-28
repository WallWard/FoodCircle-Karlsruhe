using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbfallBetragsController : MonoBehaviour
{
    //Unsere Stellschraube f�r den menschlichen Einfluss
    //AnbauSlider (oder auch ImportSlider) und der FleischSlider gehen von 0 bis 100
    //Der VerhaltenSlider basiert auf einem optimistischen Verbessungswert von Faktor 2
    public Slider anbauSlider, fleischSlider, verhaltensSlider;

    //Verh�ltnisanzeige der Slider
    public TextMeshProUGUI AnbauEmissionenVerh�ltnis, FleischAnteil, PositivesVerhalten;

    //Summe des t�glichen Nahrungsmittelbedarfes
    public const float t�glicherNahrungsmittelBedarf = 660564f;
    //StartWerte in kg
    public float t�glBedarfPflanzen = 561479f;
    public float t�glBedarfTierischeProdukte = 99084f;

    //CO2 Emissions Faktoren pro kg tierischer/pflanzlicher Nahrung
    private float pflanzlicherRegionalerEmissionsFaktor = 0.645f;
    private float pflanzlicher�berseeEmissionsFaktor = 6.085f;
    private float tierischerRegionalerEmissionsFaktor = 7.015f;
    private float tierischer�berseeEmissionsFaktor = 12.445f;

    //unsere dynamischen Werte, die durch unsere Hebel beeinflusst werden.
    public float pflanzenEmissionenProTag, tierischeEmissionenProTag, emissionenProTag, abfallProTag, biomasseProTag;
    //die Summe die sich dynamisch Ansammelt
    public float emissionen, abfall, biom�ll;

    //unsere statischen Prognose Daten um einen Vergleich zu haben
    public float prognoseEmissionen, prognoseAbfall, prognoseBiom�ll;

    //Abfall
    private float regionalerAbfallFaktor = 1.5f;
    private float �berseeAbfallFaktor = 3f;

    //Biom�ll
    private float regionalerBiomasseFaktor = 0.3f;
    private float �berseeBiomasseFaktor = 0.04f;

    //direkter Verhalten Faktor der sich in diem Zustand pauschal auf die Summen unserer Werte auswirkt
    //Festegelegt auf eine Verdoppelung, oder jenachdem wie er angewendet wird eine Halbierung der M�ll-Werte
    private float optimistischerVerhaltensFaktor = 2f;
    //40% der Emissionen kommen vom personen Einkauf
    private float privatEmissionenFaktor = 0.4f;
    //Abfall Faktor von 0,9 pro kg Nahrung
    private float privatAbfallFaktor = 0.9f;
    //Biom�ll Faktor von 0,01 pro kg Nahrung
    private float privatBiom�llFaktor = 0.01f;
    //verhaltensabh�ngige Werte, die pauschal auf unseren M�ll pro Tag summiert werden
    private float verhaltensEmissionen, verhaltensAbfall, verhaltensBiom�ll;

    void Start()
    {   //Die folgenden Bl�cke initiieren unsere M�ll Werte
        t�glBedarfTierischeProdukte = t�glicherNahrungsmittelBedarf * (fleischSlider.value / 100);
        t�glBedarfPflanzen = t�glicherNahrungsmittelBedarf - t�glBedarfTierischeProdukte;

        pflanzenEmissionenProTag = ((anbauSlider.value / 100) * t�glBedarfPflanzen * pflanzlicherRegionalerEmissionsFaktor) + (((100 - anbauSlider.value) / 100) * t�glBedarfPflanzen * pflanzlicher�berseeEmissionsFaktor);
        tierischeEmissionenProTag = ((anbauSlider.value / 100) * t�glBedarfTierischeProdukte * tierischerRegionalerEmissionsFaktor) + (((100 - anbauSlider.value) / 100) * t�glBedarfTierischeProdukte * tierischer�berseeEmissionsFaktor);

        emissionenProTag = pflanzenEmissionenProTag + tierischeEmissionenProTag;
        abfallProTag = (Mathf.Lerp(regionalerAbfallFaktor, �berseeAbfallFaktor, anbauSlider.value)) * t�glicherNahrungsmittelBedarf;
        biomasseProTag = (Mathf.Lerp(regionalerBiomasseFaktor, �berseeBiomasseFaktor, anbauSlider.value)) * t�glicherNahrungsmittelBedarf;

        verhaltensEmissionen = privatEmissionenFaktor * emissionenProTag;
        verhaltensAbfall = privatAbfallFaktor * t�glicherNahrungsmittelBedarf;
        verhaltensBiom�ll = privatBiom�llFaktor * t�glicherNahrungsmittelBedarf;

        emissionen = prognoseEmissionen = (emissionenProTag + verhaltensEmissionen) / 1000;
        abfall = prognoseAbfall = (abfallProTag + verhaltensAbfall) / 1000;
        biom�ll = prognoseBiom�ll = (biomasseProTag + verhaltensBiom�ll) / 1000;

        anbauSlider.onValueChanged.AddListener(OnAnbauSliderValueChanged);
        fleischSlider.onValueChanged.AddListener(OnFleischSliderValueChanged);
        verhaltensSlider.onValueChanged.AddListener(OnVerhaltenSliderValueChanged);

        FleischAnteil.text = fleischSlider.value.ToString("F2") + "%";
        AnbauEmissionenVerh�ltnis.text = anbauSlider.value.ToString("F2") + "%";
        PositivesVerhalten.text = verhaltensSlider.value.ToString("F2") + "%";

        Timer.Instance.OnSecondElapsed += HandleTick;
    }

    private void OnAnbauSliderValueChanged(float sliderValue)
    {
        pflanzenEmissionenProTag = ((sliderValue / 100) * t�glBedarfPflanzen * pflanzlicherRegionalerEmissionsFaktor) + (((100 - sliderValue) / 100) * t�glBedarfPflanzen * pflanzlicher�berseeEmissionsFaktor);
        tierischeEmissionenProTag = ((sliderValue / 100) * t�glBedarfTierischeProdukte * tierischerRegionalerEmissionsFaktor) + (((100 - sliderValue) / 100) * t�glBedarfTierischeProdukte * tierischer�berseeEmissionsFaktor);
        emissionenProTag = pflanzenEmissionenProTag + tierischeEmissionenProTag;

        //keine unterscheidung zwischen pflanzlich und tierisch f�r den Abfall und Biomasse
        abfallProTag = (Mathf.Lerp(regionalerAbfallFaktor, �berseeAbfallFaktor, sliderValue)) * t�glicherNahrungsmittelBedarf;
        biomasseProTag = (Mathf.Lerp(regionalerBiomasseFaktor, �berseeBiomasseFaktor, sliderValue)) * t�glicherNahrungsmittelBedarf;

        AnbauEmissionenVerh�ltnis.text = anbauSlider.value.ToString("F2") + "%";
    }

    //Beachtet f�r unseren Fall nur die Emissionen
    private void OnFleischSliderValueChanged(float sliderValue)
    {
        t�glBedarfTierischeProdukte = t�glicherNahrungsmittelBedarf * (sliderValue / 100);
        t�glBedarfPflanzen = t�glicherNahrungsmittelBedarf - t�glBedarfTierischeProdukte;

        FleischAnteil.text = fleischSlider.value.ToString("F2") + "%";
    }

    private void OnVerhaltenSliderValueChanged(float sliderValue)
    {
        float privatVerhaltensEmissionsFaktor = privatEmissionenFaktor / Mathf.Lerp(0.0001f, optimistischerVerhaltensFaktor, sliderValue);
        verhaltensEmissionen = privatVerhaltensEmissionsFaktor * emissionenProTag;

        float privatVerhaltensAbfallFaktor = privatAbfallFaktor / Mathf.Lerp(0.0001f, optimistischerVerhaltensFaktor, sliderValue);
        verhaltensAbfall = privatVerhaltensAbfallFaktor * t�glicherNahrungsmittelBedarf;

        float privatVerhaltensBiom�llFaktor = privatBiom�llFaktor / Mathf.Lerp(0.0001f, optimistischerVerhaltensFaktor, sliderValue);
        verhaltensBiom�ll = privatVerhaltensBiom�llFaktor * t�glicherNahrungsmittelBedarf;

        PositivesVerhalten.text = verhaltensSlider.value.ToString("F2") + "%";
    }

    private void HandleTick()
    {
        //ausrechnen der dynamischen Werte
        emissionen = (emissionenProTag + verhaltensEmissionen) / 1000;
        abfall = (abfallProTag + verhaltensAbfall) / 1000;
        biom�ll = (biomasseProTag + verhaltensBiom�ll) / 1000;
    }
}
