using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageSwitcherView : MonoBehaviour
{
	[SerializeField] private CanvasGroup currentPage;

	public void HidePageAndDisableRaycastInteracting()
	{
		currentPage.alpha = 0;
		currentPage.blocksRaycasts = false;
	}

	public void ShowPageAndActivateRaycastInteracting()
	{
		currentPage.alpha = 1;
		currentPage.blocksRaycasts = true;
	}

	public bool IsTransparencyOfPageIsZero() => currentPage.alpha == 0;
}
