// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Collectible : MonoBehaviour
    {
        public void OnTriggerStay(Collider other)
        {
            var collector = other.GetComponent<ICollector>();
            if (collector != null)
            {
                if (collector.IsCollecting == true)
                {
                    transform.position = Vector3.Lerp(transform.position, other.transform.position, 3f * Time.deltaTime);
                    if (Vector3.Distance(transform.position, other.transform.position) < 0.5f)
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
            else
                return;
        }
    }
}
