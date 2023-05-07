using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    # region Singleton

    private static Transform instance;
    public static Transform Instance { get {
         if ( instance == null ) {
             instance = FindObjectOfType<PlayerManager>().transform;
              } 
              return instance; }}

    # endregion


}
