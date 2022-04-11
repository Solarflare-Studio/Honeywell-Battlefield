using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Contact
{
    [System.Serializable]
    public class RequestedProduct
    {
        public string Name { get; set; }
        public bool IsRequested { get; set; }
    }

    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public string JobTitle { get; set; }
    public List<RequestedProduct> RequestedProducts { get; set; }
}



