using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WawePanelScript : MonoBehaviour
{
    public TextMeshProUGUI waweText;

    public void SetWaweText(string text)
    {
        waweText.text = "WAVE "+text;
    }
}
