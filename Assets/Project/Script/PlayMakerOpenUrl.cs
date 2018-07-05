using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("打開網頁")]
	public class PlayMakerOpenUrl : FsmStateAction
	{
		
		[RequiredField]
		[UIHint(UIHint.TextArea)]
		public FsmString Url;
		

	
		public override void OnEnter()
		{
		Application.OpenURL(Url.Value);
		Finish();
		}
		

	}
}