using UnityEngine;
using UnityEngine.Assertions;

namespace Kdevaulo.GridPositionEditor
{
    public class RandomGridInitializer : MonoBehaviour
    {
        [Header("Values")]
        [SerializeField] private int _itemsCount;

        [Header("References")]
        [SerializeField] private Grid _grid;
        [SerializeField] private PositionsData _positionsData;
        [SerializeField] private Transform _parent;

        [SerializeField] private Sprite[] _randomSprites;

        private void Start()
        {
            var positions = _positionsData.SelectedCells;

            Assert.IsTrue(positions.Count >= _itemsCount, "There were more items to place than places");

            for (int i = 0; i < _itemsCount; i++)
            {
                var item = new GameObject($"Item {i}");
                var spriteRenderer = item.AddComponent<SpriteRenderer>();

                SetRandomSprite(spriteRenderer);

                item.transform.SetParent(_parent);
                item.transform.Rotate(Vector3.forward, 90);

                item.transform.localPosition = GetPositionByGrid(positions[i]);
            }
        }

        private void SetRandomSprite(SpriteRenderer spriteRenderer)
        {
            spriteRenderer.sprite = _randomSprites[Random.Range(0, _randomSprites.Length)];
        }

        private Vector3 GetPositionByGrid(Vector2Int coordinates)
        {
            return _grid.GetCellCenterLocal((Vector3Int) coordinates);
        }
    }
}