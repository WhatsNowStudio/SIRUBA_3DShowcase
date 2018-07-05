using System.Collections;
using System.Collections.Generic;
using IndieYP;
using UnityEngine;


namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Device)]
    [Tooltip("取得裝置預設資料路徑")]
    public class PMOpenPDF : FsmStateAction
    {
        
        [RequiredField]
        [UIHint(UIHint.Variable)]
        public FsmString PDFName;
    
        public override void OnEnter()
        {
            
            #if UNITY_ANDROID
            {
                var androidPdfName = PDFName.Value.Split('.')[0];
                StartCoroutine(PDFReader.OpenDocLocal (androidPdfName));
            }
            #elif UNITY_IOS
            {
                var docPath = PDFReader.AppDataPath + "/" + PDFName.Value;
                PDFReader.OpenDocInWebViewLocal(docPath, PDFName.Value);
                PDFReader.SetWebviewPageOffset(0, docPath);
                //PDFReader.OpenDocInMenu (docPath, false);
            }
            #else
            {
                Application.OpenURL(System.IO.Path.Combine(Application.persistentDataPath, PDFName.Value));
            }
            #endif
            Finish();
        }

    }
}