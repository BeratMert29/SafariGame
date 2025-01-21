using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterModeText : MonoBehaviour
{
    [SerializeField] Text myText;
    public Player player;

    void Start()
    {
        myText.text = "Human";
    }

    void Update()
    {
        changeText();
    }

    public void changeText()
    {
        foreach (GameObject gameObject in player.createdModeList)
        {
            if (gameObject.activeSelf)
            {
                string gameObjectNameWithoutClone = gameObject.name.Replace("(Clone)", "").Trim();
                myText.text = gameObjectNameWithoutClone;
            }

        }

    }
}
