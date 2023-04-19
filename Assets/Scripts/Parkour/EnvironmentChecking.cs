using UnityEngine;

public class EnvironmentChecking : MonoBehaviour
{
   [SerializeField] private Vector3 rayOffset;
   [SerializeField] private float rayLength;
   [SerializeField] private float heightRayLength;
   [SerializeField] private LayerMask obstacleLayer;
   private float _heightDifference;
   public ObstacleInfo CheckObstacle()
   {
      var hitData = new ObstacleInfo();
      var rayOrigin = transform.position + rayOffset;
      hitData.HitFound = Physics.Raycast(rayOrigin, transform.forward, out hitData.HitInfo, rayLength, obstacleLayer);
      if (hitData.HitFound)
      {
         var heightOrigin = hitData.HitInfo.point + Vector3.up * heightRayLength;
         hitData.HeightHitFound = Physics.Raycast(heightOrigin, Vector3.down, out hitData.HeightInfo, heightRayLength, obstacleLayer);
         Debug.DrawRay(heightOrigin, Vector3.down*heightRayLength, hitData.HeightHitFound ? Color.blue : Color.red);
      }
      Debug.DrawRay(rayOrigin, transform.forward*rayLength, hitData.HitFound ? Color.blue : Color.red);
      return hitData;
   }
   public float GetRayLength()
   {
      return rayLength;
   }
   public struct ObstacleInfo
   {
      public bool HitFound;
      public bool HeightHitFound;
      public RaycastHit HitInfo;
      public RaycastHit HeightInfo;
   }
}