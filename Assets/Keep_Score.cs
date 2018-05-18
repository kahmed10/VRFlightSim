using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keep_Score : MonoBehaviour {

    public int score;
    private GameObject mainCanvas, text;
    private Text t;
    private RectTransform r;
    private bool firstScore = true;

	// Use this for initialization
	void Start () {
        score = 0;
        mainCanvas = GameObject.Find("MainCanvas");
        text = mainCanvas.transform.Find("IntroText").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		if (score > 0)
        {
            if (firstScore)
            {
                t = text.GetComponent<Text>();
                r = text.GetComponent<RectTransform>();
                r.position += new Vector3(0.0f, 1.0f, 0.0f);
                firstScore = false;
            }
            t.text = "Score: " + score.ToString();

        }
	}
}
