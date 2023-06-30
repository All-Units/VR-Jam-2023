using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

public static class CustomContextMenu
{
    [MenuItem("GameObject/Set Parent Local Position %#o", false, 20)]
    private static void SetParentLocalPosition()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects.Length == 0)
        {
            Debug.LogWarning("No objects selected.");
            return;
        }

        GameObject parentObject = selectedObjects[0].transform.parent.gameObject;
        Vector3 parentLocalPosition = selectedObjects[0].transform.localPosition;

        Undo.RecordObject(parentObject.transform, "Set Parent Local Position");

        // Set parent object's local position to selected object's local position
        parentObject.transform.localPosition = parentLocalPosition;

        // Set selected object's local position to zero
        foreach (GameObject selectedObject in selectedObjects)
        {
            Undo.RecordObject(selectedObject.transform, "Set Local Position");
            selectedObject.transform.localPosition = Vector3.zero;
        }
    }

    // Validate the menu item to ensure it only appears when an object is selected
    [MenuItem("GameObject/Set Parent Local Position", true)]
    private static bool ValidateSetParentLocalPosition()
    {
        return Selection.activeGameObject != null;
    }
}

#endif