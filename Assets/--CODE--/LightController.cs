using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    [SerializeField] private GameObject[] lights;
    public Player player;
    [SerializeField] private Light2D globalLight1, globalLight2;

    void Update()
    {
        UpdateLights();
    }

    private void UpdateLights()
    {
        bool isBatActive = false;

        foreach (GameObject gameObject in player.createdModeList)
        {
            if (gameObject.activeSelf && gameObject.name == "Bat(Clone)")
            {
                isBatActive = true;
                globalLight1.intensity = 0.8f;
                globalLight2.intensity = 0.8f;// Set the intensity directly to 1.8
                break;
            }
            else
            {
                globalLight1.intensity = 0.003f;
                globalLight2.intensity = 0.003f;
            }
        }

        foreach (GameObject light in lights)
        {
            light.SetActive(isBatActive);
        }
    }
}
