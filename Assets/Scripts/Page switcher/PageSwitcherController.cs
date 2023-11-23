using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageSwitcherController : MonoBehaviour
{
    private PageSwitcherView pageSwitcherView;

	private static Action<PageSwitcherController> OnHidePageAction;

	private void Awake()
	{
		pageSwitcherView = GetComponent<PageSwitcherView>();
		if(pageSwitcherView.IsTransparencyOfPageIsZero())
		{
			pageSwitcherView.HidePageAndDisableRaycastInteracting();
		}
	}

	private void OnEnable()
	{
		OnHidePageAction += OnHidePageOnAllControllers;
	}

	private void OnDisable()
	{
		OnHidePageAction -= OnHidePageOnAllControllers;
	}

	private void OnHidePageOnAllControllers(PageSwitcherController controllerWithShowablePage)
	{
		if(controllerWithShowablePage != this)
		{
			pageSwitcherView.HidePageAndDisableRaycastInteracting();
		}
	}

	public void ShowThisPage()
    {
		pageSwitcherView.ShowPageAndActivateRaycastInteracting();
		OnHidePageAction?.Invoke(this);
	}
}
