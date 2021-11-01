using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageActive : MonoBehaviour
{
    public TextMeshProUGUI portalText;

    private void OnEnable()
    {
        portalText.text = "Press Enter to go next stage";
    }
}
