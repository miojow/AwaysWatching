using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoPanel : MonoBehaviour{
    public Text text;
    public float delay;
    private string myText;
    private

	void Awake () 
    {
        text = GetComponentInChildren<Text>();
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

            yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(delay));
        }
    }

    public static IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }

    public void WriteText(string phrase)
    {
        myText = phrase;
        StartCoroutine(Write());
    }
    
    public static class CoroutineUtil
    {
        public static IEnumerator WaitForRealSeconds(float time)
        {
            float start = Time.realtimeSinceStartup;
            while (Time.realtimeSinceStartup < start + time)
            {
                yield return null;
            }
        }
    }
    public void ClearText()
    {
        text.text = null;
    }


}
