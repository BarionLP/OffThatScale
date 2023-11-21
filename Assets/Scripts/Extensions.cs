using Ametrin.Utils;
using UnityEngine;

namespace Ametrin.KunstBLL{
    public static class UnityExtensions{
        public static Result<T> TryGetComponent<T>(this GameObject gameObject){
            if(gameObject.TryGetComponent(out T result)){
                return result;
            }
            return ResultStatus.Null;
        }

        public static Result<T> TryGetComponent<T>(this Component component){
            return component.gameObject.TryGetComponent<T>();
        }
    }
}
