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

		[SerializeField] public float spawnWaitTime;

		private bool isEnd;
		
		private int[] spawnXList = new int[7];

		private void Start()
		{
			for (int i = 0; i < spawnXList.Length; i++)
			{
				spawnXList[i] = (-6 + (i * 2)); 
				Debug.Log(spawnXList[i]);
			}
			
			maxItemsIndex = fallingItems.Length;
			minItemsIndex = 0;

			maxSpawnPos = PlayerManager.Instance._maxPlayerMoveCount;
			minSpawnPos = PlayerManager.Instance._minPlayerMoveCount;
			spawnPosSpan = PlayerManager.Instance._moveDist;

			StartCoroutine(spawn());
		}


		private IEnumerator spawn()
		{
			while (!isEnd)
			{
				yield return new WaitForSeconds(spawnWaitTime);
				var spawnItem = fallingItems[Random.Range(minItemsIndex, maxItemsIndex)];
				var spawnObject = Instantiate(spawnItem,
					new Vector3(spawnXList[Random.Range(0, 7)], 7, 0), spawnItem.transform.rotation);
				Destroy(spawnObject,10);
			}
		}

		public void stopSpawnObjects()
		{
			isEnd = true;
		}

	}

}
