using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExerciseAnalyticsNamespace;

public class ExerciseAnalyticsController : MonoBehaviour
{
    [SerializeField] private ExerciseAnalyticsModel exerciseAnalyticsModel;
    [SerializeField] private ExerciseAnalyticsView exerciseAnalyticsView;

	private void OnEnable()
	{
		exerciseAnalyticsView.OnDayButtonClicked += ShowDayAnalyticsOnBarchart;
		exerciseAnalyticsView.OnWeekButtonClicked += ShowWeekAnalyticsOnBarchart;
		exerciseAnalyticsView.OnMonthButtonClicked += ShowMonthAnalyticsOnBarchart;
		exerciseAnalyticsView.OnYearButtonClicked += ShowYearAnalyticsOnBarchart;
	}

	private void OnDisable()
	{
		exerciseAnalyticsView.OnDayButtonClicked -= ShowDayAnalyticsOnBarchart;
		exerciseAnalyticsView.OnWeekButtonClicked -= ShowWeekAnalyticsOnBarchart;
		exerciseAnalyticsView.OnMonthButtonClicked -= ShowMonthAnalyticsOnBarchart;
		exerciseAnalyticsView.OnYearButtonClicked -= ShowYearAnalyticsOnBarchart;
	}

	private void ShowDayAnalyticsOnBarchart()
	{
		ExerciseCompletionShowableData dayData = exerciseAnalyticsModel.GetDayDataAnalytics();
		exerciseAnalyticsView.ShowAnalyticsOnBarCharts(dayData, "День");
	}

	private void ShowWeekAnalyticsOnBarchart()
	{
		ExerciseCompletionShowableData weekData = exerciseAnalyticsModel.GetWeekDataAnalytics();
		exerciseAnalyticsView.ShowAnalyticsOnBarCharts(weekData, "Неделя");
	}

	private void ShowMonthAnalyticsOnBarchart()
	{
		ExerciseCompletionShowableData monthData = exerciseAnalyticsModel.GetMonthDataAnalytics();
		exerciseAnalyticsView.ShowAnalyticsOnBarCharts(monthData, "Месяц");
	}

	private void ShowYearAnalyticsOnBarchart()
	{
		ExerciseCompletionShowableData yearData = exerciseAnalyticsModel.GetYearDataAnalytics();
		exerciseAnalyticsView.ShowAnalyticsOnBarCharts(yearData, "Год");
	}
}
