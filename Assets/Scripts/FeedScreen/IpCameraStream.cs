using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.IO;
using Mission;
using Mission.Lifecycle;
using Networking;
using UnityEngine.UI;

public class IpCameraStream : MonoBehaviour {

    public string sourceUrl { get; set; }   // Url of stream
    public Sprite Frame;    //Mesh for displaying video
    public int WaitTime { get; set; }   // Time to wait between frames
    public Texture2D DefaultTexture;    // Default texture to display while stream is offline.

    private Texture2D _texture;
    private Stream _stream;

    private Rect rect = new Rect(0, 0, 640, 480);
    private Vector2 vect = new Vector2(0, 0);

    private Coroutine _streamCoroutine;

    void OnEnable()
    {
        
    }

    public void PlayLiveFeed()
    {
        // Set up stream
        _texture = new Texture2D(2, 2);

        ServicePointManager.Expect100Continue = false;
        ServicePointManager.DefaultConnectionLimit = 4;
        // create HTTP request
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(sourceUrl);

        req.Method = "GET";
        req.Proxy = GlobalProxySelection.GetEmptyWebProxy();

        // get response
        WebResponse resp = req.GetResponse();
        // get response stream
        _stream = resp.GetResponseStream();

        _streamCoroutine = StartCoroutine(GetFrame());
    }

    public void PauseLiveFeed()
    {
        StopCoroutine(_streamCoroutine);
    }

    public void StopLiveFeed()
    {
        if (_streamCoroutine == null) return;
        StopCoroutine(_streamCoroutine);
        GetComponent<Image>().sprite = Sprite.Create(DefaultTexture, rect, vect);

        if (_stream == null) return;
        _stream.Close();
    }

    IEnumerator GetFrame() {
        Byte[] JpegData = new Byte[65536];

        while (true) {
            int bytesToRead = FindLength(_stream);
            print(bytesToRead);
            if (bytesToRead == -1) {
                print("End of stream");
                yield break;
            }

            int leftToRead = bytesToRead;

            while (leftToRead > 0) {
                leftToRead -= _stream.Read(JpegData, bytesToRead - leftToRead, leftToRead);
                yield return null;
            }

            MemoryStream ms = new MemoryStream(JpegData, 0, bytesToRead, false, true);

            _texture.LoadImage(ms.GetBuffer());
            GetComponent<Image>().sprite = Sprite.Create(_texture, rect, vect);
            _stream.ReadByte(); // CR after bytes
            _stream.ReadByte(); // LF after bytes

            yield return new WaitForSeconds(WaitTime);
        }
    }

    int FindLength(Stream stream) {
        int b;
        string line = "";
        int result = -1;
        bool atEOL = false;

        while ((b = stream.ReadByte()) != -1) {
            if (b == 10) continue; // ignore LF char
            if (b == 13) { // CR
                if (atEOL) {  // two blank lines means end of header
                    stream.ReadByte(); // eat last LF
                    return result;
                }
                if (line.StartsWith("Content-Length:")) {
                    result = Convert.ToInt32(line.Substring("Content-Length:".Length).Trim());
                } else {
                    line = "";
                }
                atEOL = true;
            } else {
                atEOL = false;
                line += (char)b;
            }
        }
        return -1;
    }
}