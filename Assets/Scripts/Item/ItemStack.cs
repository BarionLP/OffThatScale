using System;
using UnityEngine;

namespace Ametrin.KunstBLL.Items{
    [Serializable]
    public sealed class ItemStack{
        public bool IsEmpty => Type == null || Object == null;
        [field: SerializeField] public Item Type { get; private set; }
        [field: SerializeField] public GameObject Object { get; private set; }

        public ItemStack(Item type, GameObject @object){
            Type = type;
            Object = @object;
        }

        public static ItemStack Empty() => new();
        private ItemStack() {}
    }
}