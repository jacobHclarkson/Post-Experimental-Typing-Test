using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class Prompt : MonoBehaviour {

    Text prompt;
    string currentPrompt;

    string prescribedText = "";

    string[] wordlist;

    public TextAsset textAsset;

    public int wordsPerLine = 15;

	// Use this for initialization
	void Start () {
        prompt = gameObject.GetComponent<Text>();
        ReadWords();
        prompt.text = GenerateWords();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Return)){
            SavePrompt();
            currentPrompt = GenerateWords();
            prompt.text = currentPrompt;
        }
	}

    // Save prescribed text
    public void SavePrompt()
    {
        prescribedText += prompt.text;
    }

    // getter for recorded prescribed text
    public string GetPrescribedText()
    {
        return prescribedText;
    }

    // read words from file
    void ReadWords()
    {
        string wordsFromFile = textAsset.text;
        wordlist = wordsFromFile.Split(' ');
    }

    // generate the next line of words
    string GenerateWords()
    {
        string nextPrompt = "";
        for(int i=0; i<wordsPerLine; i++)
        {
            int idx = Random.Range(0, wordlist.Length);
            nextPrompt = nextPrompt + wordlist[idx] + " ";
        }
        return nextPrompt;
    }
}
