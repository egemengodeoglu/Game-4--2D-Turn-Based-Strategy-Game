using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Waves", menuName = "Wave")]
public class WaveScriptableObject : ScriptableObject
{
	public List<Wave> waves;
}
[System.Serializable]
public enum EnemyTypes
{
	WARRIOR = 1,
	ARCHER = 1,
	WIZARD = 1,
	SUICEDER = 1,
	BOSS = 1
}

[System.Serializable]
public class Enemies
{
	public EnemyTypes enemy;

}

[System.Serializable]
public class Wave
{
	public List<Enemies> wave;
}

