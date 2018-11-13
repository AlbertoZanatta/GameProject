using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectText : MonoBehaviour {

    //Fade time in seconds
    public float fadeOutTime;
    public float upSpeed;
    public Text collectText;

    public void ShowText()
    {
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        Color originalColor = collectText.color;
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            collectText.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeOutTime));
            transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }
}
