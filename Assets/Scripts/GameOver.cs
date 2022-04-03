using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
    bool gameover;
    float endTime;
    [SerializeField] float fadeTime;
    [SerializeField] Image overlay;
    [SerializeField] TextMeshProUGUI tmp;
    float fade;
    Color clearWhite = new Color(1, 1, 1, 0);

    public void EndGame()
    {
        Time.timeScale = 0;
        gameover = true;
        endTime = Time.time;
        StartCoroutine("EndAnimation");
    }

    private void LateUpdate()
    {
        if (gameover)
        {
            fade += Time.unscaledDeltaTime / fadeTime;
            overlay.color = Color.Lerp(clearWhite, Color.white, fade);
        }
    }

    IEnumerator EndAnimation()
    {
        // for any onlookers, please do not do dialogue this way
        // this is the only place in the game with text so i didn't want to bother making a whole system
        // this is me at my worst

        yield return new WaitForSecondsRealtime(fadeTime+3);
        tmp.text = "Ah";
        yield return new WaitForSecondsRealtime(1f);
        tmp.text = "Ah.";
        yield return new WaitForSecondsRealtime(1f);
        tmp.text = "Ah..";
        yield return new WaitForSecondsRealtime(1f);
        tmp.text = "Ah...";
        yield return new WaitForSecondsRealtime(1f);
        tmp.text = "Ah..";
        yield return new WaitForSecondsRealtime(0.08f);
        tmp.text = "Ah.";
        yield return new WaitForSecondsRealtime(0.08f);
        tmp.text = "Ah";
        yield return new WaitForSecondsRealtime(0.08f);
        tmp.text = "A";
        yield return new WaitForSecondsRealtime(0.08f);
        tmp.text = "";

        yield return new WaitForSecondsRealtime(2f);
        tmp.text = "I guess this is how it ends";
        yield return new WaitForSecondsRealtime(4f);
        tmp.text = "";

        yield return new WaitForSecondsRealtime(4f);
        tmp.text = "Created by Roboolet";
        yield return new WaitForSecondsRealtime(2f);
        tmp.text = "Created by Roboolet\nThanks for playing!";
    }
}
