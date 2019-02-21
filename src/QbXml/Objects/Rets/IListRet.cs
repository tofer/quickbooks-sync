namespace QbSync.QbXml.Objects
{
    /// <summary>
    /// Common properties in QuickBooks returned list objects
    /// </summary>
    public interface IListRet : IRet
    {
        string ListID { get; }
    }
}
