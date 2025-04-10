using UnityEngine;

namespace BricksBattle.Behaviors
{
    public class Brick : MonoBehaviour
    {
        [SerializeField]
        private GameObject destroyEffectPrefab;

        [field: SerializeField]
        public int Score { get; private set; } = 100;

        private bool initialized = false;
        private BricksContainer container = null;

        public void Init(BricksContainer container)
        {
            this.container = container;
            initialized = true;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag(Tags.Ball))
            {
                if (!initialized)
                {
                    Debug.LogWarning("Uninitialized brick hit!");
                    return;
                }

                //todo надо или через пул объектов, или повесить эффект на префаб самого кирпича
                GameObject effect = Instantiate(destroyEffectPrefab, 
                    transform.position, Quaternion.identity);
                Destroy(effect, 1f);

                container.DestroyBrick(this);
                gameObject.SetActive(false);
            }
        }
    }
}
