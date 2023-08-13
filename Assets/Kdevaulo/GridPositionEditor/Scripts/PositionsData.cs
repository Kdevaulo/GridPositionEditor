using System.Collections.Generic;

using UnityEngine;

namespace Kdevaulo.GridPositionEditor
{
    [CreateAssetMenu(fileName = nameof(PositionsData),
        menuName = nameof(GridPositionEditor) + "/" + nameof(PositionsData))]
    public class PositionsData : ScriptableObject
    {
        public int CellsCount => SelectedCells.Count;

        public int PointsXCount;
        public int PointsYCount;

        [SerializeField] public List<Vector2Int> SelectedCells;
    }
}