using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Braintree;
using System.Threading.Tasks;

namespace AdrianBookStore
{
    public class PAAPaymentService
    {
        protected Braintree.BraintreeGateway gateway;
        public PAAPaymentService()
        {
            string merchantId = System.Configuration.ConfigurationManager.AppSettings["Braintree.MerchantId"];
            string environment = System.Configuration.ConfigurationManager.AppSettings["Braintree.Environment"];
            string publicKey = System.Configuration.ConfigurationManager.AppSettings["Braintree.PublicKey"];
            string privateKey = System.Configuration.ConfigurationManager.AppSettings["Braintree.PrivateKey"];
            gateway = new Braintree.BraintreeGateway(environment, merchantId, publicKey, privateKey);
        }

        public async Task<Braintree.Customer> GetCustomerAsync(string email)
        {
            var customerGateway = gateway.Customer;
            CustomerSearchRequest query = new Braintree.CustomerSearchRequest();
            query.Email.Is(email);
            var matchedCustomers = await customerGateway.SearchAsync(query);
            if (matchedCustomers.Ids.Count == 0)
            {
                CustomerRequest newCustomer = new CustomerRequest()
                {
                    Email = email
                };

                var result = await customerGateway.CreateAsync(newCustomer);
                return result.Target;
            }
            else
            {
                return matchedCustomers.FirstItem;
            }
        }

        public async Task<Customer> UpdateCustomerAsync(string firstName, string lastName, string id)
        {
            Braintree.CustomerRequest request = new Braintree.CustomerRequest();
            request.FirstName = firstName;
            request.LastName = lastName;
            var result = await gateway.Customer.UpdateAsync(id, request);
            return result.Target;
        }

        public async Task DeleteAddressAsync(string email, string id)
        {
            Customer customer = await GetCustomerAsync(email);
            await gateway.Address.DeleteAsync(customer.Id, id);
        }

        public async Task AddAddressAsync(string email, string firstName, string lastName, string company, string streetAddress, string extendedAddress, string locality, string region, string postalCode, string countryName)
        {
            Customer customer = await GetCustomerAsync(email);

            Braintree.AddressRequest newAddress = new Braintree.AddressRequest
            {
                FirstName = firstName,
                LastName = lastName,
                Company = company,
                CountryName = countryName,
                PostalCode = postalCode,
                ExtendedAddress = extendedAddress,
                Locality = locality,
                Region = region,
                StreetAddress = streetAddress
            };

            await gateway.Address.CreateAsync(customer.Id, newAddress);
        }

        public async Task<string> AuthorizeCard(string email, decimal total, decimal tax, string trackingNumber, string addressId, string cardholderName, string cvv, string cardNumber, string expirationMonth, string expirationYear)
        {
            var customer = await GetCustomerAsync(email);
            Braintree.TransactionRequest transaction = new Braintree.TransactionRequest();
            transaction.Amount = total;
            transaction.TaxAmount = tax;
            transaction.OrderId = trackingNumber;
            transaction.CustomerId = customer.Id;
            transaction.ShippingAddressId = addressId;
            //https://developers.braintreepayments.com/reference/general/testing/ruby
            transaction.CreditCard = new Braintree.TransactionCreditCardRequest
            {
                CardholderName = cardholderName,
                CVV = cvv,
                Number = cardNumber,
                ExpirationYear = expirationYear,
                ExpirationMonth = expirationMonth
            };

            var result = await gateway.Transaction.SaleAsync(transaction);

            return result.Message;
        }
    }
}