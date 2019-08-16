using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Xml.Linq;
using System;
using System.IO;
using System.Xml;


namespace TweetWithScreenShot

{

    public class CustomTweetManager : SingletonMonoBehaviourFast<CustomTweetManager>
    {

        private static CustomTweetManager sinstance;

        public string[] hashTags;



        [SerializeField]

        private string[] clientID;

        private int useClientIndex = 0;



        public string ClientID

        {

            get

            {

                if (string.IsNullOrEmpty(clientID[useClientIndex])) throw new Exception("ClientIDをセットしてください");

                return clientID[useClientIndex];

            }

        }



        public static CustomTweetManager Instance

        {

            get

            {

                if (sinstance == null)

                {

                    sinstance = FindObjectOfType<CustomTweetManager>();

                    if (sinstance == null)

                    {

                        var obj = new GameObject(typeof(CustomTweetManager).Name);

                        sinstance = obj.AddComponent<CustomTweetManager>();

                    }

                }

                return sinstance;

            }

        }



        public IEnumerator TweetWithScreenShot(string text)

        {

            yield return new WaitForEndOfFrame();

            var tex = ScreenCapture.CaptureScreenshotAsTexture();



            // imgurへアップロード

            string UploadedURL = "";



            UnityWebRequest www;

            WWWForm wwwForm = new WWWForm();

            wwwForm.AddField("image", Convert.ToBase64String(tex.EncodeToJPG()));

            wwwForm.AddField("type", "base64");
            www = UnityWebRequest.Post("https://api.imgur.com/3/image.xml", wwwForm);
            www.SetRequestHeader("AUTHORIZATION", "Client-ID " + Instance.ClientID);



            yield return www.SendWebRequest();



            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Data: " + www.downloadHandler.text);

                XDocument xDoc = XDocument.Parse(www.downloadHandler.text);


                //成功判定を取る
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(new StringReader(www.downloadHandler.text));
                XmlNodeList getInfos = xmlDoc.GetElementsByTagName("data");
                var resultValue = getInfos[0].Attributes["status"].Value;
                Debug.Log(getInfos[0].Attributes["status"].Value);


                //

                if (resultValue != "200")
                {
                    useClientIndex += 1;
                    Debug.Log(useClientIndex);

                    if (useClientIndex == clientID.Length)
                    {
                        Debug.Log("Error! API is Over Request");
                        yield break;
                    }
                    else
                    {
                        StartCoroutine(TweetWithScreenShot(text));
                    }

                    yield break;
                }

                //Twitterカードように拡張子を外す

                string url = xDoc.Element("data").Element("link").Value;

                url = url.Remove(url.Length - 4, 4);

                UploadedURL = url;
            }



            text += " " + UploadedURL;

            string hashtags = "&hashtags=";
            if (sinstance.hashTags.Length > 0)
            {
                hashtags += string.Join(",", sinstance.hashTags);
            }

            // ツイッター投稿用URL
            string TweetURL = "http://twitter.com/intent/tweet?text=" + text + hashtags;



#if UNITY_WEBGL && !UNITY_EDITOR

            Application.ExternalEval(string.Format("window.open('{0}','_blank')", TweetURL));

#elif UNITY_EDITOR

            System.Diagnostics.Process.Start(TweetURL);

#else

            Application.OpenURL(TweetURL);

#endif

        }

    }

}