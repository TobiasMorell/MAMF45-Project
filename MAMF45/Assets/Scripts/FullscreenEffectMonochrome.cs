using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenEffectMonochrome : MonoBehaviour {

	public Material ShaderMaterial;

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, ShaderMaterial);
	}
}
