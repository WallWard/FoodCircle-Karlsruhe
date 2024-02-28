using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class FromAtoB : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public GameObject particlePrefab;
    public float speed = 7.5f; // Geschwindigkeitsparameter

    private GameObject particleInstance;

    private LineRenderer lineRenderer;
    public GameObject Sender;
    public float mengeBekommen, verhältnis, mengeZuSenden;
    public bool receiver;
    public bool endPoint = false;
    public TextMeshProUGUI numberText;

    void Start()
    {
        if (!endPoint)
        {
            lineRenderer = GetComponent<LineRenderer>();
            UpdateLinePositions();
        }
        Timer.Instance.OnSecondElapsed += HandleTick;

        if (receiver)
        {
            numberText.text = GetComponent<BetragManager>().Betrag.ToString();
        }
    }

    private void HandleTick()
    {
        //Debug.Log(gameObject.name+ ": " + GetComponent<BetragManager>().Betrag.ToString() + "\n");
        StartInterpolation();
        if (receiver)
        { 
            CalculateBetrag();        
        }
    }

    private void CalculateBetrag()
    {
        float betrag = GetComponent<BetragManager>().Betrag;
        
            mengeBekommen = Sender.GetComponent<FromAtoB>().mengeZuSenden;

            betrag += mengeBekommen - mengeZuSenden;

            GetComponent<BetragManager>().Betrag = betrag;
            numberText.text = betrag.ToString();          
    }

    public void verhältnisAendern(float neuerWert)
    {
        verhältnis = neuerWert;
    }

    void UpdateLinePositions()
    {
        lineRenderer.SetPosition(0, pointA.position);
        lineRenderer.SetPosition(1, pointB.position);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartInterpolation();
        }
    }

    void StartInterpolation()
    {
        if (!endPoint)
        {
            // Wenn bereits ein Partikel existiert, deaktiviere es, um es später wiederzuverwenden
            if (particleInstance != null)
            {
                particleInstance.SetActive(false);
            }

            // Instantiates the particle at pointA
            particleInstance = Instantiate(particlePrefab, pointA.position, Quaternion.identity);

            // Starts the interpolation coroutine
            StartCoroutine(InterpolateParticle());
        }
    }

    IEnumerator InterpolateParticle()
    {
        float elapsedTime = 0f;
        float journeyLength = Vector3.Distance(pointA.position, pointB.position);
        float duration = journeyLength / speed;

        while (elapsedTime < 1f)
        {
            // Calculates the current position based on interpolation with speed factor
            particleInstance.transform.position = Vector3.Lerp(pointA.position, pointB.position, elapsedTime);

            // Updates the elapsed time
            elapsedTime += Time.deltaTime / duration;

            yield return null;
        }

        // Deaktiviere das Partikelobjekt für den nächsten Durchlauf
        particleInstance.SetActive(false);
    }
}
