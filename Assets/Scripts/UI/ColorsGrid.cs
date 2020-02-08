using TMPro;
using UnityEngine;

namespace SimplePingPong
{
	public class ColorsGrid : MonoBehaviour
	{
		[SerializeField] GameObject ElementPrefab = null;

		public void Init()
		{
			ElementPrefab.SetActive(false);
			bool first = true;
			foreach (var color in GameController.Instance.BallColors)
			{
				GameObject go = Instantiate(ElementPrefab, transform);
				go.transform.localScale = Vector3.one;
				go.SetActive(true);
				ColorCell cell = go.GetComponent<ColorCell>();
				if (first)
				{
					cell.OutlineImage.enabled = true;
					first = false;
				}
				else
					cell.OutlineImage.enabled = false;
				cell.ColorImage.color = color;
			}
		}

		void OnEnable()
		{
			GameUIController.OnBallColorChangedEvent += OnBallColorChangedEvent;
			SaveLoadSystem.OnBallColorLoadedEvent += OnBallColorChangedEvent;
		}

		void OnDisable()
		{
			GameUIController.OnBallColorChangedEvent -= OnBallColorChangedEvent;
			SaveLoadSystem.OnBallColorLoadedEvent -= OnBallColorChangedEvent;
		}

		void OnBallColorChangedEvent(Color32 color)
		{
			SelectColorCell(color);
		}

		public void SelectColorCell(Color32 color)
		{
			foreach (var cell in transform.GetComponentsInChildren<ColorCell>()) // TODO: use list instead
			{
				if (cell.ColorImage.color.CompareRGB(color))
					cell.OutlineImage.enabled = true;
				else
					cell.OutlineImage.enabled = false;
			}
		}
	}
}