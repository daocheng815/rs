using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiMagner : Singleton<UiMagner>
{
    public Text scoreText;
    public GameObject[] Big7;
    public Transform bi7S;
    private readonly GameObject[] _big6 = new GameObject[6];
    public void ReSetBig7(List<int> list)
    {
        foreach (var v in _big6)
        {
            if(v != null)
                Destroy(v);
        }
        for (int i = 5; i >= 0; i--)
        {
            var n = list[i + 1];
            var v3 = bi7S.transform.position;
            _big6[i] = Instantiate(Big7[n], v3 + new Vector3(0, (-i * 2.5f) , 0), Quaternion.identity);
            _big6[i].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            _big6[i].transform.SetParent(bi7S);
        }
    }
    public void ReSetScoreText(int s)
    {
        scoreText.text = s.ToString();
    }
}
