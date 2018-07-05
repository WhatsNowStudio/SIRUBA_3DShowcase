using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Web Player")]
	[Tooltip("下載並命名")]
	public class PlayMakerDownloadFile : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Url to download data from.")]
		public FsmString url;
		[Tooltip("Gets text from the url.")]
		public FsmString FilePathAndName;

		[ActionSection("Results")]

		[UIHint(UIHint.Variable)]
		[Tooltip("Error message if there was an error during the download.")]
		public FsmString errorString;

		[UIHint(UIHint.Variable)] 
		[Tooltip("How far the download progressed (0-1).")]
		public FsmFloat progress;

		[ActionSection("Events")] 
		
		[Tooltip("Event to send when the data has finished loading (progress = 1).")]
		public FsmEvent isDone;
		
		[Tooltip("Event to send if there was an error.")]
		public FsmEvent isError;

		private WWW wwwObject;



		public override void Reset()
		{
			progress = null;
			url = null;
			FilePathAndName = null;
			errorString = null;
			wwwObject = null;
			isDone = null;
			isError = null;
		}
		
        public override void OnEnter()
		{
			if (string.IsNullOrEmpty(url.Value) || string.IsNullOrEmpty(FilePathAndName.Value))
			{
				Finish();
				return;
			}

			wwwObject = new WWW(url.Value);
		}


		public override void OnUpdate()
		{
			if (wwwObject == null)
			{
				errorString.Value = "WWW Object is Null!";
				Finish();
				return;
			}

			errorString.Value = wwwObject.error;

			if (!string.IsNullOrEmpty(wwwObject.error))
			{
				Fsm.Event(isError);
				return;
			}

			progress.Value = wwwObject.progress;

			if (wwwObject.isDone && string.IsNullOrEmpty(wwwObject.error))
			{

				//errorString.Value = wwwObject.error;
				//Fsm.Event(string.IsNullOrEmpty(errorString.Value) ? isDone : isError);

					FileStream FS = File.Create(FilePathAndName.Value);
					FS.Write(wwwObject.bytes,0,wwwObject.bytes.Length);
					FS.Close();
					Fsm.Event(isDone);
				
			}
		}
		

	}
}