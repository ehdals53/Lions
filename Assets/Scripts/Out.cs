using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Out : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ocean")
        {
            if(gameObject.tag == "Player")
            {
                SceneManager.LoadScene("Stage2");

            }
        }
    }
}
