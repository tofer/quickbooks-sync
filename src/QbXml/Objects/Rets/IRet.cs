namespace QbSync.QbXml.Objects
{
    /// <summary>
    /// Common properties in QuickBooks returned objects
    /// </summary>
    public interface IRet
    {
        DATETIMETYPE TimeCreated { get; }
        DATETIMETYPE TimeModified { get; }
        string EditSequence { get; }
    }
}
