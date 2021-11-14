using System.Collections;
using UnityEngine;
using UnityEngine.Replay;
using UnityEngine.Networking;

namespace UnityEngine.Replay
{
    /// <summary>
    ///     A UI panel containing a number of image and text objects
    /// </summary>
    public class UIPanel : UIEntity
    {
        public int panelId { get; private set; }

        public int width
        {
            get { return Mathf.RoundToInt(m_RectTransform.sizeDelta.x); }
        }

        public int height
        {
            get { return Mathf.RoundToInt(m_RectTransform.sizeDelta.y); }
        }

        /// <summary>
        ///     Sets the panel position
        /// </summary>
        /// <param name="newPos">The new position to set</param>
        public void SetPosition(Vector2Int newPos)
        {
            m_RectTransform.anchoredPosition = newPos;
        }

        /// <summary>
        ///     Sets the entity's content based on a Listing
        /// </summary>
        /// <param name="l">The Listing to use</param>
        public override void SetData(Listing l)
        {

            m_Listing = l;
            foreach (var text in m_Texts) text.SetText(l.GetText(text.textType));
            SetImage();


            StartCoroutine(GetTextures());
        }

        IEnumerator GetTextures()
        {
            for (var index = 0; index < m_Listing.images.Length; index++)
            {
                var image = m_Listing.images[index];
                if (!image.isLoaded)
                {
                    var www = UnityWebRequestTexture.GetTexture(image.url);
                    yield return www.SendWebRequest();

                    if (www.result != UnityWebRequest.Result.Success)
                    {
                        Debug.Log(www.error);
                    }
                    else
                    {
                        m_Listing.images[index].texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                        m_Listing.images[index].isLoaded = true;
                    }

                }
            }

            SetImage();
        }

        private void SetImage()
        {
            foreach (var image in m_Images)
            {
                image.SetImage(m_Listing.GetImage(image.imageType));
            }
        }
    }
}
