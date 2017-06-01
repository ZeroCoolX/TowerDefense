using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour {

    public Image image;
    public AnimationCurve curve;

    private void Start() {//fade in
        StartCoroutine(fadeIn());
    }

    public void fadeTo(string scene) {//fade out
        StartCoroutine(fadeOut(scene));
    }


    IEnumerator fadeIn() {//both in (-1) and out (+1)  and an optional scene
        float t = 1f;//animate from 1 -> 0
        while(t > 0f) {//fade in 1 + -1 = 0    fade out 1 + 1
            t -= Time.deltaTime;
            //use the animation curve with t being the parameter
            float alpha = curve.Evaluate(t);
            image.color = new Color(0f, 0f, 0f, alpha);
            yield return 0; //skip to the next frame
        }

    }

    IEnumerator fadeOut(string scene) {//both in (-1) and out (+1)  and an optional scene
        float t = 0f;//animate from 1 -> 0
        while (t < 1f) {//fade in 1 + -1 = 0    fade out 1 + 1
            t += Time.deltaTime;
            //use the animation curve with t being the parameter
            float alpha = curve.Evaluate(t);
            image.color = new Color(0f, 0f, 0f, alpha);
            yield return 0; //skip to the next frame
        }
        SceneManager.LoadScene(scene);
    }
}
