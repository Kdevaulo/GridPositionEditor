using UnityEngine;
using UnityEngine.Assertions;

namespace Kdevaulo.GridPositionEditor
{
    public class GridPositioner : MonoBehaviour
    {
        [SerializeField] private Grid _grid;
        [SerializeField] private PositionsData _positionsData;
        [SerializeField] private Transform[] _itemsToPlace;

        private void Start()
        {
            var positions = _positionsData.SelectedCells;

            Assert.IsTrue(positions.Count >= _itemsToPlace.Length);

            for (int i = 0; i < _itemsToPlace.Length; i++)
            {
                _itemsToPlace[i].localPosition = GetPositionByGrid(positions[i]);
            }
        }

        private Vector3 GetPositionByGrid(Vector2Int coordinates)
        {
            return _grid.GetCellCenterLocal((Vector3Int) coordinates);
        }
    }
}