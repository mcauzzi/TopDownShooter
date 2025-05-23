using UnityEngine;

namespace Backgrounds
{
    public class SpriteScroller : MonoBehaviour
    {
        [SerializeField] private Vector2  scrollSpeed = new Vector2(0.5f, 0.5f);
        private                  Vector2  _offset;
        private                  Material _material;
        void Awake()
        {
            _material= GetComponent<SpriteRenderer>().material;
        }

        void Update()
        {
            _material.mainTextureOffset+= scrollSpeed * Time.deltaTime;
        }
    }
}
