using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailManager : MonoBehaviour
{

    # region Singleton

    private static TrailManager instance;
    public static TrailManager Instance { get {
         if ( instance == null ) {
             instance = FindObjectOfType<TrailManager>();
              } 
              return instance; }}

    # endregion

    [SerializeField] private int maxTrailCount;
    [SerializeField] private GameObject trailPrefab;

    private List<TrailRenderer> trails;

    void Awake()
    {
        trails = new List<TrailRenderer>();
        for(int i = 0; i < maxTrailCount; i++)
        {
            var trailObj = Instantiate(trailPrefab);
            trails.Add(trailObj.GetComponent<TrailRenderer>());
            trailObj.SetActive(false);
        }
    }

    public void CreateTrail(Vector3 start, Vector3 end, Color color)
    {
        var trail = trails.Find(x => !x.gameObject.activeSelf);
        trail.Clear();
        trail.transform.position = start;
        trail.gameObject.SetActive(true);
        trail.startColor = color;
        trail.endColor = color;
        trail.AddPosition(start);
        trail.AddPosition(end);
    }
}
