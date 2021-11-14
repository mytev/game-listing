using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Replay;
using UnityEngine.Networking;

namespace Unity.Metacast.Demo
{
    /// <summary>
    ///     Populate UIBrowser with test json data
    /// </summary>
    public class TestListings : MonoBehaviour
    {
        [SerializeField] private string m_ApiBaseUrl;
        /// <summary>
        ///     Start is called on the frame when a script is enabled just
        ///     before any of the Update methods are called the first time.
        /// </summary>
        private void Start()
        {
            StartCoroutine(GetText());
        }

        IEnumerator GetText()
        {
            UnityWebRequest www = UnityWebRequest.Get($"{m_ApiBaseUrl}/listings");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                UIBrowser.instance.Init($@"{{ ""listings"": {www.downloadHandler.text} }}");
            }
        }
    }
}