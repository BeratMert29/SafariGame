using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishButtonAnim : MonoBehaviour
{
    public Button button;

    public void removeFirstDown()
    {
        button.animator.SetBool("FirstDown", false);
        SwapButtons();
    }

    public void removeSecondUp()
    {
        button.animator2.SetBool("secondUp", false);
    }

    public void removeFirstUp()
    {
        button.animator.SetBool("firstUp", false);
    }

    public void removeSecondDown()
    {
        button.animator2.SetBool("secondDown", false);
    }

    private void SwapButtons()
    {
        Vector3 tempPosition = button.firstButtonImage.transform.position;
        button.firstButtonImage.transform.position = button.secondButtonImage.transform.position;
        button.firstButtonImage.transform.position = tempPosition;
        button.animator.enabled = false;
        button.animator2.enabled = false;
    }

}
