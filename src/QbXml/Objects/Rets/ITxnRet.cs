namespace QbSync.QbXml.Objects
{
    /// <summary>
    /// Common properties in QuickBooks returned Txn objects
    /// </summary>
    public interface ITxnRet : IRet
    {
        string TxnID { get; }
        string TxnNumber { get; }
        DATETYPE TxnDate { get; }
    }
}
