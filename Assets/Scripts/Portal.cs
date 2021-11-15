using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Portal1 : MonoBehaviour
{
    public GameObject portalMessage;
    public TextMeshProUGUI portalText;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            portalText.text = "Space Ű�� ������ ���� ���������� �̵��մϴ� !";
            portalMessage.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                SceneManager.LoadScene("2Stage");
            }

        }
    }
}
