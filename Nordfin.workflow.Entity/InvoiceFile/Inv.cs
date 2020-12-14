namespace Nordfin.workflow.Entity
{
    public class Inv
    {
        public Inv()
        {
            Invoice = new Invoice();
            Customer = new Customer();
            Print = new Print();
        }

        public Invoice Invoice { get; set; }

        public Customer Customer { get; set; }

        public Print Print { get; set; }
    }
}
