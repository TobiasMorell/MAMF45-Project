using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[ExecuteInEditMode]
public class GrassGenerator : MonoBehaviour {

	private Mesh grassMesh;
	public MeshFilter grassMeshFilter;

	[Range(0, 1000000)]
	public int grassCount = 10;
	[Range(0, 100)]
	public float size = 10;

	private Vector3 lastPos;
	private float oldSize;
	private int oldCount;

	private Texture2D bendTexture;
	[Range(10, 1000)]
	public int BendTextureSize = 100;
	private int oldBendSize;
	[Range(0, 1)]
	public float BendSphereSize = 0.1f;

	// Use this for initialization
	void Start () {
		lastPos = transform.position;
		oldCount = grassCount;
		oldSize = size;
		oldBendSize = BendTextureSize;
		bendTexture = new Texture2D(BendTextureSize, BendTextureSize);
		for (var i = 0; i < BendTextureSize; i++)
			for (var j = 0; j < BendTextureSize; j++)
				bendTexture.SetPixel(i, j, Color.green);
		bendTexture.Apply();
		GetComponent<Renderer>().material.SetTexture("Bend Texture", bendTexture);
	}
	
	void Update () {
		if (lastPos != transform.position || oldCount != grassCount || oldSize != size)
		{
			List<Vector3> positions = new List<Vector3>(grassCount);
			List<Vector3> normals = new List<Vector3>(grassCount);
			List<Vector2> uv2s = new List<Vector2>(grassCount);
			List<Color> colors = new List<Color>(grassCount);
			int[] indices = new int[grassCount];
			for (int i = 0; i < grassCount; i++)
			{
				var dir = Random.Range(0, Mathf.PI * 2);
				var spawnpoint = transform.position + Vector3.up * 5 + new Vector3(Mathf.Cos(dir), 0, Mathf.Sin(dir)) * Random.Range(0, size) * Random.value;
				var hit = new RaycastHit();
				var hitAnything = Physics.Raycast(spawnpoint, -Vector3.up, out hit, 10, 1 << 9);
				Vector3 bladePosition = hit.point;
				positions.Add(bladePosition);
				normals.Add(Vector3.up);
				var v = 0.25f * Random.value;
				colors.Add(new Color(v, 1, 0));
				indices[i] = i;
				uv2s.Add(new Vector2(bladePosition.x/size / 2 + 0.5f,bladePosition.z/size / 2 + 0.5f));
			}
			grassMesh = new Mesh();
			grassMesh.SetVertices(positions);
			grassMesh.SetIndices(indices, MeshTopology.Points, 0);
			grassMesh.SetNormals(normals);
			grassMesh.SetColors(colors);
			grassMesh.SetUVs(1, uv2s);
			grassMeshFilter.mesh = grassMesh;
		}

		if (oldBendSize != BendTextureSize)
		{
			bendTexture = new Texture2D(BendTextureSize, BendTextureSize);
			for (var i = 0; i < BendTextureSize; i++)
				for (var j = 0; j < BendTextureSize; j++)
					bendTexture.SetPixel(i, j, Color.green);
			bendTexture.Apply();
			GetComponent<Renderer>().material.SetTexture("Bend Texture", bendTexture);
		}

		lastPos = transform.position;
		oldCount = grassCount;
		oldSize = size;
		oldBendSize = BendTextureSize;

		//UpdateBendTexture();
	}

	private void UpdateBendTexture()
	{
		var bodies = new List<GameObject>();
		var bunnies = GameObject.FindGameObjectsWithTag("Bunny");
		bodies.AddRange(bunnies);
		//var player = GameObject.Find("Player").GetComponent<Player>();
		//bodies.Add(player.leftHand.gameObject);
		//bodies.Add(player.rightHand.gameObject);
		//bodies.Add(player.headCollider.gameObject);

		var delta = size / BendTextureSize;
		var sqrRadius = BendSphereSize * BendSphereSize;
		for (var i = 0; i < BendTextureSize; i++) {
			for (var j = 0; j < BendTextureSize; j++) {
				bendTexture.SetPixel(i, j, Color.green);
				foreach (var body in bodies) {
					var vec = (transform.position + new Vector3(i - BendTextureSize / 2, 0, j - BendTextureSize / 2) * delta - body.transform.position);
					if (vec.sqrMagnitude < sqrRadius)
					{
						vec /= BendSphereSize;
						bendTexture.SetPixel(i, j, new Color(vec.x, vec.y, vec.z));
					}
				}
			}
		}
		bendTexture.Apply();
	}

	private void OnDrawGizmos()
	{
		//Gizmos.DrawGUITexture(new Rect(0, 1, 1 , 1 ), bendTexture);
		//Gizmos.DrawWireSphere(transform.position, size);
	}
}
