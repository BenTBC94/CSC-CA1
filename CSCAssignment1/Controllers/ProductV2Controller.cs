using CSCAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CSCAssignment1.Controllers
{
    public class ProductV2Controller : ApiController
    {
        static readonly IProductRepository repository = new ProductRepository();

        [HttpGet] //Added in
        [Route("api/ProductV2/getAllProducts")] //Added in
        public IEnumerable<Product> GetAllProducts()
        {
            return repository.GetAll();
        }

        [HttpGet] //Added in
        [Route("api/ProductV2/getProducts", Name = "getProductsByIdv2")] //Added in, 
        public Product GetProduct(int id)
        {
            Product item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        [HttpGet] //Added in
        [Route("api/ProductV2/getProductsByCategory")] //Added in
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return repository.GetAll().Where(
                p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        [HttpPost] //Added in
        [Route("api/ProductV2/addProducts")] //Added in
        public HttpResponseMessage PostProduct(Product item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item);

            string uri = Url.Link("getProductsByIdv2", new { id = item.Id }); //"DefaultApi" changed to "getProductsByIdv2"
            response.Headers.Location = new Uri(uri);
            return Request.CreateResponse(HttpStatusCode.OK, "Id " + item.Id + " " + item.Name + " is successfully added");
        }

        [HttpPut] // Added in
        [Route("api/ProductV2/updateProducts/{id:int}")] //Added in
        public string PutProduct(int id, Product product) // "void" changed to "string"
        {
            product.Id = id;
            String message = "Product " + id + " has been updated!"; // Custom message
            if (!repository.Update(product))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                return message;
            }
        }

        [HttpDelete] //Added in
        [Route("api/ProductV2/deleteProducts/{id:int}")] //Added in
        public string DeleteProduct(int id) //"void" changed to "string"
        {
            Product item = repository.Get(id);
            String message = "Product " + id + " has been deleted!"; // Custom message
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                repository.Remove(id);
                return message;
            }
        }
    }
}
