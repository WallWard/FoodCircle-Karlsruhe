using System;
using TMPro;
using UnityEngine;

public class VisuellerAbfallController : MonoBehaviour
{
    public GameObject abfallBetragsControllerObject;

    [SerializeField]
    public float unsereEmission, unserAbfall, unsereBiomasse;
    [SerializeField]
    private float emissionPrognoseWert, abfallPrognoseWert, biomassePrognoseWert;

    public GameObject unsereEmissionenCube, unserAbfallCube, unserBiomasseCube, emmisionenPrognoseCube, abfallPrognoseCube, biomassenPrognoseCube;

    public TextMeshProUGUI emissionText, abfallText, biomasseText, emissionPrognoseText, abfallPrognoseText, biomassePrognoseText;

    public TextMeshProUGUI differenzEmissionen, differenzAbfall, differenzBiomüll;

    void Start()
    {
        Timer.Instance.OnSecondElapsed += HandleTick;
    }

    private void HandleTick()
    {
        var müllWerte = abfallBetragsControllerObject.GetComponent<AbfallBetragsController>();

        //unsere veränderbaren Werte
        unsereEmission += müllWerte.emissionen;
        unserAbfall += müllWerte.abfall;
        unsereBiomasse += müllWerte.biomüll;
                                                                                         
        unsereEmissionenCube.transform.localScale = new Vector3(1.0f, unsereEmission / 20000.0f, 1.0f);
        unsereEmissionenCube.transform.localPosition = new Vector3(0.0f, 0.0f, unsereEmission / 40000.0f);
        unserAbfallCube.transform.localScale = new Vector3(1.0f, unserAbfall / 10000.0f, 1.0f);
        unserAbfallCube.transform.localPosition = new Vector3(0.0f, 0.0f, unserAbfall / 20000.0f);
        unserBiomasseCube.transform.localScale = new Vector3(1.0f, unsereBiomasse / 1000.0f, 1.0f);
        unserBiomasseCube.transform.localPosition = new Vector3(0.0f, 0.0f, unsereBiomasse / 2000.0f);

        emissionText.text = "Unsere Emissionen:\n" + unsereEmission.ToString() + "t";
        abfallText.text = "Unser Müll:\n" + unserAbfall.ToString() + "t";
        biomasseText.text = "Unsere Biomasse:\n" + unsereBiomasse.ToString() + "t";

        //prognose Werte zum Vergleich
        emissionPrognoseWert += müllWerte.prognoseEmissionen;
        abfallPrognoseWert += müllWerte.prognoseAbfall;
        biomassePrognoseWert += müllWerte.prognoseBiomüll;

        emmisionenPrognoseCube.transform.localScale = new Vector3(1.0f, emissionPrognoseWert / 20000.0f, 1.0f);
        emmisionenPrognoseCube.transform.localPosition = new Vector3(0.0f, 0.0f, emissionPrognoseWert / 40000.0f);
        abfallPrognoseCube.transform.localScale = new Vector3(1.0f, abfallPrognoseWert / 10000.0f, 1.0f);
        abfallPrognoseCube.transform.localPosition = new Vector3(0.0f, 0.0f, abfallPrognoseWert / 20000.0f);
        biomassenPrognoseCube.transform.localScale = new Vector3(1.0f, biomassePrognoseWert / 1000.0f, 1.0f);
        biomassenPrognoseCube.transform.localPosition = new Vector3(0.0f, 0.0f, biomassePrognoseWert / 2000.0f);

        emissionPrognoseText.text = "Prognose Emissionen:\n" + emissionPrognoseWert.ToString() + "t pro Tag";
        abfallPrognoseText.text = "Prognose Müll:\n" + abfallPrognoseWert.ToString() + "t pro Tag";
        biomassePrognoseText.text = "Prognose Biomasse:\n" + biomassePrognoseWert.ToString() + "t pro Tag";

        float differenzEmissionenWert = müllWerte.prognoseEmissionen - müllWerte.emissionen;
        if(differenzEmissionenWert > 0) { differenzEmissionen.color = Color.green;} else { differenzEmissionen.color = Color.red; }
        differenzEmissionen.text = Math.Abs(differenzEmissionenWert).ToString() + "t";

        float differenzAbfallWert = müllWerte.prognoseAbfall - müllWerte.abfall;
        if (differenzAbfallWert > 0) { differenzAbfall.color = Color.green; } else { differenzAbfall.color = Color.red; }
        differenzAbfall.text = Math.Abs(differenzAbfallWert).ToString() + "t";

        float differenzBiomüllWert = müllWerte.prognoseBiomüll - müllWerte.biomüll;
        if (differenzBiomüllWert > 0) { differenzBiomüll.color = Color.green; } else { differenzBiomüll.color = Color.red; }
        differenzBiomüll.text = Math.Abs(differenzBiomüllWert).ToString() + "t";
    }
}
