using System;
using System.Reflection;
using UnityEngine;

namespace Ametrin.KunstBLL{

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class SetableAttribute : PropertyAttribute{
        public string MethodName; //because stupid .net 4 attributes

        public SetableAttribute(string methodName){
            MethodName = methodName;
        }
    }
}
