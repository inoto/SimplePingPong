using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SimplePingPong
{
	public class GameController : MonoBehaviour
	{
		public static GameController Instance;

		public static event Action<int, int> OnBestScoresUpdatedEvent; 

		public bool GameStarted { get; protected set; }

		[SerializeField] protected GameConfig Config;
		[SerializeField] protected GameObject Field;
		[SerializeField] protected Gate TopGate, BottomGate;
		[SerializeField] protected Racket TopPlayer, BottomPlayer;
		[SerializeField] protected Ball Ball;
		[SerializeField] CameraController CameraController = null;
		
		protected int TopScore = 0, BottomScore = 0;
		protected int BestTopScore = 0, BestBottomScore = 0;

		public Color32[] BallColors => Config.BallColors;
		public Color32 BallColor => Ball.Color;
		
		protected virtual void Awake()
		{
			Instance = this;
		}

		void OnEnable()
		{
			SaveLoadSystem.OnScoresLoadedEvent += SaveLoadSystemOnScoresLoadedEvent;
			GameUIController.OnBallColorChangedEvent += OnBallColorChangedEvent;
			SaveLoadSystem.OnBallColorLoadedEvent += OnBallColorChangedEvent;
		}
		
		void OnDisable()
		{
			SaveLoadSystem.OnScoresLoadedEvent -= SaveLoadSystemOnScoresLoadedEvent;
			GameUIController.OnBallColorChangedEvent -= OnBallColorChangedEvent;
			SaveLoadSystem.OnBallColorLoadedEvent -= OnBallColorChangedEvent;
		}

		void OnBallColorChangedEvent(Color32 color)
		{
			Ball.Color = color;
		}

		void SaveLoadSystemOnScoresLoadedEvent(int bestTopScore, int bestBottomScore)
		{
			BestTopScore = bestTopScore;
			BestBottomScore = bestBottomScore;
			
			RaiseOnBestScoresUpdatedEvent();
		}

		public virtual void RestartGame()
		{
			SetupField();
			SetupRackets();
			SetupBall();
		}
		
		public void SetupRackets()
		{
			TopPlayer.Speed = BottomPlayer.Speed = Config.RacketSpeed;

			float limiterExtent = Config.FieldSize.x / 2 - Config.RacketSize.x / 2;
			TopPlayer.SetLimiterExtent(limiterExtent);
			BottomPlayer.SetLimiterExtent(limiterExtent);
			
			TopPlayer.transform.localScale =
				BottomPlayer.transform.localScale =
					new Vector2(Config.RacketSize.x, Config.RacketSize.y);

			float playerYPos = Config.FieldSize.y / 2 - Config.RacketSize.y / 2;
			TopPlayer.transform.position = new Vector2(0, playerYPos);
			BottomPlayer.transform.position = new Vector2(0, -playerYPos);
		}

		protected void SetupField()
		{
			Field.transform.localScale = new Vector3(Config.FieldSize.x, Config.FieldSize.y, 1);
			CameraController.SetCameraSize(Config.FieldSize);
		}

		protected void SetupBall()
		{
			Ball.Speed = Random.Range(Config.MinMaxBallSpeed.x, Config.MinMaxBallSpeed.y);
			Ball.Scale = Random.Range(Config.MinMaxBallScale.x, Config.MinMaxBallScale.y);
		}

		public void GameOver()
		{
			GameStarted = false;
		}

		protected virtual void ResetBall()
		{
			Ball.Reset();
		}

		protected void RaiseOnBestScoresUpdatedEvent()
		{
			OnBestScoresUpdatedEvent?.Invoke(BestTopScore, BestBottomScore);
		}

		void OnDrawGizmos()
		{
			if (Config != null)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawWireCube(Vector3.zero, Config.FieldSize);
				if (BottomPlayer != null)
					Gizmos.DrawWireCube(BottomPlayer.transform.position, Config.RacketSize);
			}
		}
	}
}