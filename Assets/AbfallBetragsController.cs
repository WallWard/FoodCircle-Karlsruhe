using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbfallBetragsController : MonoBehaviour
{
    //Unsere Stellschraube für den menschlichen Einfluss
    //AnbauSlider (oder auch ImportSlider) und der FleischSlider gehen von 0 bis 100
    //Der VerhaltenSlider basiert auf einem optimistischen Verbessungswert von Faktor 2
    public Slider anbauSlider, fleischSlider, verhaltensSlider;

    //Verhältnisanzeige der Slider
    public TextMeshProUGUI AnbauEmissionenVerhältnis, FleischAnteil, PositivesVerhalten;

    //Summe des täglichen Nahrungsmittelbedarfes
    public const float täglicherNahrungsmittelBedarf = 660564f;
    //StartWerte in kg
    public float täglBedarfPflanzen = 561479f;
    public float täglBedarfTierischeProdukte = 99084f;

    //CO2 Emissions Faktoren pro kg tierischer/pflanzlicher Nahrung
    private float pflanzlicherRegionalerEmissionsFaktor = 0.645f;
    private float pflanzlicherÜberseeEmissionsFaktor = 6.085f;
    private float tierischerRegionalerEmissionsFaktor = 7.015f;
    private float tierischerÜberseeEmissionsFaktor = 12.445f;

    //unsere dynamischen Werte, die durch unsere Hebel beeinflusst werden.
    public float pflanzenEmissionenProTag, tierischeEmissionenProTag, emissionenProTag, abfallProTag, biomasseProTag;
    //die Summe die sich dynamisch Ansammelt
    public float emissionen, abfall, biomüll;

    //unsere statischen Prognose Daten um einen Vergleich zu haben
    public float prognoseEmissionen, prognoseAbfall, prognoseBiomüll;

    //Abfall
    private float regionalerAbfallFaktor = 1.5f;
    private float überseeAbfallFaktor = 3f;

    //Biomüll
    private float regionalerBiomasseFaktor = 0.3f;
    private float überseeBiomasseFaktor = 0.04f;

    //direkter Verhalten Faktor der sich in diem Zustand pauschal auf die Summen unserer Werte auswirkt
    //Festegelegt auf eine Verdoppelung, oder jenachdem wie er angewendet wird eine Halbierung der Müll-Werte
    private float optimistischerVerhaltensFaktor = 2f;
    //40% der Emissionen kommen vom personen Einkauf
    private float privatEmissionenFaktor = 0.4f;
    //Abfall Faktor von 0,9 pro kg Nahrung
    private float privatAbfallFaktor = 0.9f;
    //Biomüll Faktor von 0,01 pro kg Nahrung
    private float privatBiomüllFaktor = 0.01f;
    //verhaltensabhängige Werte, die pauschal auf unseren Müll pro Tag summiert werden
    private float verhaltensEmissionen, verhaltensAbfall, verhaltensBiomüll;

    void Start()
    {   //Die folgenden Blöcke initiieren unsere Müll Werte
        täglBedarfTierischeProdukte = täglicherNahrungsmittelBedarf * (fleischSlider.value / 100);
        täglBedarfPflanzen = täglicherNahrungsmittelBedarf - täglBedarfTierischeProdukte;

        pflanzenEmissionenProTag = ((anbauSlider.value / 100) * täglBedarfPflanzen * pflanzlicherRegionalerEmissionsFaktor) + (((100 - anbauSlider.value) / 100) * täglBedarfPflanzen * pflanzlicherÜberseeEmissionsFaktor);
        tierischeEmissionenProTag = ((anbauSlider.value / 100) * täglBedarfTierischeProdukte * tierischerRegionalerEmissionsFaktor) + (((100 - anbauSlider.value) / 100) * täglBedarfTierischeProdukte * tierischerÜberseeEmissionsFaktor);

        emissionenProTag = pflanzenEmissionenProTag + tierischeEmissionenProTag;
        abfallProTag = (Mathf.Lerp(regionalerAbfallFaktor, überseeAbfallFaktor, anbauSlider.value)) * täglicherNahrungsmittelBedarf;
        biomasseProTag = (Mathf.Lerp(regionalerBiomasseFaktor, überseeBiomasseFaktor, anbauSlider.value)) * täglicherNahrungsmittelBedarf;

        verhaltensEmissionen = privatEmissionenFaktor * emissionenProTag;
        verhaltensAbfall = privatAbfallFaktor * täglicherNahrungsmittelBedarf;
        verhaltensBiomüll = privatBiomüllFaktor * täglicherNahrungsmittelBedarf;

        emissionen = prognoseEmissionen = (emissionenProTag + verhaltensEmissionen) / 1000;
        abfall = prognoseAbfall = (abfallProTag + verhaltensAbfall) / 1000;
        biomüll = prognoseBiomüll = (biomasseProTag + verhaltensBiomüll) / 1000;

        anbauSlider.onValueChanged.AddListener(OnAnbauSliderValueChanged);
        fleischSlider.onValueChanged.AddListener(OnFleischSliderValueChanged);
        verhaltensSlider.onValueChanged.AddListener(OnVerhaltenSliderValueChanged);

        FleischAnteil.text = fleischSlider.value.ToString("F2") + "%";
        AnbauEmissionenVerhältnis.text = anbauSlider.value.ToString("F2") + "%";
        PositivesVerhalten.text = verhaltensSlider.value.ToString("F2") + "%";

        Timer.Instance.OnSecondElapsed += HandleTick;
    }

    private void OnAnbauSliderValueChanged(float sliderValue)
    {
        pflanzenEmissionenProTag = ((sliderValue / 100) * täglBedarfPflanzen * pflanzlicherRegionalerEmissionsFaktor) + (((100 - sliderValue) / 100) * täglBedarfPflanzen * pflanzlicherÜberseeEmissionsFaktor);
        tierischeEmissionenProTag = ((sliderValue / 100) * täglBedarfTierischeProdukte * tierischerRegionalerEmissionsFaktor) + (((100 - sliderValue) / 100) * täglBedarfTierischeProdukte * tierischerÜberseeEmissionsFaktor);
        emissionenProTag = pflanzenEmissionenProTag + tierischeEmissionenProTag;

        //keine unterscheidung zwischen pflanzlich und tierisch für den Abfall und Biomasse
        abfallProTag = (Mathf.Lerp(regionalerAbfallFaktor, überseeAbfallFaktor, sliderValue)) * täglicherNahrungsmittelBedarf;
        biomasseProTag = (Mathf.Lerp(regionalerBiomasseFaktor, überseeBiomasseFaktor, sliderValue)) * täglicherNahrungsmittelBedarf;

        AnbauEmissionenVerhältnis.text = anbauSlider.value.ToString("F2") + "%";
    }

    //Beachtet für unseren Fall nur die Emissionen
    private void OnFleischSliderValueChanged(float sliderValue)
    {
        täglBedarfTierischeProdukte = täglicherNahrungsmittelBedarf * (sliderValue / 100);
        täglBedarfPflanzen = täglicherNahrungsmittelBedarf - täglBedarfTierischeProdukte;

        FleischAnteil.text = fleischSlider.value.ToString("F2") + "%";
    }

    private void OnVerhaltenSliderValueChanged(float sliderValue)
    {
        float privatVerhaltensEmissionsFaktor = privatEmissionenFaktor / Mathf.Lerp(0.0001f, optimistischerVerhaltensFaktor, sliderValue);
        verhaltensEmissionen = privatVerhaltensEmissionsFaktor * emissionenProTag;

        float privatVerhaltensAbfallFaktor = privatAbfallFaktor / Mathf.Lerp(0.0001f, optimistischerVerhaltensFaktor, sliderValue);
        verhaltensAbfall = privatVerhaltensAbfallFaktor * täglicherNahrungsmittelBedarf;

        float privatVerhaltensBiomüllFaktor = privatBiomüllFaktor / Mathf.Lerp(0.0001f, optimistischerVerhaltensFaktor, sliderValue);
        verhaltensBiomüll = privatVerhaltensBiomüllFaktor * täglicherNahrungsmittelBedarf;

        PositivesVerhalten.text = verhaltensSlider.value.ToString("F2") + "%";
    }

    private void HandleTick()
    {
        //ausrechnen der dynamischen Werte
        emissionen = (emissionenProTag + verhaltensEmissionen) / 1000;
        abfall = (abfallProTag + verhaltensAbfall) / 1000;
        biomüll = (biomasseProTag + verhaltensBiomüll) / 1000;
    }
}
