using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpView : MonoBehaviour
{
    [SerializeField] private Button nextPage;
    [SerializeField] private Button previousPage;
    [SerializeField] private Image[] pageImages;

    private int currentIndexOfPage = 0;

	private void Start()
	{
		nextPage.onClick.AddListener(SwitchNextPage);
		previousPage.onClick.AddListener(SwitchPreviousPage);
	}

	private void SwitchNextPage()
	{
		currentIndexOfPage++;
		ShowNeedingPage();
	}

	private void SwitchPreviousPage()
	{
		currentIndexOfPage--;
		ShowNeedingPage();
	}

	private void ShowNeedingPage()
	{
		int indexToActivatePage = Mathf.Abs(currentIndexOfPage) % pageImages.Length;

		foreach (var eachPage in pageImages)
		{
			eachPage.enabled = false;
		}
		pageImages[indexToActivatePage].enabled = true;
	}
}
