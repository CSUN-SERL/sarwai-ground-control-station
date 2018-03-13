// SpriteMaker.cs creates our occupancy grid aka MAP.
// As of now this is done with mock data.

using UnityEngine;

namespace LiveFeedScreen.E2SHPackage_Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteMaker : MonoBehaviour
    {
        private SpriteRenderer _rend;

        // Use this for initialization.
        private void Start()
        {
            // Declare sizes.
            const int width = 4000;
            const int height = 4000;
            const int oneDsize = 16000000;

            // Declare arrays.
            var oneDarr = new int[oneDsize];
            var twoDarr = new int[width, height];

            // Fill in 1Darray.
            for (var i = 0; i < oneDsize; i++)
                if (i < 5000000)
                    oneDarr[i] = 0;
                else if (i >= 5000000 && i < 10000000)
                    oneDarr[i] = 1;
                else if (i >= 10000000)
                    oneDarr[i] = 100;

            // Fill in 2Darray with 1Darray values.
            for (int x = 0, i = 0; x < height; x++)
            for (var y = 0; y < width; y++)
            {
                if (y < 300)
                    twoDarr[x, y] = oneDarr[i];
                else if (y >= 300 && y < 800)
                    twoDarr[x, y] = oneDarr[i];
                else if (y >= 800)
                    twoDarr[x, y] = oneDarr[i];
                i++;
            }

            // Initialize rend to the SpriteRenderer componenet.
            _rend = GetComponent<SpriteRenderer>();

            // Create a texture witht the 2Darray values.

            // Instantiate a Texture2D with appropriate dimensions.
            var tex = new Texture2D(width, height);

            //Instantiate a Color[] with appropriate dimensions
            var colorArray = new Color[tex.width * tex.height];

            // Itterate throught the 2Darray and instantiate the corresponding colors to the vaules in the 2DArray,
            // then, place those corresponding colors in the Color[] that we instantiated above.
            for (var x = 0; x < tex.width; x++)
            for (var y = 0; y < tex.height; y++)
                if (twoDarr[x, y] == 100)
                    colorArray[x + y * tex.width] = Color.black;
                else if (twoDarr[x, y] == 1)
                    colorArray[x + y * tex.width] = Color.gray;
                else if (twoDarr[x, y] == 0)
                    colorArray[x + y * tex.width] = Color.white;

            // Sets the vaules from the color array to pair up with pixels in the texture2d, tex.
            tex.SetPixels(colorArray);

            // Applies the pixels data to the texture.
            tex.Apply();

            // Creates a sprtie from texture.
            var newSprite = Sprite.Create(tex,
                new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);

            // Assign our procedural sprite to rend.sprite.
            _rend.sprite = newSprite;
        }
    }
}