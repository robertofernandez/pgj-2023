using System.Collections.Generic;

namespace com.sdmission.logic.model
{
    public class GameMapTile
    {
        public string id { private set; get; }
		public string blockType { private set; get; }
		List<GameItem> containedItems;

        public GameMapTile(string id, string blockType)
        {
            this.id = id;
            this.blockType = blockType;
			containedItems = new List<GameItem>();
        }
    }
}