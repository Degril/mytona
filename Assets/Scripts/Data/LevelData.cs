using System.Collections.Generic;
using UnityEngine;

// Bonus!
// Create and use namespace Myproject in whole solution with appropriate subspaces 
[CreateAssetMenu(menuName = "Data/LevelData")]
public class LevelData : ScriptableObject
{
	[SerializeField] private int index;

	public int Index => index;

	public bool[,] GetMap()
	{
		bool[,] map = new bool[12, 12];
		var lines = CharMap.Split('\n', '\r');
		for (int i = 0; i < 12; i++)
		{
			for (int j = 0; j < 12; j++)
			{
				map[i, j] = lines[i][j] == '1';
			}
		}

		return map;
	}

	[TextArea(16, 16)] public string CharMap;

	public List<WaveData> WaveDatas;
	[SerializeField] private float waveInterval = 5f;
	public float WaveInterval => waveInterval;
}