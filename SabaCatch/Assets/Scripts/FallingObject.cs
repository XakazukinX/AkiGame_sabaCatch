using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace falling
{
	public class FallingObject : MonoBehaviour
	{
		public FallingObjectType.ObjectType _fallingObjectType;

		private float _fallingSpeed;
		private int _fallingDist;
		
		private void Start()
		{
			_fallingSpeed = FallingManager.Instance.fallingSpeed;
			_fallingDist = FallingManager.Instance.fallingDist;

			StartCoroutine(falling());
		}


		private IEnumerator falling()
		{
			while (true)
			{
				gameObject.transform.position = new Vector3(transform.position.x,transform.position.y - _fallingDist,transform.position.z);
				yield return new WaitForSeconds(_fallingSpeed);
			}
		}

	}
}
