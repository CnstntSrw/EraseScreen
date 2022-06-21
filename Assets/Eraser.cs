using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    [SerializeField]
    private int eraseSize;

    private Texture2D _Texture;
    private Color[] _Colors;
    private RaycastHit2D _Hit;
    private SpriteRenderer _SpriteRend;
    private Color _ZeroAlpha = Color.clear;
    private Vector2Int _LastPos;
    private bool _Erasing = false;
    void Start()
    {
        _SpriteRend = gameObject.GetComponent<SpriteRenderer>();
        var tex = _SpriteRend.sprite.texture;
        _Texture = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
        _Texture.filterMode = FilterMode.Bilinear;
        _Texture.wrapMode = TextureWrapMode.Clamp;
        _Colors = tex.GetPixels();
        _Texture.SetPixels(_Colors);
        _Texture.Apply();
        _SpriteRend.sprite = Sprite.Create(_Texture, _SpriteRend.sprite.rect, new Vector2(0.5f, 0.5f));
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _Hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (_Hit.collider != null)
            {
                Erase();
                _Erasing = true;
            }
        }
        else
            _Erasing = false;
    }

    public void Erase()
    {
        int w = _Texture.width;
        int h = _Texture.height;
        var mousePos = _Hit.point - (Vector2)_Hit.collider.bounds.min;
        mousePos.x *= w / _Hit.collider.bounds.size.x;
        mousePos.y *= h / _Hit.collider.bounds.size.y;
        Vector2Int p = new Vector2Int((int)mousePos.x, (int)mousePos.y);
        Vector2Int start = new Vector2Int();
        Vector2Int end = new Vector2Int();
        if (!_Erasing)
            _LastPos = p;
        start.x = Mathf.Clamp(Mathf.Min(p.x, _LastPos.x) - eraseSize, 0, w);
        start.y = Mathf.Clamp(Mathf.Min(p.y, _LastPos.y) - eraseSize, 0, h);
        end.x = Mathf.Clamp(Mathf.Max(p.x, _LastPos.x) + eraseSize, 0, w);
        end.y = Mathf.Clamp(Mathf.Max(p.y, _LastPos.y) + eraseSize, 0, h);
        Vector2 dir = p - _LastPos;
        for (int x = start.x; x < end.x; x++)
        {
            for (int y = start.y; y < end.y; y++)
            {
                Vector2 pixel = new Vector2(x, y);
                Vector2 linePos = p;
                if (_Erasing)
                {
                    float d = Vector2.Dot(pixel - _LastPos, dir) / dir.sqrMagnitude;
                    d = Mathf.Clamp01(d);
                    linePos = Vector2.Lerp(_LastPos, p, d);
                }
                if ((pixel - linePos).sqrMagnitude <= eraseSize * eraseSize)
                {
                    _Colors[x + y * w] = _ZeroAlpha;
                }
            }
        }
        _LastPos = p;
        _Texture.SetPixels(_Colors);
        _Texture.Apply();
        _SpriteRend.sprite = Sprite.Create(_Texture, _SpriteRend.sprite.rect, new Vector2(0.5f, 0.5f));
    }
}
