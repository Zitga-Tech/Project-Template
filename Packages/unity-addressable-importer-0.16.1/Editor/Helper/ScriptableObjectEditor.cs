/// <summary>
/// ScriptableObjectEditor
/// </summary>
namespace UnityAddressableImporter.Helper
{
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEditor;

    [CanEditMultipleObjects]
    public class ScriptableObjectEditor<Type> : Editor
        where Type : UnityEngine.Object
    {
        private List<MethodInfo> _methods;
        private Type _target;

        private void OnEnable()
        {
            _target = target as Type;
            if (_target == null) return;

            _methods = AddressableImporterMethodHandler.CollectValidMembers(_target.GetType());
        }

        public override void OnInspectorGUI()
        {

            if (DrawWithOdin() == false)
            {
                DrawBaseEditor();
            }

            DrawCommands();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawBaseEditor()
        {
            base.OnInspectorGUI();
        }

        private void DrawCommands()
        {
            if (_methods == null) return;
            AddressableImporterMethodHandler.OnInspectorGUI(_target, _methods);
        }

        private bool DrawWithOdin()
        {
            return false;
        }
    }
}