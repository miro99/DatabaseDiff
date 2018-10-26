namespace DbDiff
{
    public class Table
    {
        private readonly string tableName;

        public Table(string tableName)
        {
            this.tableName = tableName;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Table))
            {
                return false;
            }
            return this.tableName.Equals(((Table)obj).tableName);
        }

        public override int GetHashCode()
        {
            return tableName.GetHashCode();
        }
    }
}