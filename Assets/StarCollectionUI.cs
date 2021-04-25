using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollectionUI : MonoBehaviour
{
    public List<StarUI> stars;

    public void FillNextStar()
    {
        foreach (StarUI star in stars)
        {
            if (!star.filled)
            {
                star.Fill();
                return;
            }
        }
    }
}
