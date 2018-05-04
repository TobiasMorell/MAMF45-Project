using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombstoneSpawner : MonoBehaviour {

	public GameObject Tombstone;
	public float range = 2;

	public void SpawnTombstone()
	{
		for (var i = 0; i < 100; ++i)
		{
			var dir = Random.Range(0, Mathf.PI * 2);
			var spawnpoint = transform.position + new Vector3(Mathf.Cos(dir), 0, Mathf.Sin(dir)) * Random.Range(0, range);
			var hit = new RaycastHit();
			var hitAnything = Physics.Raycast(spawnpoint, -Vector3.up, out hit);
			if (hitAnything && hit.collider.gameObject.tag != "Tombstone")
			{
				Instantiate(Tombstone, hit.point + Vector3.up*0.35f, Quaternion.identity);
				return;
			}
		}
	}

	private void OnDrawGizmos()
	{
		var p1 = Vector3.right * range;
		var p2 = Vector3.right * range;
		for (int i = 0; i <= 20; i++)
		{
			p1 = new Vector3(Mathf.Cos(Mathf.PI / 10 * i), 0, Mathf.Sin(Mathf.PI / 10 * i)) * range;
			Gizmos.DrawLine(transform.position + p1, transform.position + p2);
			p2 = p1;
		}
	}
}
