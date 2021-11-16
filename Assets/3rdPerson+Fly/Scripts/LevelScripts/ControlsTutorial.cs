using UnityEngine;
using UnityEngine.UI;
// This class is created for the example scene. There is no support for this script.
public class ControlsTutorial : MonoBehaviour
{
	private bool showMsg = false;

	private GUIStyle style;
	private Color textColor;

	public GameObject menuSet;

	//public GameObject UI_Panel;
	private GameObject KeyboardCommands;

	void Awake()
	{
		style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		style.fontSize = 36;
		style.wordWrap = true;
		textColor = Color.white;
		textColor.a = 0;
		//textArea = new Rect((Screen.width-w)/2, 0, w, h);

		//UI_Panel = this.transform.Find("ScreenHUD/Panel").gameObject;
		KeyboardCommands = this.transform.Find("ScreenHUD/Keyboard").gameObject;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
		{
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = false;
		}
		if (Input.GetButtonDown("Cancel"))
		{
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = true;

            if (menuSet.activeSelf)
            {
				menuSet.SetActive(false);
            }
			else
            {
				menuSet.SetActive(true);
            }			

		}
		KeyboardCommands.SetActive(Input.GetKey(KeyCode.F2));	
	}

	void OnGUI()
	{
		if (showMsg)
		{
			if (textColor.a <= 1)
				textColor.a += 0.5f * Time.deltaTime;
		}
		// no hint to show
		else
		{
			if (textColor.a > 0)
				textColor.a -= 0.5f * Time.deltaTime;
		}

		style.normal.textColor = textColor;

		//GUI.Label(textArea, message, style);
	}
	
}
