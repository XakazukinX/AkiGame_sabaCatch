using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace falling
{
	public class FallingManager : SingletonMonoBehaviour<FallingManager>
	{
		public float fallingSpeed;
		public int fallingDist;

		[SerializeField] private GameObject[] fallingItems;
		private int maxItemsIndex;
		private int minItemsIndex;

		private int maxSpawnPos;
		private int minSpawnPos;
		private int spawnPosSpan;

		[SerializeField] private float spawnWaitTime;

		private void Start()
		{
			maxItemsIndex = fallingItems.Length;
			minItemsIndex = 0;

			maxSpawnPos = PlayerManager.Instance._maxPlayerMoveCount;
			minSpawnPos = PlayerManager.Instance._minPlayerMoveCount;
			spawnPosSpan = PlayerManager.Instance._moveDist;

			StartCoroutine(spawn());
		}


		private IEnumerator spawn()
		{
			while (true)
			{
				yield return new WaitForSeconds(spawnWaitTime);
				var spawnObject = Instantiate(fallingItems[Random.Range(minItemsIndex, maxItemsIndex)] , new Vector3(Random.Range(minSpawnPos,maxSpawnPos) * spawnPosSpan, 7 , 0) , Quaternion.identity);
				Destroy(spawnObject,10);
			}
		}

	}

}
