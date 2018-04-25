using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Illness", menuName = "Illness/Illness")]
public class IllnessAsset : ScriptableObject {
	public string Name;
	public Color color;
	public IllnessTypes Type;
	public Sprite Icon;
}
