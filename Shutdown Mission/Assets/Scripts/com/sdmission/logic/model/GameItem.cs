namespace com.sdmission.logic.model
{
    public class GameItem
    {
        public string id { private set; get; }
		public string itemType { private set; get; }

        public GameItem(string id, string itemType)
        {
            this.id = id;
            this.itemType = itemType;
        }
    }
}