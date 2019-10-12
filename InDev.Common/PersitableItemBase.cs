namespace InDev.Common
{
    public abstract class PersistableItemBase
    {
        public PersistableItemBase()
        {

        }

        public PersistableItemBase(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
    }
}
