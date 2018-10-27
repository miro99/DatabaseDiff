namespace DbDiff
{
    public class Table : INamed
    {
        private readonly string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }            
        }

        public Table(string tableName)
        {
            this._Name = tableName;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Table))
            {
                return false;
            }
            return this.Name.Equals(((Table)obj).Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}