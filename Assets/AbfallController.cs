using TMPro;
using UnityEngine;

public class AbfallController : MonoBehaviour
{
    public GameObject import, lokal, logistik, konsum;

    [SerializeField]
    public float emission, m�ll, biomasse;

    [SerializeField]
    private float emissionPrognoseWert, m�llPrognoseWert, biomassePrognoseWert;

    public GameObject unsereEmissionen, unserM�ll, unsereBiomasse, emmisionenPrognose, m�llPrognose, biomassenPrognose;

    public TextMeshProUGUI emissionText, m�llText, biomasseText, emissionPrognoseText, m�llPrognoseText, biomassePrognoseText;

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

        //unsere ver�nderbaren Werte
        emission = imp.emission + lok.emission + log.emission + kon.emission;
        m�ll = imp.m�ll + lok.m�ll + log.m�ll + kon.m�ll;
        biomasse = imp.biomasse + lok.biomasse + log.biomasse + kon.biomasse;

        unsereEmissionen.transform.localScale = new Vector3(1.0f, emission / 1000.0f, 1.0f);
        unsereEmissionen.transform.localPosition = new Vector3(0.0f, 0.0f, emission / 2000.0f);
        unserM�ll.transform.localScale = new Vector3(1.0f, m�ll / 1000.0f, 1.0f);
        unserM�ll.transform.localPosition = new Vector3(0.0f, 0.0f, m�ll / 2000.0f);
        unsereBiomasse.transform.localScale = new Vector3(1.0f, biomasse / 1000.0f, 1.0f);
        unsereBiomasse.transform.localPosition = new Vector3(0.0f, 0.0f, biomasse / 2000.0f);

        emissionText.text = "Unsere Emissionen:\n" + emission.ToString() + "kg";
        m�llText.text = "Unser M�ll:\n" + m�ll.ToString() + "kg";
        biomasseText.text = "Unsere Biomasse:\n" + biomasse.ToString() + "kg";

        //prognose Werte zum Vergleich
        emissionPrognoseWert = imp.emissionPrognose + lok.emissionPrognose + log.emissionPrognose + kon.emissionPrognose;
        m�llPrognoseWert = imp.m�llPrognose + lok.m�llPrognose + log.m�llPrognose + kon.m�llPrognose;
        biomassePrognoseWert = imp.biomassePrognose + lok.biomassePrognose + log.biomassePrognose + kon.biomassePrognose;

        emmisionenPrognose.transform.localScale = new Vector3(1.0f, emissionPrognoseWert / 1000.0f, 1.0f);
        emmisionenPrognose.transform.localPosition = new Vector3(0.0f, 0.0f, emissionPrognoseWert / 2000.0f);
        m�llPrognose.transform.localScale = new Vector3(1.0f, m�llPrognoseWert / 1000.0f, 1.0f);
        m�llPrognose.transform.localPosition = new Vector3(0.0f, 0.0f, m�llPrognoseWert / 2000.0f);
        biomassenPrognose.transform.localScale = new Vector3(1.0f, biomassePrognoseWert / 1000.0f, 1.0f);
        biomassenPrognose.transform.localPosition = new Vector3(0.0f, 0.0f, biomassePrognoseWert / 2000.0f);

        emissionPrognoseText.text = "Prognose Emissionen:\n" + emissionPrognoseWert.ToString() + "kg";
        m�llPrognoseText.text = "Prognose M�ll:\n" + m�llPrognoseWert.ToString() + "kg";
        biomassePrognoseText.text = "Prognose Biomasse:\n" + biomassePrognoseWert.ToString() + "kg";
    }
}
