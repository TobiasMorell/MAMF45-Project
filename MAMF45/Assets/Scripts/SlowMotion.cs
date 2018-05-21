using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour {

	public Material ShaderMaterial;

	private Camera realCamera;
	private Camera highlightCamera;
	private LayerMask highlightedLayers;

	private List<GameObject> slowMotionObjects;
	private float timer;
	
	void Start ()
	{
		slowMotionObjects = new List<GameObject> ();
		highlightedLayers = LayerMask.NameToLayer ("Highlighted");
		CreateHighlightCamera ();
	}

	private void CreateHighlightCamera()
	{
		realCamera = GetComponent<Camera>();
		realCamera.depthTextureMode = DepthTextureMode.Depth;

		GameObject cameraObject = new GameObject("camera_highlight_object");
		highlightCamera = cameraObject.AddComponent<Camera>();
		highlightCamera.CopyFrom(realCamera);
		highlightCamera.enabled = false;
		highlightCamera.clearFlags = CameraClearFlags.Nothing;
		//highlightCamera.rect = new Rect(0, 0, 1, 1);
		highlightCamera.backgroundColor = new Color(0, 0, 0, 1);
		highlightCamera.cullingMask = 1 << LayerMask.NameToLayer("Highlighted");
		highlightCamera.depthTextureMode = DepthTextureMode.None;
	}

	void Update ()
	{
		if (slowMotionObjects.Count > 0) {
			timer = Mathf.Min (timer + Time.deltaTime / Time.timeScale, 1f);
		} else {
			timer = Mathf.Max (timer - 2*Time.deltaTime / Time.timeScale, 0f);
		}

		Time.timeScale = Mathf.Lerp (1f, 0.33f, timer);
		ShaderMaterial.SetFloat ("_Saturation", Mathf.Lerp(1f, 0.2f, timer));
	}

	public void StartSlowMotion(GameObject obj)
	{
		slowMotionObjects.Add (obj);
		//obj.SetLayerRecursively (highlightedLayers);
	}

	public void StopSlowMotion(GameObject obj)
	{
		slowMotionObjects.Remove (obj);
		//obj.SetLayerRecursively (LayerMask.NameToLayer("Default"));
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, ShaderMaterial);

		/*highlightCamera.transform.position = realCamera.transform.position;
		highlightCamera.transform.rotation = realCamera.transform.rotation;
		highlightCamera.transform.localScale = realCamera.transform.localScale;

		highlightCamera.targetTexture = destination;
		highlightCamera.Render ();*/
	}
}
