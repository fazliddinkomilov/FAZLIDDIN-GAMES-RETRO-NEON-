using UnityEditor;
using UnityEngine;

//Simply re-styles a gameObject name in the Hiearchy window to be black and all caps.
//Allows us to seperate our gameObjects and not lose our minds.

namespace MegaSkill.Editor
{
    [InitializeOnLoad]

    public static class HierarchySectionHeader
    {
        static HierarchySectionHeader()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
        }

        static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (gameObject != null && gameObject.name.StartsWith("//", System.StringComparison.Ordinal))
            {
                EditorGUI.DrawRect(selectionRect, new Color(0.15f,0.15f,0.15f));
                EditorGUI.DropShadowLabel(selectionRect, gameObject.name.Replace("/", "").ToUpperInvariant());
            }
        }
    }
}