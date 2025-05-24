using UnityEngine;

namespace SharedScripts
{
    public static class GameObjectHelpers
    {
        public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag) where T : Component
        {
            var t = parent.transform;
            foreach (Transform tr in t)
            {
                if (tr.CompareTag(tag))
                {
                    return tr.GetComponent<T>();
                }
            }

            return null;
        }
    }
}