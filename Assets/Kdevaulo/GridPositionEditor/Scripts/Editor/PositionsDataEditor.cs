using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using UnityEngine.Assertions;

namespace Kdevaulo.GridPositionEditor
{
    [CustomEditor(typeof(PositionsData))]
    public class PositionsDataEditor : Editor
    {
        private SerializedProperty _arrayWidth;
        private SerializedProperty _arrayHeight;
        private SerializedProperty _positionsArray;

        private List<Vector2Int> _selectedCells;

        private const float FixedMargin = 35f;

        private readonly Color SelectedColor = new Color(0.067f, 0.067f, 0.067f);
        private readonly Color NormalColor = new Color(0.4f, 0.4f, 0.4f);

        private Vector2 _scrollPosition;

        private PositionsData _positionsData;

        public override void OnInspectorGUI()
        {
            _arrayWidth = serializedObject.FindProperty("PointsXCount");
            _arrayHeight = serializedObject.FindProperty("PointsYCount");

            EditorGUILayout.PropertyField(_arrayWidth);
            EditorGUILayout.PropertyField(_arrayHeight);

            _positionsData = (PositionsData) target;
            Assert.IsNotNull(_positionsData);

            if (_positionsData.SelectedCells == null)
            {
                _positionsData.SelectedCells = new List<Vector2Int>();
            }

            _selectedCells = _positionsData.SelectedCells;

            if (GUILayout.Button("Print Selected Cells"))
            {
                PrintCells();
            }

            if (GUILayout.Button("Clear Cells"))
            {
                ClearCells();
            }

            float totalWidth = EditorGUIUtility.currentViewWidth - FixedMargin;
            float buttonSize = totalWidth / _arrayWidth.intValue - EditorGUIUtility.standardVerticalSpacing * 1.5f;

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUIStyle.none,
                GUIStyle.none);

            for (int y = _arrayHeight.intValue - 1; y >= 0; y--)
            {
                EditorGUILayout.BeginHorizontal();

                for (int x = 0; x < _arrayWidth.intValue; x++)
                {
                    bool isSelected = _selectedCells.Contains(new Vector2Int(x, y));
                    Color cellColor = isSelected ? SelectedColor : NormalColor;

                    if (GUILayout.Button("", EditorStyles.label, GUILayout.Width(buttonSize),
                            GUILayout.Height(buttonSize)))
                    {
                        if (isSelected)
                        {
                            _selectedCells.Remove(new Vector2Int(x, y));
                        }
                        else
                        {
                            _selectedCells.Add(new Vector2Int(x, y));
                        }
                    }

                    EditorGUI.DrawRect(GUILayoutUtility.GetLastRect(), cellColor);
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();

            EditorUtility.SetDirty(target);
            serializedObject.ApplyModifiedProperties();
        }

        private void PrintCells()
        {
            Debug.Log(nameof(PrintCells));
            Debug.Log($"Selected cells count = {_positionsData.SelectedCells.Count}");

            foreach (Vector2Int cell in _positionsData.SelectedCells)
            {
                Debug.Log("Selected cell at index: (" + cell.x + ", " + cell.y + ")");
            }
        }

        private void ClearCells()
        {
            Debug.Log(nameof(ClearCells));

            _selectedCells.Clear();
            _positionsData.SelectedCells.Clear();
        }
    }
}