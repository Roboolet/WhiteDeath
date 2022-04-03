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
    Color fade;

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

            fade = Color.Lerp(Color.clear, Color.white, (Time.unscaledTime - endTime) * fadeTime);
        }
    }

    IEnumerator EndAnimation()
    {
        yield return new WaitForSecondsRealtime(fadeTime);
    }
}
