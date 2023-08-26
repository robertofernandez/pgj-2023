namespace com.sdmission.logic.model
{
    public class GameMapTile
    {
        public string id { private set; get; }
		public string blockType { private set; get; }
		public GameItem[] containedItems;
		

        public GameMapTile(string id, string blockType)
        {
            this.id = id;
            this.blockType = blockType;
        }
    }
}