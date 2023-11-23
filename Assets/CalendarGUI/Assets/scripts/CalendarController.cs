using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarController : MonoBehaviour
{
    public GameObject _calendarPanel;
    public Text _yearNumText;
    public Text _monthNumText;

    public GameObject _item;

    public List<GameObject> _dateItems = new List<GameObject>();
    private Dictionary<DateTime, CalendarDateItem> datesAndAddedDateItems = new Dictionary<DateTime, CalendarDateItem>();

    const int _totalDateNum = 42;

    private DateTime _dateTime;
    public static CalendarController _calendarInstance;

    public Action<DateTime> OnClickDateForCompleteExerciseAction;

    void Awake()
    {
        _calendarInstance = this;
        Vector3 startPos = _item.transform.localPosition;
        _dateItems.Clear();
        _dateItems.Add(_item);

        for (int i = 1; i < _totalDateNum; i++)
        {
            GameObject item = GameObject.Instantiate(_item) as GameObject;
            item.name = "Item" + (i + 1).ToString();
            item.transform.SetParent(_item.transform.parent);
            item.transform.localScale = Vector3.one;
            item.transform.localRotation = Quaternion.identity;
            item.transform.localPosition = new Vector3((i % 7) * 31 + startPos.x, startPos.y - (i / 7) * 25, startPos.z);

            _dateItems.Add(item);
        }

        _dateTime = DateTime.Now;
        CreateCalendar();
    }

    void CreateCalendar()
    {
        DateTime firstDay = _dateTime.AddDays(-(_dateTime.Day - 1));
        int index = GetDays(firstDay.Date.DayOfWeek);

        int date = 0;
		datesAndAddedDateItems.Clear();

		for (int i = 0; i < _totalDateNum; i++)
        {
            _dateItems[i].SetActive(false);

            if (i >= index)
            {
                DateTime thatDay = firstDay.Date.AddDays(date);
                if (thatDay.Month == firstDay.Month)
                {
                    _dateItems[i].SetActive(true);

					CalendarDateItem currentDateItem = _dateItems[i].GetComponent<CalendarDateItem>();
                    currentDateItem.ShowDateItem(thatDay);
                    datesAndAddedDateItems.Add(thatDay, currentDateItem);

					date++;
                }
            }
        }
        _yearNumText.text = _dateTime.Year.ToString();
        _monthNumText.text = _dateTime.Month.ToString();
    }

    int GetDays(DayOfWeek day)
    {
        switch (day)
        {
            case DayOfWeek.Monday: return 0;
            case DayOfWeek.Tuesday: return 1;
            case DayOfWeek.Wednesday: return 2;
            case DayOfWeek.Thursday: return 3;
            case DayOfWeek.Friday: return 4;
            case DayOfWeek.Saturday: return 5;
            case DayOfWeek.Sunday: return 6;
        }

        return 0;
    }

	public void YearPrev()
    {
        _dateTime = _dateTime.AddYears(-1);
        CreateCalendar();
    }

    public void YearNext()
    {
        _dateTime = _dateTime.AddYears(1);
        CreateCalendar();
    }

    public void MonthPrev()
    {
        _dateTime = _dateTime.AddMonths(-1);
        CreateCalendar();
    }

    public void MonthNext()
    {
        _dateTime = _dateTime.AddMonths(1);
        CreateCalendar();
    }

	public void HighlightCompletedExerciseByDate(DateTime dateTime)
	{
		if (datesAndAddedDateItems.ContainsKey(dateTime))
		{
			datesAndAddedDateItems[dateTime].HighlightDateItem();
		}
	}

	public void ShowCompletedExercisesByDates(HashSet<DateTime> completedExerciseDates)
	{
		foreach (DateTime date in completedExerciseDates)
		{
			HighlightCompletedExerciseByDate(date);
		}
	}
}
