using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConfirmButton : MonoBehaviour {

    public InputField nameField;
    public static string playerName;

    public void Start()
    {
        nameField.text = "";
        nameField.Select();
        nameField.ActivateInputField();
    }

    public void Update()
    {
        if (Input.GetKeyDown("enter") || Input.GetKeyDown("return"))
        {
            ConfirmButton_Click();
        }
    }

	public void ConfirmButton_Click()
    {
        playerName = nameField.text;
        SceneManager.LoadScene("Main");
    }
}
