using TMPro;
using UnityEngine;

public class VisuellerAbfallController : MonoBehaviour
{
    public GameObject abfallBetragsControllerObject;

    [SerializeField]
    public float unsereEmission, unserMüll, unsereBiomasse;
    [SerializeField]
    private float emissionPrognoseWert, abfallPrognoseWert, biomassePrognoseWert;

    public GameObject unsereEmissionenCube, unserAbfallCube, unserBiomasseCube, emmisionenPrognoseCube, abfallPrognoseCube, biomassenPrognoseCube;

    public TextMeshProUGUI emissionText, abfallText, biomasseText, emissionPrognoseText, abfallPrognoseText, biomassePrognoseText;

    void Start()
    {
        Timer.Instance.OnSecondElapsed += HandleTick;
    }

    private void HandleTick()
    {
        var abfallWerte = abfallBetragsControllerObject.GetComponent<AbfallBetragsController>();

        //unsere veränderbaren Werte
        unsereEmission += abfallWerte.emissionen;
        unserMüll += abfallWerte.abfall;
        unsereBiomasse += abfallWerte.biomüll;

        unsereEmissionenCube.transform.localScale = new Vector3(1.0f, unsereEmission / 10000.0f, 1.0f);
        unsereEmissionenCube.transform.localPosition = new Vector3(0.0f, 0.0f, unsereEmission / 20000.0f);
        unserAbfallCube.transform.localScale = new Vector3(1.0f, unserMüll / 10000.0f, 1.0f);
        unserAbfallCube.transform.localPosition = new Vector3(0.0f, 0.0f, unserMüll / 20000.0f);
        unserBiomasseCube.transform.localScale = new Vector3(1.0f, unsereBiomasse / 1000.0f, 1.0f);
        unserBiomasseCube.transform.localPosition = new Vector3(0.0f, 0.0f, unsereBiomasse / 2000.0f);

        emissionText.text = "Unsere Emissionen:\n" + unsereEmission.ToString() + "t";
        abfallText.text = "Unser Müll:\n" + unserMüll.ToString() + "t";
        biomasseText.text = "Unsere Biomasse:\n" + unsereBiomasse.ToString() + "t";

        //prognose Werte zum Vergleich
        emissionPrognoseWert += abfallWerte.prognoseEmissionen;
        abfallPrognoseWert += abfallWerte.prognoseAbfall;
        biomassePrognoseWert += abfallWerte.prognoseBiomüll;

        emmisionenPrognoseCube.transform.localScale = new Vector3(1.0f, emissionPrognoseWert / 10000.0f, 1.0f);
        emmisionenPrognoseCube.transform.localPosition = new Vector3(0.0f, 0.0f, emissionPrognoseWert / 20000.0f);
        abfallPrognoseCube.transform.localScale = new Vector3(1.0f, abfallPrognoseWert / 10000.0f, 1.0f);
        abfallPrognoseCube.transform.localPosition = new Vector3(0.0f, 0.0f, abfallPrognoseWert / 20000.0f);
        biomassenPrognoseCube.transform.localScale = new Vector3(1.0f, biomassePrognoseWert / 1000.0f, 1.0f);
        biomassenPrognoseCube.transform.localPosition = new Vector3(0.0f, 0.0f, biomassePrognoseWert / 2000.0f);

        emissionPrognoseText.text = "Prognose Emissionen:\n" + emissionPrognoseWert.ToString() + "t";
        abfallPrognoseText.text = "Prognose Müll:\n" + abfallPrognoseWert.ToString() + "t";
        biomassePrognoseText.text = "Prognose Biomasse:\n" + biomassePrognoseWert.ToString() + "t";
    }
}
