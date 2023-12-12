using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    public RawImage raw;
    public float x, y;

    // Update is called once per frame
    void Update()
    {
        raw.uvRect = new Rect(raw.uvRect.position + new Vector2(x, y) * Time.deltaTime, raw.uvRect.size);
    }
}
