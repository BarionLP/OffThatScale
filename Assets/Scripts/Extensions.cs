using Ametrin.Utils;
using UnityEngine;

namespace Ametrin.KunstBLL{
    public static class UnityExtensions{
        public static Result<T> TryGetComponent<T>(this GameObject gameObject) where T : Component{
            if(gameObject.TryGetComponent(out T result)){
                return result;
            }
            return ResultStatus.Null;
        }

        public static Result<T> TryGetComponent<T>(this Component component) where T : Component{
            return component.gameObject.TryGetComponent<T>();
        }
    }
}
