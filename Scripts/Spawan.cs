using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawan : MonoBehaviour
{
    [SerializeField]private GameObject[] tetriminoss;
    [SerializeField]private GameObject[] ghostPieces;
    private List<GameObject> _myTetriminos = new List<GameObject>();
    public static GhostPiece MyGhostPiece;

    private float time = 0.5f;
    private float _time;

    private void OnEnable()
    {
        Tevent.NextT += SpawanT;
    }

    private void OnDisable()
    {
        Tevent.NextT -= SpawanT;
    }

    private void Start()
    {
        TState.Change(TStates.Fall);
        AudioManger.Instance.PlayMusic(0);
        SpawanT();
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time >= time)
        {
            _time = 0f;
            CheckPreFabToNull();
        }
    }

    public void CheckPreFabToNull()
    {
        if (_myTetriminos != null)
        {
            for (int i = 0; i < _myTetriminos.Count; i++)
            {
                if (_myTetriminos[i] != null && _myTetriminos[i].transform.childCount == 0)
                {
                    Destroy(_myTetriminos[i]);
                    _myTetriminos[i] = null;
                }
            }
        }
    }
    
    private void SpawanT()
    {
        if (tetriminoss != null && tetriminoss.Length > 0 && !TState.Check(TStates.GameOver))
        {
            if (MyGhostPiece != null)
            {
                Destroy(MyGhostPiece.gameObject);
            }
            var bigr7 = GameManger.Instance.Next();
            var a= Instantiate(tetriminoss[bigr7], transform.position, quaternion.identity);
            a.transform.SetParent(transform);
            int nullIndex = _myTetriminos.FindIndex(item => item == null);
            if (nullIndex != -1)
                _myTetriminos[nullIndex] = a;
            else
                _myTetriminos.Add(a);
            MyGhostPiece = Instantiate(ghostPieces[bigr7], transform.position, quaternion.identity).GetComponent<GhostPiece>();
            MyGhostPiece.transform.SetParent(transform);
            a.GetComponent<Tetrimino>().tSpeed -=  GameManger.Instance.Score/ 0x186A0;
        }
    }
}
