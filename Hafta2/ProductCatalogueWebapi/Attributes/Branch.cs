namespace ProductCatalogueWebapi.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.All, AllowMultiple = true)]
    public class Branch : System.Attribute
    {
        private string Name { get; set; }
        
        // This is a positional argument
        public Branch(string name)
        {
            Name = name;
        }
        
        public string BranchName()
        {
            // get { return branchName; }
            return Name;
        }
        
        // This is a named argument
        // public int NamedInt { get; set; }
    }
}