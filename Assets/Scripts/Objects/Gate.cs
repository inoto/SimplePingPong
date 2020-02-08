using System;
using UnityEngine;

namespace SimplePingPong
{
	[RequireComponent(typeof(BoxCollider2D))]
	public class Gate : MonoBehaviour
	{
		public event Action<PlayerSide> OnGoal;
		
		public PlayerSide Side = PlayerSide.Bottom;

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Ball")) OnGoal?.Invoke(Side);
		}
	}
}