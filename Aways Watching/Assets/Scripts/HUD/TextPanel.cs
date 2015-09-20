using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextPanel : MonoBehaviour {
    public GameObject infoPanel;
    public Text text;
    public float delay;
    public AudioClip sound;
    public Button btnOK;
    private string myText;
	// Use this for initialization
	void Awake () 
    {
        infoPanel = this.gameObject;
        btnOK = GetComponentInChildren<Button>();
        btnOK.enabled = false;
        text = GetComponentInChildren<Text>();
        infoPanel.SetActive(false);
	}

    IEnumerator Write()
    {
        foreach (char letter in myText.ToCharArray())
        {
            text.text += letter;
            //if (sound)
            //{
            //    audio.PlayOneShot(sound);
            //}
            yield return new WaitForSeconds(delay);
        }
        btnOK.enabled = true;
    }
    public void OK()
    {
        btnOK.enabled = false;
        text.text = null;
        infoPanel.SetActive(false);
    }

    public void WriteText(string phrase)
    {
        infoPanel.SetActive(true);
        myText = phrase;
        StartCoroutine(Write());
    }
}
