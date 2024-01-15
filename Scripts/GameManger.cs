using System;
using UnityEngine;
public class GameManger:Singleton<GameManger>
{
    public SevenBagTetriminoSelector bigr7 = new SevenBagTetriminoSelector();

    private bool _pause = false;

    public bool Pause
    {
        get => _pause;
        set 
        {
            _pause = value;
            TetrisBase.Pause = value;
        } 
    }
    
    private int _score = 0;
    public int Score
    {
        get => _score;
        set => _score = value;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause = !Pause;
    }

    public void AddScore(int s)
    {
        _score += s;
        UiMagner.Instance.ReSetScoreText(_score);
    }

    public int Next()
    {
        UiMagner.Instance.ReSetBig7(bigr7.Bag);
        return bigr7.GetNext();
    }
}
