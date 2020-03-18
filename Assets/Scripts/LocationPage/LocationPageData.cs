using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "LocationData", menuName = "Data", order = 1)]
public class LocationPageData : ScriptableObject
{
    public string LocationName;

    public List<Sprite> Pictures;

    public List<Review> Review;
    [TextArea(4, 3)]
    public string Information;
}

[System.Serializable]
public struct Review
{
    public string reviewName;
    public int reviewStars;
    [TextArea(4, 3)]
    public string reviewComment;
}