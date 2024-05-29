using UnityEngine;

namespace JuiceIt2Content.Programming.FX
{
    public class VFXDespawn : MonoBehaviour
    {
        private void Awake()
        {
            Destroy(gameObject, 10);
        }
    }
}
