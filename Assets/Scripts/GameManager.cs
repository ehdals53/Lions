using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject MenuSet;
    public GameObject Keyboard;
    public GameObject Help;
    public GameObject Portal_UI;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Cursor.visible = false;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            if (MenuSet.activeSelf)
            {
                MenuSet.SetActive(false);
            }
            else
            {
                MenuSet.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (Help.activeSelf)
            {
                Help.SetActive(false);
            }
            else
            {
                Help.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (Keyboard.activeSelf)
            {
                Keyboard.SetActive(false);
            }
            else
            {
                Keyboard.SetActive(true);
            }
        }
    }

    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GameExit()
    {
        Application.Quit();
    }
    public void NextStageBtn()
    {
        SceneManager.LoadScene("SceneLoader_1");
    }
    public void CancelBtn()
    {
        Portal_UI.SetActive(false);
    }
}
