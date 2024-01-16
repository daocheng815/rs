using UnityEngine;
public class GhostPiece : TetrisBase
{
    private bool _upDatePos;

    private void Start()
    {
        UpDateGhostPiece(GetPos(),transform.rotation);
    }

    public void UpDateGhostPiece(Vector3 pos, Quaternion rot)
    {
        SetPos(pos);
        transform.rotation = rot;
        _upDatePos = false;
        while (!_upDatePos)
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                _upDatePos = true;
            }
        }
    }
}
