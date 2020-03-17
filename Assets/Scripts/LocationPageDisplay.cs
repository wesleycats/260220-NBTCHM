using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationPageDisplay : MonoBehaviour
{
    //temp public
    public LocationPageData locationPageData;

    [SerializeField] private Text locationName;
    [SerializeField] private List<Image> locationPictures;

    [SerializeField] private GameObject locationReview;

    [SerializeField] private Text locationInformation;

    //set right data
    public void SetLocationData(LocationPageData _locationPageData)
    {
        locationPageData = _locationPageData;
    }

    //update the location page with the right information
    public void UpdatePage()
    {
        locationName.text = locationPageData.LocationName;
        
        for(int i = 0; i < locationPictures.Count; i++)
        {
            locationPictures[i].sprite = locationPageData.Pictures[i];
        }

        locationInformation.text = locationPageData.Information;
    }

    //temp start for testing
    private void Start()
    {
        UpdatePage();
    }
}
