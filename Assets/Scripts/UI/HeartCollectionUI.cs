using System.Collections.Generic;
using UnityEngine;

public class HeartCollectionUI : MonoBehaviour
{
    public List<Heart> hearts;

    public int PopHeart()
    {
        int count = CountUpHearts();
        if (count > 0)
            hearts[count - 1].Down();
        return CountUpHearts();
    }
    
    public int AddHeart()
    {
        int count = CountUpHearts();
        if (count < hearts.Count)
            hearts[count].Up();
        return CountUpHearts();
    }

    private int CountUpHearts()
    {        
        int count = 0;
        foreach (Heart heart in hearts)
        {
            if (heart.up)
                count ++;
        }
        return count;
    }
}
