namespace OpenCBS.CoreDomain
{
    public  class EntryFee
    {
        public EntryFee()
        {
            Id = null;
            Name = "";
            Min = null;
            Max = null;
            Value = null;
            IsRate = false;
            Index = -1;
        }
        public int? Id { get; set; }
        public string Name { get; set; }
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
        public decimal? Value { get; set; }
        public bool IsRate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdded { get; set; }
        public int Index { get; set; }
        public int? CycleId { get; set; }
        public int IdForNewItem { get; set; }

        public void Copy(EntryFee entryFee)
        {
            entryFee.Id = Id;
            entryFee.Name = Name;
            entryFee.Min = Min;
            entryFee.Max = Max;
            entryFee.Value = Value;
            entryFee.IsRate = IsRate;
            entryFee.IsAdded = IsAdded;
            entryFee.Index = Index;
            entryFee.CycleId = CycleId;
            entryFee.IdForNewItem = IdForNewItem;
        }
    }
}
