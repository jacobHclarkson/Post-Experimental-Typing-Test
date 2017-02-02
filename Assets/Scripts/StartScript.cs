using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartScript : MonoBehaviour {

    Text startText;
    bool started = false;
    float timeRemaining = 5.0f;

    bool go = false;

    public GameObject prompt;
    public GameObject timer;
    public GameObject field;

	// Use this for initialization
	void Start () {
        startText = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Space))
        {
            started = true;
        }
        if(started)
        {
            UpdateTime();
        }

        if (go)
        {
            // activate all the other objects
            prompt.SetActive(true);
            timer.SetActive(true);
            field.SetActive(true);
            // deactivate this object
            this.gameObject.SetActive(false);
        }
	}

    // start timer
    void UpdateTime()
    {
            timeRemaining -= Time.deltaTime;
            startText.text = Mathf.Round(timeRemaining).ToString();
            if(timeRemaining <= 0)
            {
                go = true;
            }
    }
}