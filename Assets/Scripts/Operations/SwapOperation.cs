﻿public class SwapOperation : IOperation, IPatternOperation, IPatternSelectorOperation
{
	private DesignPattern Pattern;
	private bool _IsFinished = false;

	public SwapOperation(DesignPattern pattern)
	{
		this.Pattern = pattern;
	}

	public void Abort()
	{
		_IsFinished = true;
	}

	public string GetName()
	{
		return Pattern.Name;
	}

	public DesignPattern GetPattern()
	{
		return this.Pattern;
	}

	public bool IsFinished()
	{
		return this._IsFinished;
	}

	public void SelectPattern(DesignPattern pattern)
	{
		/*
		if (pattern == Pattern) return;
		var backup = new DesignPattern();
		
		if (Pattern.IsPro)
		{
			backup.CopyFrom(Controller.Instance.CurrentSavegame.ProDesignPatterns[Pattern.Index]);
			Controller.Instance.CurrentSavegame.ProDesignPatterns[Pattern.Index].CopyFrom(Controller.Instance.CurrentSavegame.ProDesignPatterns[pattern.Index]);
			Controller.Instance.CurrentSavegame.ProDesignPatterns[pattern.Index].CopyFrom(backup);
		}
		else
		{
			backup.CopyFrom(Controller.Instance.CurrentSavegame.DesignPatterns[Pattern.Index]);
			Controller.Instance.CurrentSavegame.DesignPatterns[Pattern.Index].CopyFrom(Controller.Instance.CurrentSavegame.DesignPatterns[pattern.Index]);
			Controller.Instance.CurrentSavegame.DesignPatterns[pattern.Index].CopyFrom(backup);
		}
   
		_IsFinished = true;*/
	}

	public void Start()
	{
		Controller.Instance.Popup.SetText("To swap your <#FF6666>pattern<#FFFFFF> please select the spot you want to swap with.", false, () => { return true; });
	}
}