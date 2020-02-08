using UnityEngine;

namespace SimplePingPong
{
	[CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig", order = 0)]
	public class GameConfig : ScriptableObject
	{
		[SerializeField] Vector2 fieldSize = new Vector2(100, 70);
		public Vector2 FieldSize => fieldSize;

		[SerializeField] Vector2 racketSize = new Vector2(8, 2);
		public Vector2 RacketSize => racketSize;

		[SerializeField] float racketSpeed = 5f;
		public float RacketSpeed => racketSpeed;

		[SerializeField] int scoresToWin = 10;
		public int ScoresToWin => scoresToWin;
		
		[SerializeField] Vector2 minMaxBallSpeed = new Vector2(5f, 8f);
		public Vector2 MinMaxBallSpeed => minMaxBallSpeed;
		
		[SerializeField] Vector2 minMaxBallScale = new Vector2(1f, 3f);
		public Vector2 MinMaxBallScale => minMaxBallScale;

		[SerializeField] Color32[] ballColors = new Color32[1];
		public Color32[] BallColors => ballColors;
	}
}