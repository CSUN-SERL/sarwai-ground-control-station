// GetImage.cs is responsible for grabing a tecture form ROSbridge's 
// video_websocket_server. The tecture corresponds to the live feed camera
// that is attached to the Husky bot in Gazebo. 
// After capturing the image, the image is displayed onto a Gameobject in Unity,
// for visability. 
// The image is refreshed at every frame providing an updated new image, thus giving
// a live video stream of the cameras on the Gazebo Husky bot.

using System.Collections;
using Networking;
using UnityEngine;
using UnityEngine.UI;

namespace LiveFeedScreen.E2SHPackage_Scripts
{
    [RequireComponent(typeof(RawImage))]
    public class GetImage : MonoBehaviour
    {
        private Coroutine _streamCoroutine;
        private RawImage rawImage;

        public int RobotId;

        private Texture2D tex;

        // At start of program the first image is captured.
        private void OnEnable()
        {
            // Initialize new tex var to hold image.
            tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
            // Calls the funtion GetTexture to grab the Husky image.
            rawImage = GetComponent<RawImage>();
            _streamCoroutine = StartCoroutine(GetTexture());
        }

        private void OnDisable()
        {
            StopCoroutine(_streamCoroutine);
            _streamCoroutine = null;
        }


        // Handles getting. 
        private IEnumerator GetTexture()
        {
            //TODO Fix memory leak.
            while (true)
            {
                // Instantiate a Texture2D, and gives the 
                // dimensions and format of the texture.
                // Don't do this here because it fucks the computers memory by allocating space for the new texture over and over :D
                //var tex = new Texture2D(4, 4, TextureFormat.DXT1, false); 

                // A WWW instance is given access to a website, in this case the link to the ROSbridge 
                // video_websocket_server image.
                var www = new WWW(ServerURL.GetRobotLiveStream(RobotId));
                //Debug.Log(ServerURL.GetRobotLiveStream(RobotId));

                // Pause/wait for the site to respond with the image
                yield return www;


                // Check shit
                if (www.error != null)
                {
                    //Debug.Log(www.error);
                    continue;
                }

                if (tex == null) continue;

                //Loads the ROSbridge video_websocket_server husky image into the Textture2D 'tex'
                www.LoadImageIntoTexture(tex);

                www.Dispose();
                // Grabs the image loaded in 'tex' and renders it for visualization in the Unity GameObject that 
                // the script is attached to. (allows for visualization)

                rawImage.texture = tex;
            }
        }
    }
}