using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationPageDisplay : MonoBehaviour
{
    //temp public
    public LocationPageData LocationPageData;

    [SerializeField] private Text locationName;
    [SerializeField] private Text locationInformation;

    [SerializeField] private List<Image> locationPictures;

    [SerializeField] private List<GameObject> locationReviews;

    //set right data
    public void SetLocationData(LocationPageData _locationPageData)
    {
        LocationPageData = _locationPageData;
    }

    //update the location page with the right information
    public void UpdatePage()
    {
        locationName.text = LocationPageData.LocationName;
        
        for(int i = 0; i < locationPictures.Count; i++)
        {
            locationPictures[i].sprite = LocationPageData.Pictures[i];
        }

        locationInformation.text = LocationPageData.Information;

        for (int i = 0; 3 > i; i++)
        {
            locationReviews[i].GetComponent<ReviewDisplay>().SetReview(LocationPageData.Review[i]);
            locationReviews[i].GetComponent<ReviewDisplay>().UpdateReview();
        }
    }

    //temp start for testing
    private void Start()
    {
        UpdatePage();
    }
}
