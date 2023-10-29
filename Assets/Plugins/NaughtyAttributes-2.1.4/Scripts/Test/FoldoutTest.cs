using UnityEngine;

namespace NaughtyAttributes.Test
{
    public class FoldoutTest : MonoBehaviour
    {
        [Foldout("Integers")]
        public int int0;
        [Foldout("Integers")]
        public int int1;

          public int int2;
    }
}
