using TMPro;
using UnityEngine;

public class AbfallController : MonoBehaviour
{
    public GameObject import, lokal, logistik, konsum;

    [SerializeField]
    public float emission, müll, biomasse;

    [SerializeField]
    private float emissionPrognoseWert, müllPrognoseWert, biomassePrognoseWert;

    public GameObject unsereEmissionen, unserMüll, unsereBiomasse, emmisionenPrognose, müllPrognose, biomassenPrognose;

    public TextMeshProUGUI emissionText, müllText, biomasseText, emissionPrognoseText, müllPrognoseText, biomassePrognoseText;

    // Start is called before the first frame update
    void Start()
    {
        Timer.Instance.OnSecondElapsed += HandleTick;
    }

    private void HandleTick()
    {
        var imp = import.GetComponent<ImportController>();
        var lok = lokal.GetComponent<LokalerAnbauController>();
        var log = logistik.GetComponent<LogistikController>();
        var kon = konsum.GetComponent<KonsumController>();

        //unsere veränderbaren Werte
        emission = imp.emission + lok.emission + log.emission + kon.emission;
        müll = imp.müll + lok.müll + log.müll + kon.müll;
        biomasse = imp.biomasse + lok.biomasse + log.biomasse + kon.biomasse;

        unsereEmissionen.transform.localScale = new Vector3(1.0f, emission / 1000.0f, 1.0f);
        unsereEmissionen.transform.localPosition = new Vector3(0.0f, 0.0f, emission / 2000.0f);
        unserMüll.transform.localScale = new Vector3(1.0f, müll / 1000.0f, 1.0f);
        unserMüll.transform.localPosition = new Vector3(0.0f, 0.0f, müll / 2000.0f);
        unsereBiomasse.transform.localScale = new Vector3(1.0f, biomasse / 1000.0f, 1.0f);
        unsereBiomasse.transform.localPosition = new Vector3(0.0f, 0.0f, biomasse / 2000.0f);

        emissionText.text = "Unsere Emissionen:\n" + emission.ToString() + "kg";
        müllText.text = "Unser Müll:\n" + müll.ToString() + "kg";
        biomasseText.text = "Unsere Biomasse:\n" + biomasse.ToString() + "kg";

        //prognose Werte zum Vergleich
        emissionPrognoseWert = imp.emissionPrognose + lok.emissionPrognose + log.emissionPrognose + kon.emissionPrognose;
        müllPrognoseWert = imp.müllPrognose + lok.müllPrognose + log.müllPrognose + kon.müllPrognose;
        biomassePrognoseWert = imp.biomassePrognose + lok.biomassePrognose + log.biomassePrognose + kon.biomassePrognose;

        emmisionenPrognose.transform.localScale = new Vector3(1.0f, emissionPrognoseWert / 1000.0f, 1.0f);
        emmisionenPrognose.transform.localPosition = new Vector3(0.0f, 0.0f, emissionPrognoseWert / 2000.0f);
        müllPrognose.transform.localScale = new Vector3(1.0f, müllPrognoseWert / 1000.0f, 1.0f);
        müllPrognose.transform.localPosition = new Vector3(0.0f, 0.0f, müllPrognoseWert / 2000.0f);
        biomassenPrognose.transform.localScale = new Vector3(1.0f, biomassePrognoseWert / 1000.0f, 1.0f);
        biomassenPrognose.transform.localPosition = new Vector3(0.0f, 0.0f, biomassePrognoseWert / 2000.0f);

        emissionPrognoseText.text = "Prognose Emissionen:\n" + emissionPrognoseWert.ToString() + "kg";
        müllPrognoseText.text = "Prognose Müll:\n" + müllPrognoseWert.ToString() + "kg";
        biomassePrognoseText.text = "Prognose Biomasse:\n" + biomassePrognoseWert.ToString() + "kg";
    }
}
