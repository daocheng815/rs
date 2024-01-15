using UnityEngine;

// ReSharper disable Unity.InefficientMultidimensionalArrayUsage
// ReSharper disable Unity.PerformanceCriticalCodeInvocation

public class Tetrimino : TetrisBase
{
    
    private float _pSpeed;//方塊時間
    public float tSpeed;//下墜時間
    
    public Vector3 rotatePos;
    
    private bool _isFastDrop;
    const float LockDelayTime = 0.5f;
    private float _lockDelayTime = 0f;
    
    public Transform g;

    private void Update()
    {
        if(Pause)
            return;
        
        switch (TState.State)
        {
            case TStates.Fall:
                Fall();
                break;
            case TStates.LockDelay:
                LockDelay();
                break;
            case TStates.FallSuccess:
                FallSuccess();
                break;
        }
        
        g.position = transform.TransformPoint(rotatePos);
    }

    private void Fall()
    {
        //下墜
        IsFall(true);
        //左右選轉操作
        MoveLr(1, Key.Right);
        MoveLr(-1,Key.Left);
        RotateT(90, Key.RotateR1, Key.RotateR2);
        RotateT(-90, Key.RotateL1,Key.RotateL2);
    }

    private void LockDelay()
    {
        _lockDelayTime += Time.deltaTime;
        //下墜
        IsFall(false);
        if (MoveLr(1, Key.Right)
            ||MoveLr(-1,Key.Left)
            ||RotateT(90, Key.RotateR1, Key.RotateR2)
            ||RotateT(-90, Key.RotateL1,Key.RotateL2))
        {
            _lockDelayTime = 0;
            TState.Change(TStates.Fall);
        }
        if (_lockDelayTime >= LockDelayTime || Input.GetKey(Key.HardDrop1) || Input.GetKey(Key.HardDrop2) || Input.GetKey(Key.HardDrop3))
        {
            _lockDelayTime = 0;
            TState.Change(TStates.FallSuccess);
        }
    }
    private void FallSuccess()
    {
        AddToGrid();
        CheckForLines();
        GameManger.Instance.AddScore(20);
        Tevent.NextT.Invoke();
        TState.Change(TStates.Fall);
        enabled = false;
    }
    private void IsFall(bool l)
    {
        if (Input.GetKeyDown(Key.FastDrop))
            _isFastDrop = true;
        float dropSpeed = Input.GetKey(Key.HardDrop1) || Input.GetKey(Key.HardDrop2) || Input.GetKey(Key.HardDrop3) ? tSpeed / 10 : _isFastDrop ? 0 : tSpeed;
        if (_isFastDrop)
            dropSpeed = 0;
        if (Time.time - _pSpeed > dropSpeed)
        { 
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                if (_isFastDrop)
                {
                    TState.Change(TStates.FallSuccess);
                }
                //進入硬直
                if(l)
                    TState.Change(TStates.LockDelay);
                if (!CheckBorund())
                {
                    TState.Change(TStates.GameOver);
                }
            }
            _pSpeed = Time.time;
        }
    }
    private bool MoveLr(int distance, KeyCode k1)
    {
        
        if (Input.GetKeyDown(k1))
        {
            AudioManger.Instance.PlaySound(0);
            transform.position += new Vector3(distance, 0, 0);
            Spawan.MyGhostPiece.UpDateGhostPiece(transform.position,transform.rotation);
            if (!ValidMove())
            {
                transform.position -= new Vector3(distance, 0, 0);
                Spawan.MyGhostPiece.UpDateGhostPiece(transform.position,transform.rotation);
                return false;
            }
            return true;
        }
        return false;
    }
    private bool RotateT(int rotate,KeyCode k1, KeyCode k2)
    {
        if (Input.GetKeyDown(k1)||Input.GetKeyDown(k2))
        {
            AudioManger.Instance.PlaySound(0);
            transform.RotateAround(transform.TransformPoint(rotatePos),new Vector3(0,0,1),rotate);
            Spawan.MyGhostPiece.UpDateGhostPiece(transform.position,transform.rotation);
            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotatePos),new Vector3(0,0,1),-rotate);
                Spawan.MyGhostPiece.UpDateGhostPiece(transform.position,transform.rotation);
                return false;
            }
            return true;
        }
        return false;
    }
    
    private bool CheckBorund()
    {
        foreach (Transform c in transform)
        {
            var vector3 = c.transform.position;
            var roundX = Mathf.FloorToInt(vector3.x);
            var roundY = Mathf.FloorToInt(vector3.y);
            //判斷位置是否在範圍內
            if (roundX < 0 || roundX >= W || roundY < 0 || roundY >= H)
            {
                return false;
            }
        }
        return true;
    }
    private void AddToGrid()
    {
        foreach (Transform c in transform)
        {
            var vector3 = c.transform.position;
            var roundX = Mathf.FloorToInt(vector3.x);
            var roundY = Mathf.FloorToInt(vector3.y);
            //判斷位置是否在範圍內
            if (roundX < 0 || roundX >= W || roundY < 0 || roundY >= H)
            {
                return;
            }
            Grid[roundX, roundY] = c;
        }
    }
    
    private void CheckForLines()
    {
        var num = 0;
        for (int i = H - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                num++;
                GameManger.Instance.AddScore(200 *num);
                DeleteLine(i);
                RowDown(i);
            }
        }

        AudioManger.Instance.PlaySound(0);
    }
    
    private void DeleteLine(int i)
    {
        for (int j = 0; j < W; j++)
        {
            Destroy(Grid[j,i].gameObject);
            Grid[j, i] = null;
        } 
    }

    private void RowDown(int i)
    {
        for (int y = i; y < H; y++)
        {
            for (int j = 0; j < W; j++)
            {
                if (Grid[j, y] != null)
                {
                    Grid[j, y - 1] = Grid[j, y];
                    Grid[j, y] = null;
                    Grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }
    
    private bool HasLine(int i)
    {
        for (int j = 0; j < W; j++)
        {
            if (Grid[j, i] == null)
                return false;
        }
        return true;
    }
    
}
