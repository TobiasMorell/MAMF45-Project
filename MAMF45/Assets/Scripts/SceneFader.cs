using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour {

	public Material ShaderMaterial;
    public string TargetScene;

    private bool fadeOut;
    private float timer;
    private float maxTime;

    void Start() {
        timer = 1;
        maxTime = 1;
    }

    public void FadeOut(float time) {
        fadeOut = true;
        maxTime = time;
    }

    void Update() {
        if (fadeOut) {
            if (timer >= maxTime) {
                SceneManager.LoadScene(TargetScene);
            }
            timer = Mathf.Min(timer + Time.deltaTime / Time.timeScale, maxTime);
        } else {
            timer = Mathf.Max(timer - Time.deltaTime / Time.timeScale, 0f);
        }
        ShaderMaterial.SetFloat("_Fade", timer / maxTime);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
        Graphics.Blit(source, destination, ShaderMaterial);
    }
}
