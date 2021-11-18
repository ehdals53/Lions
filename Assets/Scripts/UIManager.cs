using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject helpUI;

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            helpUI.SetActive(false);
        }
    }

    public void OnClickStartBtn()
    {
        SceneManager.LoadScene("SceneLoader");
    }
    public void HelpBtn()
    {
        helpUI.SetActive(true);
    }
    public void ExitBtn()
    {
        Application.Quit();
    }
}
