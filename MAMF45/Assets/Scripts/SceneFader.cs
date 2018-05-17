using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour {

	public Material ShaderMaterial;
    public string TargetScene;

    private bool fadeOut;
    private float timer;

    void Start() {
        timer = 1;
    }

    public void FadeOut() {
        fadeOut = true;
    }

    void Update() {
        if (fadeOut) {
            if (timer >= 1f) {
                SceneManager.LoadScene(TargetScene);
            }
            timer = Mathf.Min(timer + Time.deltaTime / Time.timeScale, 1f);
        } else {
            timer = Mathf.Max(timer - Time.deltaTime / Time.timeScale, 0f);
        }
        ShaderMaterial.SetFloat("_Fade", timer);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
        Graphics.Blit(source, destination, ShaderMaterial);
    }
}
