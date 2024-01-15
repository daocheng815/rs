using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

public class SevenBagTetriminoSelector
{
    private List<int> bag;
    private Queue<int> nextBag;
    private Random random;
    private List<int> nextBags;

    public List<int> Bag
    {
        get => nextBags;
        set => nextBags = value;
    }

    public SevenBagTetriminoSelector()
    {
        bag = new List<int> { 0, 1, 2, 3, 4, 5, 6 };
        random = new Random();
        ShuffleBag();
        RefillNextBag();
        nextBags = new List<int>();
        InitializeUI();
    }

    private void InitializeUI()
    {
        for (int i = 0; i < 7; i++)
        {
            nextBags.Add(GetNextTetriminoIndex());
        }
    }
    //我使用這個讀取下個方塊
    public int GetNext()
    {
        var next = nextBags[0];
        nextBags.RemoveAt(0);
        nextBags.Add(GetNextTetriminoIndex());
        return next;
    }
    private void ShuffleBag()
    {
        int n = bag.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(0, n + 1);
            (bag[k], bag[n]) = (bag[n], bag[k]);
        }
    }

    private void RefillNextBag()
    {
        nextBag = new Queue<int>(bag.OrderBy(x => random.Next()));
    }

    public int GetNextTetriminoIndex()
    {
        if (nextBag.Count == 0)
        {
            ShuffleBag();
            RefillNextBag();
        }

        return nextBag.Dequeue();
    }
}