using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ametrin.KunstBLL.EditorTools{

    [CustomPropertyDrawer(typeof(SetableAttribute))]
    public sealed class SetablePropertyDrawer : PropertyDrawer{
        // public override VisualElement CreatePropertyGUI(SerializedProperty property){
        //     var methodName = (attribute as SetableAttribute).MethodName;
        //     var methods = property.serializedObject.targetObjects.Select(obj =>{
        //         var method = obj.GetType().GetMethod(methodName);
        //         return new Action(() => method.Invoke(obj, Array.Empty<object>()));
        //     }).ToArray();


        //     var root = new VisualElement();
        //     root.style.flexDirection = FlexDirection.Row;

        //     var setButton = new Button{
        //         text = "Set"
        //     };
        //     setButton.clicked += ()=>{
        //         foreach(var method in methods){
        //             method?.Invoke();
        //         }
        //     };

        //     var valueField = new PropertyField(property);
        //     valueField.style.flexGrow = 1;
        //     root.Add(valueField);
        //     root.Add(setButton);
        //     return root;
        // }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label){
            const int BUTTON_WIDTH = 40;
            var propRect = new Rect(position.x, position.y, position.width - BUTTON_WIDTH - 2, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(propRect, property, label, true);

            var buttonRect = new Rect(position.x + position.width - BUTTON_WIDTH, position.y, BUTTON_WIDTH, EditorGUIUtility.singleLineHeight);
            if(UnityEngine.GUI.Button(buttonRect, "Set")){
                var methodName = (attribute as SetableAttribute).MethodName;
                foreach(var obj in property.serializedObject.targetObjects){
                    obj.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Invoke(obj, Array.Empty<object>());
                }
            }
        }
    }
}
