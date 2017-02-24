using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class Field : MonoBehaviour {

    InputField input;

    public Text prompt;
    Prompt promptController;

    public Text timerText;

    string transcribedText = "";
    string rawInput = "";

    public float timeRemaining = 60.0f;

    bool timeup = false;

    // for time to first kep press
    bool firstKeyPressed = false;
    float timeToFirstKeyPress = 0.0f;


	// Use this for initialization
	void Start () {
        promptController = prompt.GetComponent<Prompt>();
        input = GetComponent<InputField>();
        input.Select();
	}
	
	// Update is called once per frame
	void Update () {

        if (!firstKeyPressed)
        {
            if (rawInput != "")
            {
                firstKeyPressed = true;
            }
        }

	    if (Input.GetKeyDown(KeyCode.Return)){

            SaveText();
            rawInput += " ";

            input.text = "";
            input.ActivateInputField();
            input.Select();
        }


        CaptureRawInput();

        UpdateTime();
	}

    // capture raw input
    void CaptureRawInput()
    {
        foreach(char c in Input.inputString)
        {
            if(c == "\b"[0])
            {
                rawInput += '~';
            }
            rawInput += c;
        }
    }

    // capture full transcribed text
    void SaveText()
    {
        transcribedText += input.text + " ";
    }

    // timer
    void UpdateTime()
    {
        // time to first key press
        if (!firstKeyPressed)
        {
            timeToFirstKeyPress += Time.deltaTime;
        } 

        timeRemaining -= Time.deltaTime;
        if(!timeup && timeRemaining <=0)
        {
            timeup = true;
            Debug.Log("Exiting");
            SaveText();
            promptController.SavePrompt();
            WriteToFile();
            Application.Quit();
        }
        timerText.text = Mathf.Round(timeRemaining).ToString();
    }


    // write data to file
    void WriteToFile()
    {
        StreamWriter sw = new StreamWriter(Application.dataPath + "data.txt");
        sw.WriteLine(promptController.GetPrescribedText());
        sw.WriteLine(transcribedText);
        sw.WriteLine(rawInput);
        sw.WriteLine(timeToFirstKeyPress);
        sw.Close();
    }
}