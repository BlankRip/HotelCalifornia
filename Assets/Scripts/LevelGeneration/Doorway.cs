using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    public class Doorway : MonoBehaviour
    {
        private void OnDrawGizmos() {
            Ray ray = new Ray(transform.position, transform.forward);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(ray);
        }
    }
}
