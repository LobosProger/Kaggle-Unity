using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExerciseAnalyticsNamespace;

public class ExerciseAnalyticsView : MonoBehaviour
{
    [SerializeField] private BarView barPrefab;
    [SerializeField] private Transform barsPanel;

	[SerializeField] private Button daySelection;
	[SerializeField] private Button weekSelection;
	[SerializeField] private Button monthSelection;
	[SerializeField] private Button yearSelection;

	public Action OnDayButtonClicked;
	public Action OnWeekButtonClicked;
	public Action OnMonthButtonClicked;
	public Action OnYearButtonClicked;

	private void Awake()
	{
		daySelection.onClick.AddListener(OnDayButtonClickedEvent);
		weekSelection.onClick.AddListener(OnWeekButtonClickedEvent);
		monthSelection.onClick.AddListener(OnMonthButtonClickedEvent);
		yearSelection.onClick.AddListener(OnYearButtonClickedEvent);
	}

	public void ShowAnalyticsOnBarCharts(ExerciseCompletionShowableData exerciseCompletionData)
	{
		ClearPanelOfBarChart();
		for (int i=0; i < exerciseCompletionData.GetAmountOfShowingBargraphs(); i++)
		{
			float regularityFillAmount = exerciseCompletionData.GetRegularityForBargraphByIndex(i);
			float timeFillAmount = exerciseCompletionData.GetTimeForBargraphByIndex(i);
			float difficultyFillAmount = exerciseCompletionData.GetDifficultyForBargraphByIndex(i);
			
			CreateBarChart(regularityFillAmount, timeFillAmount, difficultyFillAmount, $"Week{i+1}");
		}
	}

	private void CreateBarChart(float regularityValue, float timeValue, float difficultyValue, string titleOfBar)
	{
		BarView barView = Instantiate(barPrefab, barsPanel);
		barView.ShowDataOnBar(regularityValue, timeValue, difficultyValue, titleOfBar);
	}

	private void ClearPanelOfBarChart()
	{
		foreach(Transform eachBar in barsPanel)
		{
			Destroy(eachBar.gameObject);
		}
	}

	private void OnDayButtonClickedEvent()
	{
		OnDayButtonClicked?.Invoke();
	}

	private void OnWeekButtonClickedEvent()
	{
		OnWeekButtonClicked?.Invoke();
	}

	private void OnMonthButtonClickedEvent()
	{
		OnMonthButtonClicked?.Invoke();
	}

	private void OnYearButtonClickedEvent()
	{
		OnYearButtonClicked?.Invoke();
	}
}
