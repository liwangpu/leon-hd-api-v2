namespace App.OSS.Domain.SeedWork
{
    public abstract class PagingSpecification<T> : Specification<T>, IPagingSpecification<T>
      where T : IAggregateRoot
    {
        public int Page { get; protected set; }
        public int PageSize { get; protected set; }
        public string OrderBy { get; protected set; }
        public bool Desc { get; protected set; } = true;
        public string Ext1 { get; protected set; }
        public string Ext2 { get; protected set; }
    }
}
