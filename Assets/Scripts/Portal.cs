using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Portal : MonoBehaviour
{
    public GameObject portalMessage;
    public TextMeshProUGUI portalText;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            portalText.text = "Press Enter to go to the next stage";
            portalMessage.gameObject.SetActive(true);
            if (Input.GetButtonDown("Enter")) 
            {
                SceneManager.LoadScene("2Stage");

            }

        }
    }
}
