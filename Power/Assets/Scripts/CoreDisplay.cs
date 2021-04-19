using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreDisplay : MonoBehaviour
{
    public Text coreText;
    private void Update()
    {
        coreText.text = GameManager.instance.driveNumber.ToString() + " / 3 CORES";
    }
}
