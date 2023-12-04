using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Shop
{
    public class ShopDataConfig : SerializedScriptableObject
    {
        public Dictionary<string, ShopIAPCsv> gemPacks = new();

        #region Init Data From Csv

        private ShopIAPCsv[] _needConvertGemPacks;

        public void ConvertGemPacks()
        {
            gemPacks = new();
            foreach (ShopIAPCsv entry in _needConvertGemPacks)
            {
                gemPacks.TryAdd(entry.gameProductId, entry);
            }
        }

        #endregion

        #region Getter

        public bool TryGetGemPackByGameProductId(string gameProductId, out ShopIAPCsv packData)
        {
            var result = gemPacks.TryGetValue(gameProductId, out packData);
            if (result == false)
            {
                packData = new ShopIAPCsv();
            }
            return result;
        }

        #endregion
    }
}