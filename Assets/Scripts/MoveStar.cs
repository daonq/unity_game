using UnityEngine;
using DG.Tweening;

public class MoveStar : MonoBehaviour
{
    public GameObject posLevel;
    public void Move()
    {
        transform.DOLocalMove(posLevel.transform.position, 1);
    }
}
