using TMPro;
using UnityEngine;

public class VisuellerAbfallController : MonoBehaviour
{
    public GameObject abfallBetragsControllerObject;

    [SerializeField]
    public float unsereEmission, unserM�ll, unsereBiomasse;
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

        //unsere ver�nderbaren Werte
        unsereEmission += abfallWerte.emissionen;
        unserM�ll += abfallWerte.abfall;
        unsereBiomasse += abfallWerte.biom�ll;

        unsereEmissionenCube.transform.localScale = new Vector3(1.0f, unsereEmission / 10000.0f, 1.0f);
        unsereEmissionenCube.transform.localPosition = new Vector3(0.0f, 0.0f, unsereEmission / 20000.0f);
        unserAbfallCube.transform.localScale = new Vector3(1.0f, unserM�ll / 10000.0f, 1.0f);
        unserAbfallCube.transform.localPosition = new Vector3(0.0f, 0.0f, unserM�ll / 20000.0f);
        unserBiomasseCube.transform.localScale = new Vector3(1.0f, unsereBiomasse / 1000.0f, 1.0f);
        unserBiomasseCube.transform.localPosition = new Vector3(0.0f, 0.0f, unsereBiomasse / 2000.0f);

        emissionText.text = "Unsere Emissionen:\n" + unsereEmission.ToString() + "t";
        abfallText.text = "Unser M�ll:\n" + unserM�ll.ToString() + "t";
        biomasseText.text = "Unsere Biomasse:\n" + unsereBiomasse.ToString() + "t";

        //prognose Werte zum Vergleich
        emissionPrognoseWert += abfallWerte.prognoseEmissionen;
        abfallPrognoseWert += abfallWerte.prognoseAbfall;
        biomassePrognoseWert += abfallWerte.prognoseBiom�ll;

        emmisionenPrognoseCube.transform.localScale = new Vector3(1.0f, emissionPrognoseWert / 10000.0f, 1.0f);
        emmisionenPrognoseCube.transform.localPosition = new Vector3(0.0f, 0.0f, emissionPrognoseWert / 20000.0f);
        abfallPrognoseCube.transform.localScale = new Vector3(1.0f, abfallPrognoseWert / 10000.0f, 1.0f);
        abfallPrognoseCube.transform.localPosition = new Vector3(0.0f, 0.0f, abfallPrognoseWert / 20000.0f);
        biomassenPrognoseCube.transform.localScale = new Vector3(1.0f, biomassePrognoseWert / 1000.0f, 1.0f);
        biomassenPrognoseCube.transform.localPosition = new Vector3(0.0f, 0.0f, biomassePrognoseWert / 2000.0f);

        emissionPrognoseText.text = "Prognose Emissionen:\n" + emissionPrognoseWert.ToString() + "t";
        abfallPrognoseText.text = "Prognose M�ll:\n" + abfallPrognoseWert.ToString() + "t";
        biomassePrognoseText.text = "Prognose Biomasse:\n" + biomassePrognoseWert.ToString() + "t";
    }
}
