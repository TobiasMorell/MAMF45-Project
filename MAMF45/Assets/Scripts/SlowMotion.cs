using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour {

	public Material ShaderMaterial;

	private Camera realCamera;
	private Camera lightCamera;
	private LayerMask highlightedLayers;

	private List<GameObject> slowMotionObjects;
	private float timer;
	
	void Start ()
	{
		slowMotionObjects = new List<GameObject> ();
		highlightedLayers = LayerMask.NameToLayer ("Highlighted");
		CreateLightCamera ();
	}

	private void CreateLightCamera()
	{
		realCamera = GetComponent<Camera>();
		realCamera.depthTextureMode = DepthTextureMode.Depth;

		GameObject cameraObject = new GameObject("camera_highlight_object");
		lightCamera = cameraObject.AddComponent<Camera>();
		lightCamera.CopyFrom(realCamera);
		lightCamera.enabled = false;
		lightCamera.clearFlags = CameraClearFlags.Nothing;
		//lightCamera.rect = new Rect(0, 0, 1, 1);
		lightCamera.backgroundColor = new Color(0, 0, 0, 1);
		lightCamera.cullingMask = 1 << LayerMask.NameToLayer("Highlighted");
		lightCamera.depthTextureMode = DepthTextureMode.None;
	}

	void Update ()
	{
		if (slowMotionObjects.Count > 0) {
			timer = Mathf.Min (timer + Time.deltaTime / Time.timeScale, 1f);
		} else {
			timer = Mathf.Max (timer - 2*Time.deltaTime / Time.timeScale, 0f);
		}

		Time.timeScale = Mathf.Lerp (1f, 0.2f, timer);
		ShaderMaterial.SetFloat ("_Saturation", Mathf.Lerp(1f, 0.2f, timer));
	}

	public void StartSlowMotion(GameObject obj)
	{
		slowMotionObjects.Add (obj);
		obj.SetLayerRecursively (highlightedLayers);
	}

	public void StopSlowMotion(GameObject obj)
	{
		slowMotionObjects.Remove (obj);
		obj.SetLayerRecursively (LayerMask.NameToLayer("Default"));
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, ShaderMaterial);

		lightCamera.transform.position = realCamera.transform.position;
		lightCamera.transform.rotation = realCamera.transform.rotation;
		lightCamera.transform.localScale = realCamera.transform.localScale;

		lightCamera.targetTexture = destination;
		lightCamera.Render ();
	}
}
