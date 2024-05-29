using JuiceIt2Content.Programming.Player.Scripts;
using UnityEngine;

namespace JuiceIt2Content.Programming.FX
{
    public class CollectFX : MonoBehaviour
    {
        private ParticleSystem _ps;
        private void Start()
        {
            _ps.trigger.SetCollider(0, FindFirstObjectByType<PlayerEngine>().GetComponents<SphereCollider>()[0]);
        }
    }
}
