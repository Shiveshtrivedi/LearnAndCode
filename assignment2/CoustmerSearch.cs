public class CustomerSearchHandler
{
    private List<Customer> CreateCustomerQuery(string field, string value)
    {
        var query =
            from c in db.customers
            where c.GetType().GetProperty(field).GetValue(c, null).ToString().Contains(value)
            orderby c.CustomerID ascending
            select c;

        return query.ToList();
    }

    public List<Customer> SearchByCountry(string country)
    {
        return CreateCustomerQuery("Country", country);
    }

    public List<Customer> SearchByCompanyName(string company)
    {
        return CreateCustomerQuery("CompanyName", company);
    }

    public List<Customer> SearchByContact(string contact)
    {
        return CreateCustomerQuery("ContactName", contact);
    }

    private string GenerateCSV(List<Customer> customerData)
    {
        StringBuilder sb = new StringBuilder();

        foreach (var item in customerData)
        {
            sb.AppendFormat("{0},{1},{2},{3}", item.CustomerID, item.CompanyName, item.ContactName, item.Country);
            sb.AppendLine();
        }

        return sb.ToString();
    }

    public string ExportToCSV(List<Customer> customerData)
    {
        return GenerateCSV(customerData);
    }
}
