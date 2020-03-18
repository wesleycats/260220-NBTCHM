using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviewDisplay : MonoBehaviour
{
    private Review review;

    [SerializeField] private Text reviewName;
    [SerializeField] private Text reviewComment;
    [SerializeField] private List<GameObject> starsGO;

    public void SetReview(Review _review)
    {
        review = _review;
    }

    public void UpdateReview()
    {
        reviewName.text = review.reviewName;
        reviewComment.text = review.reviewComment;

        UnactiveStars();
        SetStarsActive(review.reviewStars);
    }

    private void SetStarsActive(int stars)
    {
        for (int i = 0; stars > i; i++)
        {
            starsGO[i].SetActive(true);
        }
    }

    private void UnactiveStars()
    {
        for (int i = 0; starsGO.Count > i; i++)
        {
            starsGO[i].SetActive(false);
        }
    }
}
